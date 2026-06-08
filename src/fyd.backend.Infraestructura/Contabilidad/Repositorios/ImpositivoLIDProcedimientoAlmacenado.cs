using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class ImpositivoLIDProcedimientoAlmacenado : IImpositivoLIDProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<ImpositivoLIDProcedimientoAlmacenado> _log;

        public ImpositivoLIDProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<ImpositivoLIDProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbImpositivoLIDVentasCBTEReadModel> ObtenerCstctbImpositivoLIDVentasCBTE(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de LID - Ventas CBTE.");

            int grillaOrden = 0;
            var resultado = _contexto.CstctbImpositivoLIDVentasCBTEs
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_LID] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoLIDVentasAlicuotasReadModel> ObtenerCstctbImpositivoLIDVentasAlicuotas(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de LID - Ventas Alícuotas.");

            int grillaOrden = 1;
            var resultado = _contexto.CstctbImpositivoLIDVentasAlicuotas
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_LID] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoLIDComprasCBTEReadModel> ObtenerCstctbImpositivoLIDComprasCBTE(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de LID - Compras CBTE.");

            int grillaOrden = 2;
            var resultado = _contexto.CstctbImpositivoLIDComprasCBTEs
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_LID] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoLIDComprasAlicuotasReadModel> ObtenerCstctbImpositivoLIDComprasAlicuotas(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de LID - Compras Alícuotas.");

            int grillaOrden = 3;
            var resultado = _contexto.CstctbImpositivoLIDComprasAlicuotas
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_LID] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoLIDComprasCBTEReadModel> ObtenerCstctbImpositivoLIDComprasImportacion(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de LID - Compras Importación.");

            int grillaOrden = 4;
            var resultado = _contexto.CstctbImpositivoLIDComprasCBTEs
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_LID] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoLIDComprasImportacionAlicuotasReadModel> ObtenerCstctbImpositivoLIDComprasImportacionAlicuotas(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de LID - Compras Importación Alícuotas.");

            int grillaOrden = 5;
            var resultado = _contexto.CstctbImpositivoLIDComprasImportacionAlicuotas
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_LID] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
