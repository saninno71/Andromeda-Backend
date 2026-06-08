using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Dominio.General;

namespace fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces
{
    public interface IAsientoServicio
    {
        Task<Resultado<int>> CrearAsiento(CrearAsientoDto dto);
        Task<Resultado<ConsultarAsientoDto>> ObtenerPorId(int id);
        Task<Resultado> ActualizarAsiento(ActualizarAsientoDto dto);
        Task<Resultado> EliminarAsiento(int id);
    }
}
