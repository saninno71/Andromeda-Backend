using fyd.backend.Dominio.Contabilidad.Consultas;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IImpositivoSiCoReProcedimientoAlmacenado
    {
        IQueryable<CstctbImpositivoSiCoReRetPercReadModel> ObtenerCstctbImpositivoSiCoReRetPerc(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoSiCoReSujetosRetenidosReadModel> ObtenerCstctbImpositivoSiCoReSujetosRetenidos(int empresaId, int fechaInicio, int fechaFin);
    }
}
