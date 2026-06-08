using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.Contabilidad.Entidades;
using fyd.backend.Infraestructura.General.Entidades;
using fyd.backend.Infraestructura.ORM;
using fyd.backend.Infraestructura.Parametros.Entidades;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class ConsolidacionRepositorio : IConsolidacionRepositorio
    {
        private readonly ContextoAplicacion _contexto;

        public ConsolidacionRepositorio(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        // =====================================================
        // Verificaciones de estado
        // =====================================================

        public async Task<bool> ModuloYaGenerado(int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos)
        {
            var tiposArr = tipos.ToArray();
            return await (
                from ac in _contexto.AsientoComprobantes
                join comp in _contexto.ComprobantesContables on ac.ComprobanteId equals comp.Id
                join nt in _contexto.NumeraTipos on comp.NumeraTipoId equals nt.Id
                where comp.EmpresaId == empresaId
                    && comp.Fecha >= primerDia
                    && comp.Fecha <= ultimoDia
                    && tiposArr.Contains(nt.Tipo ?? 0)
                select ac.Id
            ).AnyAsync();
        }

        public async Task<bool> TieneComprobantes(int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos)
        {
            var tiposArr = tipos.ToArray();
            return await (
                from comp in _contexto.ComprobantesContables
                join nt in _contexto.NumeraTipos on comp.NumeraTipoId equals nt.Id
                where comp.EmpresaId == empresaId
                    && comp.Fecha >= primerDia
                    && comp.Fecha <= ultimoDia
                    && tiposArr.Contains(nt.Tipo ?? 0)
                select comp.Id
            ).AnyAsync();
        }

        // =====================================================
        // Obtener imputaciones para el proceso de consolidación
        // Migración de abmctbConsolidacion_cst
        // =====================================================

        public async Task<IReadOnlyList<ImputacionConsolidacionData>> ObtenerImputaciones(
            int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos)
        {
            var tiposArr = tipos.ToArray();

            var resultado = await (
                from comp in _contexto.ComprobantesContables
                join nt in _contexto.NumeraTipos on comp.NumeraTipoId equals nt.Id
                join imp in _contexto.Imputaciones on comp.Id equals imp.ComprobanteId
                from cuenta in _contexto.CuentasContables
                    .Where(c => c.Id == imp.CuentaId).DefaultIfEmpty()
                from num in _contexto.Numeraciones
                    .Where(n => n.Id == nt.NumeracionId).DefaultIfEmpty()
                from vtsComp in _contexto.VentaComprobantes
                    .Where(v => v.ComprobanteId == comp.Id).DefaultIfEmpty()
                from cliente in _contexto.Clientes
                    .Where(c => vtsComp != null && c.Id == vtsComp.ClienteId).DefaultIfEmpty()
                from agCliente in _contexto.Agendas
                    .Where(a => cliente != null && a.Id == cliente.AgendaId).DefaultIfEmpty()
                from cpsComp in _contexto.CompraComprobantes
                    .Where(c => c.ComprobanteId == comp.Id).DefaultIfEmpty()
                from proveedor in _contexto.Proveedores
                    .Where(p => cpsComp != null && cpsComp.ProveedorId.HasValue && p.Id == cpsComp.ProveedorId!.Value).DefaultIfEmpty()
                from agProveedor in _contexto.Agendas
                    .Where(a => proveedor != null && a.Id == proveedor.AgendaId).DefaultIfEmpty()
                where comp.EmpresaId == empresaId
                    && comp.Fecha >= primerDia
                    && comp.Fecha <= ultimoDia
                    && tiposArr.Contains(nt.Tipo ?? 0)
                orderby comp.Id
                select new
                {
                    comp.Id,
                    comp.Fecha,
                    comp.MonedaId,
                    comp.CotizacionLocal,
                    comp.CotizacionReferencia,
                    comp.ImpTotal,
                    CuentaId = imp.CuentaId ?? 0,
                    imp.Importe,
                    imp.ImporteLocal,
                    imp.ImporteReferencia,
                    DetalleComprobante = comp.Detalle,
                    DetalleImputacion = imp.Detalle,
                    NumeraTipoSimbolo = nt.Simbolo,
                    NumeraTipoTipo = nt.Tipo ?? 0,
                    PuntoVenta = cpsComp != null ? (int?)cpsComp.PuntoVenta : (num != null ? num.PuntoVenta : null),
                    comp.Numero,
                    AgendaNombre = agCliente != null ? agCliente.Nombre : (agProveedor != null ? agProveedor.Nombre : null),
                    SubcuentaTipoCuenta = cuenta != null ? (int)cuenta.SubcuentaTipo : 0
                }
            ).AsNoTracking().ToListAsync();

            return resultado.Select(r => new ImputacionConsolidacionData(
                ComprobanteId: r.Id,
                Fecha: r.Fecha,
                MonedaId: r.MonedaId,
                CotizacionLocal: r.CotizacionLocal,
                CotizacionReferencia: r.CotizacionReferencia,
                ImpTotal: r.ImpTotal,
                CuentaId: r.CuentaId,
                Importe: r.Importe,
                ImporteLocal: r.ImporteLocal,
                ImporteReferencia: r.ImporteReferencia,
                DetalleComprobante: r.DetalleComprobante,
                DetalleImputacion: r.DetalleImputacion,
                NumeraTipoSimbolo: r.NumeraTipoSimbolo,
                NumeraTipoTipo: r.NumeraTipoTipo,
                PuntoVenta: r.PuntoVenta,
                Numero: r.Numero,
                AgendaNombre: r.AgendaNombre,
                SubcuentaTipoCuenta: r.SubcuentaTipoCuenta
            )).ToList().AsReadOnly();
        }

        // =====================================================
        // Obtener asientos a eliminar
        // Migración de abmctbConsolidacion_Asientos con @EliminarOK=1
        // =====================================================

        public async Task<IReadOnlyList<int>> ObtenerAsientosParaEliminar(
            int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos)
        {
            var tiposArr = tipos.ToArray();
            return await (
                from ac in _contexto.AsientoComprobantes
                join comp in _contexto.ComprobantesContables on ac.ComprobanteId equals comp.Id
                join nt in _contexto.NumeraTipos on comp.NumeraTipoId equals nt.Id
                where comp.EmpresaId == empresaId
                    && comp.Fecha >= primerDia
                    && comp.Fecha <= ultimoDia
                    && tiposArr.Contains(nt.Tipo ?? 0)
                    && ac.AsientoId.HasValue
                select ac.AsientoId!.Value
            ).Distinct().ToListAsync();
        }

        // =====================================================
        // Guardar asiento generado por consolidación
        // Persiste gnrComprobantes + ctbAsientos y retorna el nuevo gnrComprobantes.Id
        // =====================================================

        public async Task<int> GuardarAsientoDeConsolidacion(AsientoParaGuardar parametros)
        {
            var infraComprobante = new Comprobante
            {
                Fecha = parametros.Fecha,
                FechaEmision = parametros.FechaEmision,
                NumeraTipoId = parametros.NumeraTipoId,
                Numero = 0,
                EmpresaId = parametros.EmpresaId,
                MonedaId = parametros.MonedaId,
                CotizacionLocal = parametros.CotizacionLocal,
                CotizacionReferencia = parametros.CotizacionReferencia,
                ImpTotal = parametros.ImpTotal,
                Detalle = parametros.Detalle,
                Memo = parametros.Memo
            };

            foreach (var linea in parametros.Lineas)
            {
                infraComprobante.Asientos.Add(new Asiento
                {
                    CuentaId = linea.CuentaId,
                    Tipo = linea.Tipo,
                    Importe = linea.Importe,
                    ImpLocal = linea.ImpLocal,
                    ImpReferencia = linea.ImpReferencia,
                    Detalle = linea.Detalle,
                    EliminarOk = false,
                    Comprobante = infraComprobante
                });
            }

            _contexto.ComprobantesContables.Add(infraComprobante);
            await _contexto.SaveChangesAsync();

            return infraComprobante.Id;
        }

        // =====================================================
        // Vincular comprobante con asiento generado (PorComprobante)
        // Migración de abmctbConsolidacion_Comprobantes con @ComprobanteID > 0
        // =====================================================

        public async Task VincularComprobanteConAsiento(int sourceComprobanteId, int asientoGnrId, int empresaId)
        {
            _contexto.AsientoComprobantes.Add(new AsientoComprobante
            {
                ComprobanteId = sourceComprobanteId,
                AsientoId = asientoGnrId
            });

            // Actualizar vtsComprobantes.AsientoId con el ctbAsientos.Id de la línea de Deudores
            var asientoDeudoresId = await ObtenerCtbAsientoIdDeCuentaParametro(asientoGnrId, "CuentaIDDeudores", empresaId);
            if (asientoDeudoresId.HasValue)
            {
                var ventaComp = await _contexto.VentaComprobantes
                    .FirstOrDefaultAsync(v => v.ComprobanteId == sourceComprobanteId);
                if (ventaComp is not null)
                    ventaComp.AsientoId = asientoDeudoresId;
            }

            // Actualizar cpsComprobantes.AsientoId con el ctbAsientos.Id de la línea de Proveedores
            var asientoProveedoresNacId = await ObtenerCtbAsientoIdDeCuentaParametro(asientoGnrId, "CuentaIDProveedoresNacionales", empresaId);
            var asientoProveedoresExtId = await ObtenerCtbAsientoIdDeCuentaParametro(asientoGnrId, "CuentaIDProveedoresExtranjeros", empresaId);
            var asientoProvId = asientoProveedoresNacId ?? asientoProveedoresExtId;
            if (asientoProvId.HasValue)
            {
                var compraComp = await _contexto.CompraComprobantes
                    .FirstOrDefaultAsync(c => c.ComprobanteId == sourceComprobanteId);
                if (compraComp is not null)
                    compraComp.AsientoId = asientoProvId;
            }
        }

        // =====================================================
        // Vincular todos los comprobantes del período con un asiento resumen
        // Migración de abmctbConsolidacion_Comprobantes con @ComprobanteID <= 0
        // =====================================================

        public async Task VincularResumenConComprobantes(
            int asientoGnrId, int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos)
        {
            var tiposArr = tipos.ToArray();
            var comprobanteIds = await (
                from comp in _contexto.ComprobantesContables
                join nt in _contexto.NumeraTipos on comp.NumeraTipoId equals nt.Id
                where comp.EmpresaId == empresaId
                    && comp.Fecha >= primerDia
                    && comp.Fecha <= ultimoDia
                    && tiposArr.Contains(nt.Tipo ?? 0)
                select comp.Id
            ).ToListAsync();

            foreach (var comprobanteId in comprobanteIds)
            {
                _contexto.AsientoComprobantes.Add(new AsientoComprobante
                {
                    ComprobanteId = comprobanteId,
                    AsientoId = asientoGnrId
                });
            }
        }

        // =====================================================
        // Desvincular comprobantes (eliminación)
        // Migración de abmctbConsolidacion_Comprobantes con @AsientoID = -1
        // =====================================================

        public async Task DesvincularComprobantes(
            int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos)
        {
            var tiposArr = tipos.ToArray();
            var comprobanteIds = await (
                from comp in _contexto.ComprobantesContables
                join nt in _contexto.NumeraTipos on comp.NumeraTipoId equals nt.Id
                where comp.EmpresaId == empresaId
                    && comp.Fecha >= primerDia
                    && comp.Fecha <= ultimoDia
                    && tiposArr.Contains(nt.Tipo ?? 0)
                select comp.Id
            ).ToListAsync();

            var vinculos = await _contexto.AsientoComprobantes
                .Where(ac => comprobanteIds.Contains(ac.ComprobanteId ?? 0))
                .ToListAsync();
            _contexto.AsientoComprobantes.RemoveRange(vinculos);

            var ventaComprobantes = await _contexto.VentaComprobantes
                .Where(vc => comprobanteIds.Contains(vc.ComprobanteId))
                .ToListAsync();
            foreach (var vc in ventaComprobantes)
                vc.AsientoId = null;

            var compraComprobantes = await _contexto.CompraComprobantes
                .Where(cc => comprobanteIds.Contains(cc.ComprobanteId))
                .ToListAsync();
            foreach (var cc in compraComprobantes)
                cc.AsientoId = null;
        }

        // =====================================================
        // Validar que todos los comprobantes tienen asiento
        // Migración de abmctbConsolidacion_ValidarAsientos
        // =====================================================

        public async Task<bool> ValidarAsientosCompletos(
            int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos)
        {
            var tiposArr = tipos.ToArray();
            var sinAsiento = await (
                from comp in _contexto.ComprobantesContables
                join nt in _contexto.NumeraTipos on comp.NumeraTipoId equals nt.Id
                from ac in _contexto.AsientoComprobantes
                    .Where(a => a.ComprobanteId == comp.Id).DefaultIfEmpty()
                where comp.EmpresaId == empresaId
                    && comp.Fecha >= primerDia
                    && comp.Fecha <= ultimoDia
                    && tiposArr.Contains(nt.Tipo ?? 0)
                    && ac == null
                select comp.Id
            ).AnyAsync();

            return !sinAsiento;
        }

        // =====================================================
        // Diferencia de cambio: obtener aplicaciones del comprobante
        // =====================================================

        public async Task<IReadOnlyList<AplicacionDifCambioData>> ObtenerAplicacionesParaDifCambio(int comprobanteId)
        {
            var ventaAplicaciones = await (
                from app in _contexto.Aplicaciones
                from ctaCte in _contexto.VentaCuentasCorrientes
                    .Where(v => v.Id == app.VentaCtaCteIdAfectado).DefaultIfEmpty()
                from ventaComp in _contexto.VentaComprobantes
                    .Where(vc => ctaCte != null && vc.Id == ctaCte.VentaId).DefaultIfEmpty()
                from compAfectado in _contexto.ComprobantesContables
                    .Where(c => ventaComp != null && c.Id == ventaComp.ComprobanteId).DefaultIfEmpty()
                where app.ComprobanteId == comprobanteId
                    && app.ImpAfectado.HasValue
                    && app.VentaCtaCteIdAfectado.HasValue
                    && compAfectado != null
                select new AplicacionDifCambioData(
                    compAfectado!.Id,
                    app.ImpAfectado!.Value,
                    compAfectado.CotizacionLocal,
                    compAfectado.CotizacionReferencia
                )
            ).AsNoTracking().ToListAsync();

            var compraAplicaciones = await (
                from app in _contexto.Aplicaciones
                from ctaCte in _contexto.CompraCuentasCorrientes
                    .Where(c => c.Id == app.CompraCtaCteIdAfectado).DefaultIfEmpty()
                from compraComp in _contexto.CompraComprobantes
                    .Where(cc => ctaCte != null && cc.Id == ctaCte.CompraId).DefaultIfEmpty()
                from compAfectado in _contexto.ComprobantesContables
                    .Where(c => compraComp != null && c.Id == compraComp.ComprobanteId).DefaultIfEmpty()
                where app.ComprobanteId == comprobanteId
                    && app.ImpAfectado.HasValue
                    && app.CompraCtaCteIdAfectado.HasValue
                    && compAfectado != null
                select new AplicacionDifCambioData(
                    compAfectado!.Id,
                    app.ImpAfectado!.Value,
                    compAfectado.CotizacionLocal,
                    compAfectado.CotizacionReferencia
                )
            ).AsNoTracking().ToListAsync();

            return ventaAplicaciones.Concat(compraAplicaciones).ToList().AsReadOnly();
        }

        // =====================================================
        // Obtener SubcuentaTipo de una cuenta contable
        // =====================================================

        public async Task<int> ObtenerSubcuentaTipoCuenta(int cuentaId)
        {
            return (int)(await _contexto.CuentasContables
                .Where(c => c.Id == cuentaId)
                .Select(c => c.SubcuentaTipo)
                .FirstOrDefaultAsync());
        }

        // =====================================================
        // Obtener CuentaId desde parámetros (prmVariosEmpresa)
        // =====================================================

        public async Task<int?> ObtenerCuentaIdParametro(string codigoVario, int empresaId)
        {
            var valor = await (
                from ve in _contexto.VariosEmpresas
                join v in _contexto.Varios on ve.VarioId equals v.Id
                where v.Codigo == codigoVario && ve.EmpresaId == empresaId
                select ve.Valor
            ).FirstOrDefaultAsync();

            return valor is not null && int.TryParse(valor, out var id) ? id : null;
        }

        // =====================================================
        // Eliminar asiento por gnrComprobantes.Id
        // =====================================================

        public async Task EliminarAsientoPorComprobanteId(int asientoGnrId)
        {
            var lineas = await _contexto.Asientos
                .Where(a => a.ComprobanteId == asientoGnrId)
                .ToListAsync();
            _contexto.Asientos.RemoveRange(lineas);

            var comprobante = await _contexto.ComprobantesContables
                .FirstOrDefaultAsync(c => c.Id == asientoGnrId);
            if (comprobante is not null)
                _contexto.ComprobantesContables.Remove(comprobante);
        }

        // =====================================================
        // Helper privado: busca el ctbAsientos.Id de la línea
        // cuya CuentaId coincide con el parámetro dado
        // =====================================================

        private async Task<int?> ObtenerCtbAsientoIdDeCuentaParametro(
            int asientoGnrId, string codigoVario, int empresaId)
        {
            var cuentaId = await ObtenerCuentaIdParametro(codigoVario, empresaId);
            if (cuentaId is null) return null;

            return await _contexto.Asientos
                .Where(a => a.ComprobanteId == asientoGnrId && a.CuentaId == cuentaId.Value)
                .Select(a => (int?)a.Id)
                .FirstOrDefaultAsync();
        }
    }
}
