using fyd.backend.Dominio.Contabilidad.Consultas;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface ICstctbIndicesProcedimientoAlmacenado
    {
        IQueryable<CstctbIndicesReadModel> ObtenerCstctbIndices(int? id, int? periodoDesde, int? periodoHasta);
    }
}
