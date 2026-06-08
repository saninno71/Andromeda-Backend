using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Enums;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Repositorios;

namespace fyd.backend.Aplicacion.Contabilidad.Servicios
{
    public class ConsolidacionServicio : IConsolidacionServicio
    {
        private readonly IConsolidacionRepositorio _repositorio;
        private readonly IBloqueoRepositorio _bloqueoRepositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        private const string TablaBloqueo = "abmctbConsolidacion";

        public ConsolidacionServicio(
            IConsolidacionRepositorio repositorio,
            IBloqueoRepositorio bloqueoRepositorio,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorio = repositorio;
            _bloqueoRepositorio = bloqueoRepositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        // =====================================================
        // Verificar estado de cada módulo para el período
        // =====================================================

        public async Task<Resultado<IReadOnlyList<ModuloEstadoDto>>> VerificarEstado(VerificarEstadoDto dto)
        {
            var primerDia = new DateTime(dto.Anio, dto.Mes, 1);
            var ultimoDia = primerDia.AddMonths(1).AddDays(-1);

            var resultados = new List<ModuloEstadoDto>();

            foreach (var modulo in Enum.GetValues<ModuloConsolidacion>())
            {
                var tipos = modulo.ObtenerTiposComprobante();
                var yaGenerado = await _repositorio.ModuloYaGenerado(dto.EmpresaId, primerDia, ultimoDia, tipos);
                var tieneComprobantes = await _repositorio.TieneComprobantes(dto.EmpresaId, primerDia, ultimoDia, tipos);

                resultados.Add(new ModuloEstadoDto(
                    modulo.ObtenerNombre(),
                    yaGenerado,
                    tieneComprobantes
                ));
            }

            return Resultado<IReadOnlyList<ModuloEstadoDto>>.Exito(resultados.AsReadOnly());
        }

        // =====================================================
        // Generar asientos de consolidación
        // Migración de abmctbConsolidacion_Asientos (@EliminarOK=0)
        // =====================================================

        public async Task<Resultado> Generar(GenerarConsolidacionDto dto)
        {
            var modulosParseados = ParsearModulos(dto.Modulos);
            if (modulosParseados is null)
                return Resultado.Falla(ConsolidacionError.ModuloNoValido);

            var primerDia = new DateTime(dto.Anio, dto.Mes, 1);
            var ultimoDia = primerDia.AddMonths(1).AddDays(-1);
            var periodoId = dto.Anio * 100 + dto.Mes;

            foreach (var modulo in modulosParseados)
            {
                var tipos = modulo.ObtenerTiposComprobante();

                var yaGenerado = await _repositorio.ModuloYaGenerado(dto.EmpresaId, primerDia, ultimoDia, tipos);
                if (yaGenerado)
                    return Resultado.Falla(ConsolidacionError.PeriodoYaGenerado(modulo.ObtenerNombre()));

                var tieneComprobantes = await _repositorio.TieneComprobantes(dto.EmpresaId, primerDia, ultimoDia, tipos);
                if (!tieneComprobantes)
                    return Resultado.Falla(ConsolidacionError.SinComprobantes(modulo.ObtenerNombre()));
            }

            if (await _bloqueoRepositorio.BloqueoExiste(TablaBloqueo, periodoId))
                return Resultado.Falla(ConsolidacionError.BloqueoOcupado);

            await _bloqueoRepositorio.AgregarBloqueo(TablaBloqueo, periodoId);

            try
            {
                foreach (var modulo in modulosParseados)
                {
                    var tipos = modulo.ObtenerTiposComprobante();
                    var imputaciones = await _repositorio.ObtenerImputaciones(dto.EmpresaId, primerDia, ultimoDia, tipos);

                    if (dto.PorComprobante)
                    {
                        var resultado = await GenerarPorComprobante(dto, modulo, imputaciones);
                        if (!resultado.Exitoso)
                            return resultado;
                    }
                    else
                    {
                        var resultado = await GenerarResumen(dto, modulo, primerDia, ultimoDia, imputaciones);
                        if (!resultado.Exitoso)
                            return resultado;
                    }

                    var valido = await _repositorio.ValidarAsientosCompletos(dto.EmpresaId, primerDia, ultimoDia, tipos);
                    if (!valido)
                    {
                        await _repositorio.DesvincularComprobantes(dto.EmpresaId, primerDia, ultimoDia, tipos);
                        await _unidadDeTrabajo.GuardarCambios();
                        return Resultado.Falla(ConsolidacionError.ValidacionAsientosFallida(modulo.ObtenerNombre()));
                    }
                }

                return Resultado.Exito();
            }
            finally
            {
                await _bloqueoRepositorio.EliminarBloqueo(TablaBloqueo, periodoId);
            }
        }

        // =====================================================
        // Eliminar asientos de consolidación del período
        // Migración de abmctbConsolidacion_Asientos (@EliminarOK=1)
        // =====================================================

        public async Task<Resultado> Eliminar(EliminarConsolidacionDto dto)
        {
            var modulosParseados = ParsearModulos(dto.Modulos);
            if (modulosParseados is null)
                return Resultado.Falla(ConsolidacionError.ModuloNoValido);

            var primerDia = new DateTime(dto.Anio, dto.Mes, 1);
            var ultimoDia = primerDia.AddMonths(1).AddDays(-1);
            var periodoId = dto.Anio * 100 + dto.Mes;

            foreach (var modulo in modulosParseados)
            {
                var tipos = modulo.ObtenerTiposComprobante();
                var yaGenerado = await _repositorio.ModuloYaGenerado(dto.EmpresaId, primerDia, ultimoDia, tipos);
                if (!yaGenerado)
                    return Resultado.Falla(ConsolidacionError.PeriodoNoPuedeEliminarse);
            }

            if (await _bloqueoRepositorio.BloqueoExiste(TablaBloqueo, periodoId))
                return Resultado.Falla(ConsolidacionError.BloqueoOcupado);

            await _bloqueoRepositorio.AgregarBloqueo(TablaBloqueo, periodoId);

            try
            {
                foreach (var modulo in modulosParseados)
                {
                    var tipos = modulo.ObtenerTiposComprobante();
                    var asientosAEliminar = await _repositorio.ObtenerAsientosParaEliminar(dto.EmpresaId, primerDia, ultimoDia, tipos);

                    await _repositorio.DesvincularComprobantes(dto.EmpresaId, primerDia, ultimoDia, tipos);

                    foreach (var asientoGnrId in asientosAEliminar)
                        await _repositorio.EliminarAsientoPorComprobanteId(asientoGnrId);

                    await _unidadDeTrabajo.GuardarCambios();
                }

                return Resultado.Exito();
            }
            finally
            {
                await _bloqueoRepositorio.EliminarBloqueo(TablaBloqueo, periodoId);
            }
        }

        // =====================================================
        // Helpers privados
        // =====================================================

        private async Task<Resultado> GenerarPorComprobante(
            GenerarConsolidacionDto dto,
            ModuloConsolidacion modulo,
            IReadOnlyList<ImputacionConsolidacionData> imputaciones)
        {
            var agrupados = imputaciones.GroupBy(i => i.ComprobanteId);

            foreach (var grupo in agrupados)
            {
                var primera = grupo.First();
                var lineas = await ConstruirLineasPorComprobante(grupo.ToList(), dto.EmpresaId, modulo);

                var detalle = ConstruirDetallePorComprobante(primera);

                var asientoGnrId = await _repositorio.GuardarAsientoDeConsolidacion(new AsientoParaGuardar(
                    Fecha: primera.Fecha,
                    FechaEmision: DateTime.Today,
                    NumeraTipoId: dto.NumeraTipoId,
                    EmpresaId: dto.EmpresaId,
                    MonedaId: primera.MonedaId,
                    CotizacionLocal: primera.CotizacionLocal,
                    CotizacionReferencia: primera.CotizacionReferencia,
                    ImpTotal: primera.ImpTotal,
                    Detalle: string.IsNullOrWhiteSpace(dto.Detalle) ? detalle : dto.Detalle,
                    Memo: dto.Observaciones,
                    Lineas: lineas
                ));

                await _repositorio.VincularComprobanteConAsiento(grupo.Key, asientoGnrId, dto.EmpresaId);
                await _unidadDeTrabajo.GuardarCambios();
            }

            return Resultado.Exito();
        }

        private async Task<Resultado> GenerarResumen(
            GenerarConsolidacionDto dto,
            ModuloConsolidacion modulo,
            DateTime primerDia,
            DateTime ultimoDia,
            IReadOnlyList<ImputacionConsolidacionData> imputaciones)
        {
            // Acumular líneas por CuentaId y Tipo (débito/crédito)
            var acumulado = new Dictionary<(int CuentaId, int Tipo), (decimal Importe, decimal ImpLocal, decimal ImpRef)>();

            foreach (var imp in imputaciones)
            {
                var clave = (imp.CuentaId, imp.NumeraTipoTipo);
                if (acumulado.TryGetValue(clave, out var existente))
                {
                    acumulado[clave] = (
                        existente.Importe + imp.Importe,
                        existente.ImpLocal + imp.ImporteLocal,
                        existente.ImpRef + imp.ImporteReferencia
                    );
                }
                else
                {
                    acumulado[clave] = (imp.Importe, imp.ImporteLocal, imp.ImporteReferencia);
                }
            }

            var lineas = acumulado.Select(kv => new LineaAsientoParaGuardar(
                CuentaId: kv.Key.CuentaId,
                Tipo: kv.Key.Tipo,
                Importe: kv.Value.Importe,
                ImpLocal: kv.Value.ImpLocal,
                ImpReferencia: kv.Value.ImpRef,
                Detalle: modulo.ObtenerDetalleResumen(primerDia.Month, primerDia.Year)
            )).ToList();

            var primeraImputacion = imputaciones.First();
            var detalleResumen = modulo.ObtenerDetalleResumen(primerDia.Month, primerDia.Year);

            var asientoGnrId = await _repositorio.GuardarAsientoDeConsolidacion(new AsientoParaGuardar(
                Fecha: ultimoDia,
                FechaEmision: DateTime.Today,
                NumeraTipoId: dto.NumeraTipoId,
                EmpresaId: dto.EmpresaId,
                MonedaId: primeraImputacion.MonedaId,
                CotizacionLocal: 1m,
                CotizacionReferencia: 1m,
                ImpTotal: imputaciones.Sum(i => i.ImpTotal),
                Detalle: string.IsNullOrWhiteSpace(dto.Detalle) ? detalleResumen : dto.Detalle,
                Memo: dto.Observaciones,
                Lineas: lineas.AsReadOnly()
            ));

            await _repositorio.VincularResumenConComprobantes(asientoGnrId, dto.EmpresaId, primerDia, ultimoDia,
                modulo.ObtenerTiposComprobante());
            await _unidadDeTrabajo.GuardarCambios();

            return Resultado.Exito();
        }

        private async Task<IReadOnlyList<LineaAsientoParaGuardar>> ConstruirLineasPorComprobante(
            IReadOnlyList<ImputacionConsolidacionData> imputaciones,
            int empresaId,
            ModuloConsolidacion modulo)
        {
            var lineas = new List<LineaAsientoParaGuardar>();

            foreach (var imp in imputaciones)
            {
                var detalle = imp.DetalleImputacion ?? imp.DetalleComprobante;
                lineas.Add(new LineaAsientoParaGuardar(
                    CuentaId: imp.CuentaId,
                    Tipo: imp.NumeraTipoTipo,
                    Importe: imp.Importe,
                    ImpLocal: imp.ImporteLocal,
                    ImpReferencia: imp.ImporteReferencia,
                    Detalle: detalle
                ));
            }

            // Calcular diferencia de cambio para cobranzas y pagos con subcuenta de clientes/proveedores
            var primera = imputaciones.First();
            var esCobranzaOPago = modulo == ModuloConsolidacion.Cobranzas || modulo == ModuloConsolidacion.Pagos;
            var tieneSubcuenta = primera.SubcuentaTipoCuenta == (int)SubcuentaTipo.Clientes
                              || primera.SubcuentaTipoCuenta == (int)SubcuentaTipo.Proveedores;

            if (esCobranzaOPago && tieneSubcuenta && primera.MonedaId != 1)
            {
                var lineaDifCambio = await CalcularLineaDifCambio(primera, empresaId, modulo);
                if (lineaDifCambio is not null)
                    lineas.Add(lineaDifCambio);
            }

            return lineas.AsReadOnly();
        }

        private async Task<LineaAsientoParaGuardar?> CalcularLineaDifCambio(
            ImputacionConsolidacionData comprobante,
            int empresaId,
            ModuloConsolidacion modulo)
        {
            var aplicaciones = await _repositorio.ObtenerAplicacionesParaDifCambio(comprobante.ComprobanteId);
            if (!aplicaciones.Any())
                return null;

            var diferenciasLocal = 0m;
            var diferenciasRef = 0m;

            foreach (var app in aplicaciones)
            {
                diferenciasLocal += app.ImpAfectado * (comprobante.CotizacionLocal - app.CotizacionLocalOriginal);
                diferenciasRef += app.ImpAfectado * (comprobante.CotizacionReferencia - app.CotizacionReferenciaOriginal);
            }

            if (diferenciasLocal == 0 && diferenciasRef == 0)
                return null;

            var codigoParametro = modulo == ModuloConsolidacion.Cobranzas
                ? "CuentaIDDifCambioDeudores"
                : "CuentaIDDifCambioProveedores";

            var cuentaDifCambioId = await _repositorio.ObtenerCuentaIdParametro(codigoParametro, empresaId);
            if (cuentaDifCambioId is null)
                return null;

            // El tipo se invierte respecto a la línea de cta corriente
            var tipo = modulo == ModuloConsolidacion.Cobranzas ? 2 : 1;

            return new LineaAsientoParaGuardar(
                CuentaId: cuentaDifCambioId.Value,
                Tipo: tipo,
                Importe: diferenciasLocal,
                ImpLocal: diferenciasLocal,
                ImpReferencia: diferenciasRef,
                Detalle: "Dif. de cambio"
            );
        }

        private static string ConstruirDetallePorComprobante(ImputacionConsolidacionData primera)
        {
            var simbolo = primera.NumeraTipoSimbolo;
            var numero = primera.Numero.ToString("D8");
            var pv = primera.PuntoVenta.HasValue ? $"{primera.PuntoVenta:D4}-" : string.Empty;
            var agenda = primera.AgendaNombre is not null ? $" - {primera.AgendaNombre}" : string.Empty;
            return $"{simbolo} {pv}{numero}{agenda}";
        }

        private static List<ModuloConsolidacion>? ParsearModulos(ICollection<string> nombres)
        {
            var resultado = new List<ModuloConsolidacion>();
            foreach (var nombre in nombres)
            {
                if (!Enum.TryParse<ModuloConsolidacion>(nombre, ignoreCase: true, out var modulo))
                    return null;
                resultado.Add(modulo);
            }
            return resultado;
        }
    }
}
