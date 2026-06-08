using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class PlanDeCuentaProcedimientoAlmacenado : IPlanDeCuentaProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<PlanDeCuentaProcedimientoAlmacenado> _log;

        public PlanDeCuentaProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<PlanDeCuentaProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }
        public IQueryable<CstctbCuentasReadModel> ObtenerCstctbCuentas(int? id, string? codigo, string? nombre, int? cuentaMadreId, int? asientoOk, int? subCuentaTipo, bool? ajustaOk, int? monedasTipo, int? empresaId)
        {
            _log.LoguearInformacion("Haciendo un log desde la capa de infraestructura");
            var resultado = _contexto.CstctbCuentas.FromSqlInterpolated($"EXECUTE [dbo].[cstctbCuentas] {id}, {codigo}, {nombre},{cuentaMadreId},{asientoOk},{subCuentaTipo},{ajustaOk},{monedasTipo},{empresaId}")
                            .AsNoTracking()
                            .ToList();

            return resultado.AsQueryable();
        }
    }
}
