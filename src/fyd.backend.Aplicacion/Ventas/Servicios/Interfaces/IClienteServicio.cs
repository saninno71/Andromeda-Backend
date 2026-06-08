using fyd.backend.Aplicacion.Ventas.DTOs;
using fyd.backend.Dominio.General;
using System.Threading.Tasks;

namespace fyd.backend.Aplicacion.Ventas.Servicios.Interfaces
{
    public interface IClienteServicio
    {
        Task<Resultado<ConsultarClienteDto>> ObtenerPorId(int id);
        Task<Resultado<int>> Crear(CrearClienteDto dto);
        Task<Resultado> Actualizar(ActualizarClienteDto dto);
        Task<Resultado> Eliminar(int id);
    }
}
