using fyd.backend.Dominio.Contabilidad.Consultas;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IImpositivoSiFeReProcedimientoAlmacenado
    {
        IQueryable<CstctbImpositivoSiFeReFacturacionJurisdiccionReadModel> ObtenerCstctbImpositivoSiFeReFacturacionJurisdiccion(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoSiFeReFacturacionTipoImporteReadModel> ObtenerCstctbImpositivoSiFeReFacturacionTipoImporte(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoSiFeReRetencionReadModel> ObtenerCstctbImpositivoSiFeReRetencion(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoSiFeRePercepcionReadModel> ObtenerCstctbImpositivoSiFeRePercepcion(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoSiFeRePercepcionAduaneraReadModel> ObtenerCstctbImpositivoSiFeRePercepcionAduanera(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoSiFeReRecaudacionBancariaReadModel> ObtenerCstctbImpositivoSiFeReRecaudacionBancaria(int empresaId, int fechaInicio, int fechaFin);
    }
}
