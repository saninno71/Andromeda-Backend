using fyd.backend.Dominio.Contabilidad.Entidades;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IIndiceContableRepositorio
    {
        Task Agregar(IndiceContable indice);
        void Eliminar(IndiceContable indice);
        Task<IndiceContable?> ObtenerPorId(int id);
        Task<bool> ExistePeriodo(DateTime periodo, int? ignorarId = null);
    }
}
