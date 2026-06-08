using fyd.backend.Dominio.Contabilidad.Consultas;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IImpositivoDecreto3685ProcedimientoAlmacenado
    {
        IQueryable<CstctbImpositivoDecreto3685CabeceraReadModel> ObtenerCstctbImpositivoDecreto3685Cabecera(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoDecreto3685DetalleReadModel> ObtenerCstctbImpositivoDecreto3685Detalle(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoDecreto3685PercepcionReadModel> ObtenerCstctbImpositivoDecreto3685Percepcion(int empresaId, int fechaInicio, int fechaFin);
    }
}
