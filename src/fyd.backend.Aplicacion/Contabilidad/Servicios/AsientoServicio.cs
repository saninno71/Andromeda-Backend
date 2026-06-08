using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Entidades;

namespace fyd.backend.Aplicacion.Contabilidad.Servicios
{
    public class AsientoServicio : IAsientoServicio
    {
        private readonly IAsientoRepositorio _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public AsientoServicio(IAsientoRepositorio repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        // =====================================================
        // Crear Asiento
        // =====================================================
        public async Task<Resultado<int>> CrearAsiento(CrearAsientoDto dto)
        {
            if (dto.CotizacionLocal <= 0 || dto.CotizacionReferencia <= 0)
                return Resultado<int>.Falla(AsientoError.CotizacionInvalida);

            // B11: Validar que el período no esté cerrado
            if (await _repositorio.PeriodoEstaCerrado(dto.Fecha))
                return Resultado<int>.Falla(AsientoError.PeriodoCerrado);

            if (await _repositorio.PeriodoEstaRenumerado(dto.Fecha))
                return Resultado<int>.Falla(AsientoError.PeriodoRenumerado);

            // Crear el comprobante cabecera
            // TODO: Cuando exista la lógica de numeración (NumeraTipos/Numeraciones),
            //       obtener automáticamente Letra, PuntoVenta, y auto-numerar si Numero es null.
            var resultadoComprobante = Comprobante.Crear(
                fecha: dto.Fecha,
                fechaEmision: dto.FechaEmision,
                numeraTipoId: dto.NumeraTipoId,
                numero: dto.Numero ?? 0,
                empresaId: dto.EmpresaId,
                monedaId: dto.MonedaId,
                cotizacionLocal: dto.CotizacionLocal,
                cotizacionReferencia: dto.CotizacionReferencia,
                impTotal: 0,
                detalle: dto.Detalle,
                memo: dto.Observaciones
            );

            if (!resultadoComprobante.Exitoso)
                return Resultado<int>.Falla(resultadoComprobante.Error!);

            var comprobante = resultadoComprobante.Valor!;

            // Validar y crear cada línea de asiento
            var lineas = new List<Asiento>();

            foreach (var linea in dto.Lineas)
            {
                // Validar la línea contra el dominio (B1-B9)
                var resultadoValidacion = await ValidarLineaDeAsiento(
                    linea.CuentaId, linea.Tipo, linea.Importe, linea.Detalle,
                    linea.ClienteId, linea.ProveedorId, linea.CajaId,
                    dto.EmpresaId);

                if (!resultadoValidacion.Exitoso)
                    return Resultado<int>.Falla(resultadoValidacion.Error!);

                // Calcular importes en moneda local y referencia
                decimal impLocal = linea.Importe * dto.CotizacionLocal;
                decimal impReferencia = linea.Importe * dto.CotizacionReferencia;

                var resultadoAsiento = Asiento.Crear(
                    comprobanteId: 0, // Se asigna después del save del comprobante
                    cuentaId: linea.CuentaId,
                    tipo: linea.Tipo,
                    importe: linea.Importe,
                    impLocal: impLocal,
                    impReferencia: impReferencia,
                    detalle: linea.Detalle,
                    clienteId: linea.ClienteId,
                    proveedorId: linea.ProveedorId,
                    cajaId: linea.CajaId);

                if (!resultadoAsiento.Exitoso)
                    return Resultado<int>.Falla(resultadoAsiento.Error!);

                lineas.Add(resultadoAsiento.Valor!);
            }

            // B10: Validar que el asiento balancee (Σ Debe = Σ Haber)
            decimal totalDebe = lineas.Where(l => l.Tipo == 1).Sum(l => l.Importe);
            decimal totalHaber = lineas.Where(l => l.Tipo == -1).Sum(l => l.Importe);

            if (totalDebe != totalHaber)
                return Resultado<int>.Falla(AsientoError.AsientoNoBalancea);

            // Calcular el importe total del comprobante y actualizarlo
            var resultadoActualizarComprobante = comprobante.Actualizar(
                comprobante.Fecha,
                comprobante.FechaEmision,
                comprobante.NumeraTipoId,
                comprobante.Numero,
                comprobante.EmpresaId,
                comprobante.MonedaId,
                comprobante.CotizacionLocal,
                comprobante.CotizacionReferencia,
                impTotal: totalDebe,
                comprobante.Detalle,
                comprobante.Memo
            );

            if (!resultadoActualizarComprobante.Exitoso)
                return Resultado<int>.Falla(resultadoActualizarComprobante.Error!);

            // Asignar las líneas al comprobante y configurar navegación inversa
            var propComprobante = typeof(Asiento).GetProperty("Comprobante");
            foreach (var linea in lineas)
            {
                comprobante.Asientos.Add(linea);
                if (propComprobante != null && propComprobante.CanWrite)
                {
                    propComprobante.SetValue(linea, comprobante, null);
                }
                await _repositorio.Agregar(linea);
            }

            // Persistir
            await _unidadDeTrabajo.GuardarCambios();

            return Resultado<int>.Exito(comprobante.Id);
        }

        // =====================================================
        // Obtener por Id
        // =====================================================
        public async Task<Resultado<ConsultarAsientoDto>> ObtenerPorId(int id)
        {
            // Buscamos por el primer asiento del comprobante
            var asiento = await _repositorio.ObtenerPorId(id);

            if (asiento == null || asiento.Comprobante == null)
                return Resultado<ConsultarAsientoDto>.Falla(AsientoError.NoEncontrado(id));

            var comprobante = asiento.Comprobante;

            // TODO: Cuando existan las entidades de NumeraTipos y Numeraciones,
            //       obtener Simbolo, Letra y PuntoVenta del comprobante.
            var dto = new ConsultarAsientoDto(
                Id: comprobante.Id,
                Fecha: comprobante.Fecha,
                FechaEmision: comprobante.FechaEmision,
                NumeraTipoId: comprobante.NumeraTipoId,
                NumeraTipoSimbolo: "", // TODO: Obtener de NumeraTipos
                Letra: "",             // TODO: Obtener de Numeraciones
                PuntoVenta: null,      // TODO: Obtener de Numeraciones
                Numero: comprobante.Numero,
                EmpresaId: comprobante.EmpresaId,
                EmpresaNombre: "",     // TODO: Obtener de Empresas
                MonedaId: comprobante.MonedaId,
                MonedaNombre: "",      // TODO: Obtener de Monedas
                CotizacionLocal: comprobante.CotizacionLocal,
                CotizacionReferencia: comprobante.CotizacionReferencia,
                ImpTotal: comprobante.ImpTotal,
                Detalle: comprobante.Detalle,
                Observaciones: comprobante.Memo,
                Lineas: comprobante.Asientos.Select(a => new ConsultarAsientoLineaDto(
                    Id: a.Id,
                    CuentaId: a.CuentaId,
                    CuentaCodigo: "", // TODO: Obtener de CuentaContable
                    CuentaNombre: "", // TODO: Obtener de CuentaContable
                    Tipo: a.Tipo,
                    Importe: a.Importe,
                    ImpLocal: a.ImpLocal,
                    ImpReferencia: a.ImpReferencia,
                    Detalle: a.Detalle,
                    ClienteId: a.ClienteId,
                    ClienteNombre: null,   // TODO: Obtener de Cliente → Agenda
                    ProveedorId: a.ProveedorId,
                    ProveedorNombre: null,  // TODO: Obtener de Proveedor → Agenda
                    CajaId: a.CajaId,
                    CajaNombre: null        // TODO: Obtener de Caja
                )).ToList()
            );

            return Resultado<ConsultarAsientoDto>.Exito(dto);
        }

        // =====================================================
        // Actualizar Asiento
        // =====================================================
        public async Task<Resultado> ActualizarAsiento(ActualizarAsientoDto dto)
        {
            if (dto.CotizacionLocal <= 0 || dto.CotizacionReferencia <= 0)
                return Resultado.Falla(AsientoError.CotizacionInvalida);

            // Buscar asiento existente para validar
            var asientoExistente = await _repositorio.ObtenerPorId(dto.Id);

            if (asientoExistente == null || asientoExistente.Comprobante == null)
                return Resultado.Falla(AsientoError.NoEncontrado(dto.Id));

            // B12: Validar que no fue generado por un comprobante padre
            if (await _repositorio.TieneComprobantePadre(asientoExistente.Id))
                return Resultado.Falla(AsientoError.GeneradoPorComprobantePadre);

            // B11: Validar período abierto
            if (await _repositorio.PeriodoEstaCerrado(dto.Fecha))
                return Resultado.Falla(AsientoError.PeriodoCerrado);

            if (await _repositorio.PeriodoEstaRenumerado(dto.Fecha))
                return Resultado.Falla(AsientoError.PeriodoRenumerado);

            var comprobante = asientoExistente.Comprobante;

            if (await _repositorio.TieneNumeroAsientoAsignado(comprobante.Id))
            {
                if (comprobante.EmpresaId != dto.EmpresaId || 
                    comprobante.NumeraTipoId != dto.NumeraTipoId || 
                    comprobante.Fecha != dto.Fecha)
                {
                    return Resultado.Falla(AsientoError.ComprobanteConNumeroAsiento);
                }
            }

            // Validar cada línea nueva
            foreach (var linea in dto.Lineas)
            {
                var resultadoValidacion = await ValidarLineaDeAsiento(
                    linea.CuentaId, linea.Tipo, linea.Importe, linea.Detalle,
                    linea.ClienteId, linea.ProveedorId, linea.CajaId,
                    dto.EmpresaId);

                if (!resultadoValidacion.Exitoso)
                    return Resultado.Falla(resultadoValidacion.Error!);
            }

            // B10: Validar balance
            decimal totalDebe = dto.Lineas.Where(l => l.Tipo == 1).Sum(l => l.Importe);
            decimal totalHaber = dto.Lineas.Where(l => l.Tipo == -1).Sum(l => l.Importe);

            if (totalDebe != totalHaber)
                return Resultado.Falla(AsientoError.AsientoNoBalancea);

            // Actualizar cabecera
            var resultadoActualizar = comprobante.Actualizar(
                fecha: dto.Fecha,
                fechaEmision: dto.FechaEmision,
                numeraTipoId: dto.NumeraTipoId,
                numero: dto.Numero ?? comprobante.Numero,
                empresaId: dto.EmpresaId,
                monedaId: dto.MonedaId,
                cotizacionLocal: dto.CotizacionLocal,
                cotizacionReferencia: dto.CotizacionReferencia,
                impTotal: totalDebe,
                detalle: dto.Detalle,
                memo: dto.Observaciones
            );

            if (!resultadoActualizar.Exitoso)
                return Resultado.Falla(resultadoActualizar.Error!);

            // Reemplazar líneas: eliminar existentes y recrear
            comprobante.Asientos.Clear();

            foreach (var linea in dto.Lineas)
            {
                decimal impLocal = linea.Importe * dto.CotizacionLocal;
                decimal impReferencia = linea.Importe * dto.CotizacionReferencia;

                var resultadoAsiento = Asiento.Crear(
                    comprobanteId: comprobante.Id,
                    cuentaId: linea.CuentaId,
                    tipo: linea.Tipo,
                    importe: linea.Importe,
                    impLocal: impLocal,
                    impReferencia: impReferencia,
                    detalle: linea.Detalle,
                    clienteId: linea.ClienteId,
                    proveedorId: linea.ProveedorId,
                    cajaId: linea.CajaId);

                if (!resultadoAsiento.Exitoso)
                    return Resultado.Falla(resultadoAsiento.Error!);

                comprobante.Asientos.Add(resultadoAsiento.Valor!);
            }

            await _unidadDeTrabajo.GuardarCambios();

            return Resultado.Exito();
        }

        // =====================================================
        // Eliminar Asiento
        // =====================================================
        public async Task<Resultado> EliminarAsiento(int id)
        {
            var asiento = await _repositorio.ObtenerPorId(id);

            if (asiento == null || asiento.Comprobante == null)
                return Resultado.Falla(AsientoError.NoEncontrado(id));

            // B12: Validar que no fue generado por un comprobante padre
            if (await _repositorio.TieneComprobantePadre(asiento.Id))
                return Resultado.Falla(AsientoError.GeneradoPorComprobantePadre);

            // B11: Validar período abierto
            if (await _repositorio.PeriodoEstaCerrado(asiento.Comprobante.Fecha))
                return Resultado.Falla(AsientoError.PeriodoCerrado);

            if (await _repositorio.PeriodoEstaRenumerado(asiento.Comprobante.Fecha))
                return Resultado.Falla(AsientoError.PeriodoRenumerado);

            // B13: Validar que no esté vinculado
            if (await _repositorio.TieneVinculaciones(asiento.Id))
                return Resultado.Falla(AsientoError.VinculadoAOtroComprobante);

            // B14: Validar que no esté aplicado en Cta. Cte.
            if (await _repositorio.TieneAplicacionesEnCtaCte(asiento.ComprobanteId))
                return Resultado.Falla(AsientoError.AplicadoEnCtaCte);

            _repositorio.Eliminar(asiento);
            await _unidadDeTrabajo.GuardarCambios();

            return Resultado.Exito();
        }

        // =====================================================
        // Método privado: Validar línea de asiento (B1-B9)
        // =====================================================
        private async Task<Resultado> ValidarLineaDeAsiento(
            int cuentaId, int tipo, decimal importe, string detalle,
            int? clienteId, int? proveedorId, int? cajaId,
            int empresaIdComprobante)
        {
            // B1: La cuenta debe existir
            if (!await _repositorio.ExisteCuenta(cuentaId))
                return Resultado.Falla(AsientoError.CuentaNoExiste(cuentaId));

            // B3: La cuenta debe permitir asientos
            if (!await _repositorio.CuentaPermiteAsiento(cuentaId))
                return Resultado.Falla(AsientoError.CuentaNoPermiteAsiento(cuentaId));

            // Validar subcuenta caja
            if (await _repositorio.CuentaTieneSubcuentaCaja(cuentaId))
                return Resultado.Falla(AsientoError.SubcuentaCajaInvalida(cuentaId));

            // B2: La empresa de la cuenta debe coincidir con la del comprobante
            var empresaIdCuenta = await _repositorio.ObtenerEmpresaIdDeCuenta(cuentaId);
            if (empresaIdCuenta.HasValue && empresaIdCuenta.Value != empresaIdComprobante)
                return Resultado.Falla(AsientoError.CuentaNoPerteneceAEmpresa(cuentaId, empresaIdComprobante));

            // B4, B5: Validar cliente
            if (clienteId.HasValue)
            {
                if (!await _repositorio.ExisteCliente(clienteId.Value))
                    return Resultado.Falla(AsientoError.ClienteNoExiste(clienteId.Value));

                var empresaIdCliente = await _repositorio.ObtenerEmpresaIdDeCliente(clienteId.Value);
                if (empresaIdCliente.HasValue && empresaIdCliente.Value != empresaIdComprobante)
                    return Resultado.Falla(AsientoError.ClienteNoPerteneceAEmpresa(clienteId.Value, empresaIdComprobante));
            }

            // B6, B7: Validar proveedor
            if (proveedorId.HasValue)
            {
                if (!await _repositorio.ExisteProveedor(proveedorId.Value))
                    return Resultado.Falla(AsientoError.ProveedorNoExiste(proveedorId.Value));

                var empresaIdProveedor = await _repositorio.ObtenerEmpresaIdDeProveedor(proveedorId.Value);
                if (empresaIdProveedor.HasValue && empresaIdProveedor.Value != empresaIdComprobante)
                    return Resultado.Falla(AsientoError.ProveedorNoPerteneceAEmpresa(proveedorId.Value, empresaIdComprobante));
            }

            // B8, B9: Validar caja
            if (cajaId.HasValue)
            {
                if (!await _repositorio.ExisteCaja(cajaId.Value))
                    return Resultado.Falla(AsientoError.CajaNoExiste(cajaId.Value));

                var empresaIdCaja = await _repositorio.ObtenerEmpresaIdDeCaja(cajaId.Value);
                if (empresaIdCaja.HasValue && empresaIdCaja.Value != empresaIdComprobante)
                    return Resultado.Falla(AsientoError.CajaNoPerteneceAEmpresa(cajaId.Value, empresaIdComprobante));

                if (await _repositorio.CajaBancariaEstaConciliada(cajaId.Value))
                    return Resultado.Falla(AsientoError.CajaBancariaConciliada(cajaId.Value));
            }

            return Resultado.Exito();
        }
    }
}
