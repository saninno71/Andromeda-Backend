using fyd.backend.Dominio.Contabilidad.Consultas;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IImpositivoIIBBBsAsProcedimientoAlmacenado
    {
        IQueryable<CstctbImpositivoIIBBBsAsRetencionReadModel> ObtenerCstctbImpositivoIIBBBsAsRetencion(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoIIBBBsAsPercepcionReadModel> ObtenerCstctbImpositivoIIBBBsAsPercepcion(int empresaId, int fechaInicio, int fechaFin);
    }
}
