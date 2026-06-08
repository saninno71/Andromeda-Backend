using fyd.backend.Dominio.Contabilidad.Consultas;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IImpositivoIIBBCABAProcedimientoAlmacenado
    {
        IQueryable<CstctbImpositivoIIBBCABARetPercReadModel> ObtenerCstctbImpositivoIIBBCABARetPerc(int empresaId, int fechaInicio, int fechaFin);
        IQueryable<CstctbImpositivoIIBBCABANotaCreditoReadModel> ObtenerCstctbImpositivoIIBBCABANotaCredito(int empresaId, int fechaInicio, int fechaFin);
    }
}
