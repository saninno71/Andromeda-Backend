using fyd.backend.Aplicacion.Compras.DTOs;
using fyd.backend.Dominio.General;
using System.Threading.Tasks;

namespace fyd.backend.Aplicacion.Compras.Servicios.Interfaces
{
    public interface IProveedorServicio
    {
        Task<Resultado<int>> Crear(CrearProveedorDto dto);
        Task<Resultado<ConsultarProveedorDto>> ObtenerPorId(int id);
        Task<Resultado> Actualizar(ActualizarProveedorDto dto);
        Task<Resultado> Eliminar(int id);
        Task<Resultado<bool>> TieneMovimientosEnCc(int id);
    }
}
