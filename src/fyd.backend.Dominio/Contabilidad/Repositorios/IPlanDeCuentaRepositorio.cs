using fyd.backend.Dominio.Contabilidad.Entidades;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IPlanDeCuentaRepositorio
    {
        // Básicos
        Task Agregar(CuentaContable cuenta);
        Task Actualizar(CuentaContable cuenta);
        void Eliminar(CuentaContable cuenta);

        Task<CuentaContable?> ObtenerPorId(int id);
        // Validaciones de OT
        Task<bool> ExisteCodigo(string codigo, int? ignorarId = null);

        Task<bool> TieneMovimientos(int cuentaId);

        Task<bool> TieneCuentasHijas(int cuentaId);

        Task<bool> EsReferenciaCircular(int posibleCuentaHijaId, int posibleCuentaMadreId);

        Task<bool> SuperaMaximoNivelPermitido(int cuentaMadreId);

        Task<ICollection<Grupo>> ObtenerGruposPorIds(IEnumerable<int> gruposIds);
    }
}
