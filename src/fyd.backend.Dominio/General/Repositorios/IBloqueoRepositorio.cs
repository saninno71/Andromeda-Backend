namespace fyd.backend.Dominio.General.Repositorios
{
    public interface IBloqueoRepositorio
    {
        Task<bool> BloqueoExiste(string tabla, int periodoId);

        Task AgregarBloqueo(string tabla, int periodoId);

        Task EliminarBloqueo(string tabla, int periodoId);
    }
}
