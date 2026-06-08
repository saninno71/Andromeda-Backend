using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Dominio.General;

namespace fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces
{
    public interface IPlanDeCuentaServicio
    {
        Task<Resultado<ConsultarCuentaDto>> ObtenerPorId(int id);
        Task<Resultado<int>> CrearCuenta(CrearCuentaDto dto);
        Task<Resultado> ActualizarCuenta(ActualizarCuentaDto dto);
        Task<Resultado> EliminarCuenta(int id);
    }
}
