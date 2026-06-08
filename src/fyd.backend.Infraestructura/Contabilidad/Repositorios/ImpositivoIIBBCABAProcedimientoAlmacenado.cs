using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class ImpositivoIIBBCABAProcedimientoAlmacenado : IImpositivoIIBBCABAProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<ImpositivoIIBBCABAProcedimientoAlmacenado> _log;

        public ImpositivoIIBBCABAProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<ImpositivoIIBBCABAProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbImpositivoIIBBCABARetPercReadModel> ObtenerCstctbImpositivoIIBBCABARetPerc(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IIBB CABA - Retenciones y Percepciones.");

            int grillaOrden = 1;
            var resultado = _contexto.CstctbImpositivoIIBBCABARetPercs
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IIBB_CABA] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoIIBBCABANotaCreditoReadModel> ObtenerCstctbImpositivoIIBBCABANotaCredito(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de IIBB CABA - Notas de Crédito.");

            int grillaOrden = 2;
            var resultado = _contexto.CstctbImpositivoIIBBCABANotasCredito
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_IIBB_CABA] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
