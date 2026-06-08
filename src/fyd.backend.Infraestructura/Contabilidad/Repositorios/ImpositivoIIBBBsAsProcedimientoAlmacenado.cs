using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class ImpositivoIIBBBsAsProcedimientoAlmacenado : IImpositivoIIBBBsAsProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<ImpositivoIIBBBsAsProcedimientoAlmacenado> _log;

        public ImpositivoIIBBBsAsProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<ImpositivoIIBBBsAsProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbImpositivoIIBBBsAsRetencionReadModel> ObtenerCstctbImpositivoIIBBBsAsRetencion(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IIBB BsAs - Retenciones.");

            int grillaOrden = 1;
            var resultado = _contexto.CstctbImpositivoIIBBBsAsRetenciones
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IIBB_BsAs] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoIIBBBsAsPercepcionReadModel> ObtenerCstctbImpositivoIIBBBsAsPercepcion(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IIBB BsAs - Percepciones.");

            int grillaOrden = 2;
            var resultado = _contexto.CstctbImpositivoIIBBBsAsPercepciones
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IIBB_BsAs] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
