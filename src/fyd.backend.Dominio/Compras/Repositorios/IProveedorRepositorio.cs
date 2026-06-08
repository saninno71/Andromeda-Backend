using fyd.backend.Dominio.Compras.Entidades;

namespace fyd.backend.Dominio.Compras.Repositorios
{
    public interface IProveedorRepositorio
    {
        Task Agregar(Proveedor entidad);
        void Actualizar(Proveedor entidad);
        void Eliminar(Proveedor entidad);
        Task<Proveedor?> ObtenerPorId(int id);
        Task<bool> ExisteCodigo(int codigo);
        Task<int> ObtenerMaximoCodigo();
        Task<bool> EnUso(int id);
        Task<bool> TieneMovimientosEnCc(int id);
    }
}
