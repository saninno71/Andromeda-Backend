using fyd.backend.Dominio.Contabilidad.Consultas;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IImpositivoLIDProcedimientoAlmacenado
    {
        IQueryable<CstctbImpositivoLIDVentasCBTEReadModel> ObtenerCstctbImpositivoLIDVentasCBTE(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoLIDVentasAlicuotasReadModel> ObtenerCstctbImpositivoLIDVentasAlicuotas(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoLIDComprasCBTEReadModel> ObtenerCstctbImpositivoLIDComprasCBTE(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoLIDComprasAlicuotasReadModel> ObtenerCstctbImpositivoLIDComprasAlicuotas(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoLIDComprasCBTEReadModel> ObtenerCstctbImpositivoLIDComprasImportacion(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoLIDComprasImportacionAlicuotasReadModel> ObtenerCstctbImpositivoLIDComprasImportacionAlicuotas(int empresaId, int fechaInicio, int fechaFin);
    }
}
