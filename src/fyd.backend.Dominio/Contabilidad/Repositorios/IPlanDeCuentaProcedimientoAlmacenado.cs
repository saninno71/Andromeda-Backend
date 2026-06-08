using fyd.backend.Dominio.Contabilidad.Consultas;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IPlanDeCuentaProcedimientoAlmacenado
    {
        public IQueryable<CstctbCuentasReadModel> ObtenerCstctbCuentas(int? id, string? codigo, string? nombre, int? cuentaMadreId, int? asientoOk, int? subCuentaTipo, bool? ajustaOk, int? monedasTipo, int? empresaId);
    }
}
