using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Dominio.General;

namespace fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces
{
    public interface IConsolidacionServicio
    {
        Task<Resultado<IReadOnlyList<ModuloEstadoDto>>> VerificarEstado(VerificarEstadoDto dto);

        Task<Resultado> Generar(GenerarConsolidacionDto dto);

        Task<Resultado> Eliminar(EliminarConsolidacionDto dto);
    }
}
