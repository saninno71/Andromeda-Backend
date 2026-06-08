using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Infraestructura.ORM;

namespace fyd.backend.Infraestructura.Infraestructura
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly ContextoAplicacion _contexto;

        // Inyectamos el contexto de escritura principal
        public UnidadDeTrabajo(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> GuardarCambios()
        {
            // Aquí es donde EF Core dispara el "COMMIT" de la transacción SQL
            return await _contexto.SaveChangesAsync();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
