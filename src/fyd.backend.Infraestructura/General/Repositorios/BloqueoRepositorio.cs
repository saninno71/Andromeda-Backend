using fyd.backend.Dominio.General.Repositorios;
using fyd.backend.Infraestructura.General.Entidades;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.General.Repositorios
{
    public class BloqueoRepositorio : IBloqueoRepositorio
    {
        private readonly ContextoAplicacion _contexto;

        public BloqueoRepositorio(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> BloqueoExiste(string tabla, int periodoId)
        {
            return await _contexto.Bloqueos
                .AnyAsync(b => b.Tabla == tabla && b.PeriodoId == periodoId);
        }

        public async Task AgregarBloqueo(string tabla, int periodoId)
        {
            _contexto.Bloqueos.Add(new Bloqueo
            {
                Tabla = tabla,
                PeriodoId = periodoId
            });
            await _contexto.SaveChangesAsync();
        }

        public async Task EliminarBloqueo(string tabla, int periodoId)
        {
            var bloqueo = await _contexto.Bloqueos
                .FirstOrDefaultAsync(b => b.Tabla == tabla && b.PeriodoId == periodoId);

            if (bloqueo is not null)
            {
                _contexto.Bloqueos.Remove(bloqueo);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}
