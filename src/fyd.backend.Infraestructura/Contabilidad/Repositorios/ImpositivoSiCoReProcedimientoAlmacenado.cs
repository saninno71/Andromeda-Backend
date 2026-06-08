using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class ImpositivoSiCoReProcedimientoAlmacenado : IImpositivoSiCoReProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<ImpositivoSiCoReProcedimientoAlmacenado> _log;

        public ImpositivoSiCoReProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<ImpositivoSiCoReProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbImpositivoSiCoReRetPercReadModel> ObtenerCstctbImpositivoSiCoReRetPerc(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de SiCoRe - Retenciones y Percepciones.");

            int grillaOrden = 0;
            var resultado = _contexto.CstctbImpositivoSiCoReRetPercs
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_SiCoRe] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoSiCoReSujetosRetenidosReadModel> ObtenerCstctbImpositivoSiCoReSujetosRetenidos(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de SiCoRe - Sujetos Retenidos.");

            int grillaOrden = 1;
            var resultado = _contexto.CstctbImpositivoSiCoReSujetosRetenidos
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_SiCoRe] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
