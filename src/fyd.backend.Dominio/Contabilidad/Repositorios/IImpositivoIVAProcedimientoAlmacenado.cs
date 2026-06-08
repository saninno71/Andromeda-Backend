using fyd.backend.Dominio.Contabilidad.Consultas;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IImpositivoIVAProcedimientoAlmacenado
    {
        IQueryable<CstctbImpositivoIVAVentasDebitoFiscalReadModel> ObtenerCstctbImpositivoIVAVentasDebitoFiscal(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoIVAGrupoReadModel> ObtenerCstctbImpositivoIVAComprasCreditoFiscal(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoIVAGrupoReadModel> ObtenerCstctbImpositivoIVAComprasCreditoFiscalConsolidado(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoIVASinCreditoFiscalReadModel> ObtenerCstctbImpositivoIVASinCreditoFiscal(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoIVARestitucionDebitoFiscalReadModel> ObtenerCstctbImpositivoIVARestitucionDebitoFiscal(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoIVARetencionReadModel> ObtenerCstctbImpositivoIVARetencion(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoIVAPercepcionReadModel> ObtenerCstctbImpositivoIVAPercepcion(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoIVAComprasExteriorReadModel> ObtenerCstctbImpositivoIVAComprasExterior(int empresaId, int fechaInicio, int fechaFin);
    }
}
