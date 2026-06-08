using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Dominio.General;

namespace fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces
{
    public interface IIndiceContableServicio
    {
        Task<Resultado<int>> Crear(CrearIndiceDto dto);
        Task<Resultado> Actualizar(ActualizarIndiceDto dto);
        Task<Resultado> Eliminar(int id);
        Task<Resultado<ConsultarIndiceDto>> ObtenerPorId(int id);
    }
}
