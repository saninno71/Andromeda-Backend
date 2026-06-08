using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class ImpositivoIVAProcedimientoAlmacenado : IImpositivoIVAProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<ImpositivoIVAProcedimientoAlmacenado> _log;

        public ImpositivoIVAProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<ImpositivoIVAProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbImpositivoIVAVentasDebitoFiscalReadModel> ObtenerCstctbImpositivoIVAVentasDebitoFiscal(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IVA - Ventas Débito Fiscal.");

            int grillaOrden = 0;
            var resultado = _contexto.CstctbImpositivoIVAVentasDebitoFiscales
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IVA] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoIVAGrupoReadModel> ObtenerCstctbImpositivoIVAComprasCreditoFiscal(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IVA - Compras Crédito Fiscal.");

            int grillaOrden = 1;
            var resultado = _contexto.CstctbImpositivoIVAGrupos
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IVA] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoIVAGrupoReadModel> ObtenerCstctbImpositivoIVAComprasCreditoFiscalConsolidado(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IVA - Compras Crédito Fiscal Consolidado.");

            int grillaOrden = 2;
            var resultado = _contexto.CstctbImpositivoIVAGrupos
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IVA] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoIVASinCreditoFiscalReadModel> ObtenerCstctbImpositivoIVASinCreditoFiscal(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IVA - Sin Crédito Fiscal.");

            int grillaOrden = 3;
            var resultado = _contexto.CstctbImpositivoIVASinCreditoFiscales
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IVA] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoIVARestitucionDebitoFiscalReadModel> ObtenerCstctbImpositivoIVARestitucionDebitoFiscal(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IVA - Restitución Débito Fiscal.");

            int grillaOrden = 4;
            var resultado = _contexto.CstctbImpositivoIVARestitucionDebitoFiscales
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IVA] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoIVARetencionReadModel> ObtenerCstctbImpositivoIVARetencion(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IVA - Retenciones.");

            int grillaOrden = 5;
            var resultado = _contexto.CstctbImpositivoIVARetenciones
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IVA] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoIVAPercepcionReadModel> ObtenerCstctbImpositivoIVAPercepcion(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IVA - Percepciones.");

            int grillaOrden = 6;
            var resultado = _contexto.CstctbImpositivoIVAPercepciones
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IVA] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoIVAComprasExteriorReadModel> ObtenerCstctbImpositivoIVAComprasExterior(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IVA - Compras Exterior.");

            int grillaOrden = 7;
            var resultado = _contexto.CstctbImpositivoIVAComprasExteriores
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IVA] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
