using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.Contabilidad.Mapeos;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class IndiceContableRepositorio : IIndiceContableRepositorio
    {
        private readonly ContextoAplicacion _contextoAplicacion;

        public IndiceContableRepositorio(ContextoAplicacion contextoAplicacion)
        {
            _contextoAplicacion = contextoAplicacion;
        }

        public async Task<IndiceContable?> ObtenerPorId(int id)
        {
            var entidad = await _contextoAplicacion.IndicesContables
                .FirstOrDefaultAsync(i => i.Id == id);

            if (entidad == null) return null;

            return IndiceContableMapeo.ADominio(entidad);
        }

        public async Task Agregar(IndiceContable indice)
        {
            var entidad = IndiceContableMapeo.AInfraestructura(indice);
            await _contextoAplicacion.IndicesContables.AddAsync(entidad);
        }

        public void Eliminar(IndiceContable indice)
        {
            var entidadTracked = _contextoAplicacion.IndicesContables.Local
                .FirstOrDefault(i => i.Id == indice.Id);

            if (entidadTracked != null)
            {
                _contextoAplicacion.IndicesContables.Remove(entidadTracked);
            }
            else
            {
                var entidad = IndiceContableMapeo.AInfraestructura(indice);
                _contextoAplicacion.IndicesContables.Remove(entidad);
            }
        }

        public async Task<bool> ExistePeriodo(DateTime periodo, int? ignorarId = null)
        {
            var periodoNormalizado = new DateTime(periodo.Year, periodo.Month, 1);

            return await _contextoAplicacion.IndicesContables
                .AnyAsync(i => i.Periodo == periodoNormalizado && (ignorarId == null || i.Id != ignorarId.Value));
        }
    }
}
