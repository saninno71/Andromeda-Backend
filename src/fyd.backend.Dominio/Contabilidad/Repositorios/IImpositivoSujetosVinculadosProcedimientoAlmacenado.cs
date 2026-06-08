using fyd.backend.Dominio.Contabilidad.Consultas;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IImpositivoSujetosVinculadosProcedimientoAlmacenado
    {
        IQueryable<CstctbImpositivoSujetosVinculadosCBTEReadModel> ObtenerCstctbImpositivoSujetosVinculadosCBTE(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoSujetosVinculadosAlicuotasReadModel> ObtenerCstctbImpositivoSujetosVinculadosAlicuotas(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoSujetosVinculadosOperacionesReadModel> ObtenerCstctbImpositivoSujetosVinculadosOperaciones(int empresaId, int fechaInicio, int fechaFin);
    }
}
