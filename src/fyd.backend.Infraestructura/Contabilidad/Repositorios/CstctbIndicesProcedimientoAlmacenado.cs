using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class CstctbIndicesProcedimientoAlmacenado : ICstctbIndicesProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;

        public CstctbIndicesProcedimientoAlmacenado(ContextoLectura contexto)
        {
            _contexto = contexto;
        }

        public IQueryable<CstctbIndicesReadModel> ObtenerCstctbIndices(int? id, int? periodoDesde, int? periodoHasta)
        {
            var resultado = _contexto.CstctbIndices
                .FromSqlInterpolated($@"EXEC [dbo].[cstctbIndices] {id}, {periodoDesde}, {periodoHasta}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
