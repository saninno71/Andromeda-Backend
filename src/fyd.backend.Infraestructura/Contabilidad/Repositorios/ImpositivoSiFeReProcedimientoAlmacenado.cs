using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class ImpositivoSiFeReProcedimientoAlmacenado : IImpositivoSiFeReProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<ImpositivoSiFeReProcedimientoAlmacenado> _log;

        public ImpositivoSiFeReProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<ImpositivoSiFeReProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbImpositivoSiFeReFacturacionJurisdiccionReadModel> ObtenerCstctbImpositivoSiFeReFacturacionJurisdiccion(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de SiFeRe - Facturación por Jurisdicción.");

            int grillaOrden = 0;
            var resultado = _contexto.CstctbImpositivoSiFeReFacturacionJurisdicciones
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_SiFeRe] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoSiFeReFacturacionTipoImporteReadModel> ObtenerCstctbImpositivoSiFeReFacturacionTipoImporte(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de SiFeRe - Facturación por Tipo de Importe.");

            int grillaOrden = 1;
            var resultado = _contexto.CstctbImpositivoSiFeReFacturacionTipoImportes
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_SiFeRe] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoSiFeReRetencionReadModel> ObtenerCstctbImpositivoSiFeReRetencion(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de SiFeRe - Retenciones.");

            int grillaOrden = 2;
            var resultado = _contexto.CstctbImpositivoSiFeReRetenciones
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_SiFeRe] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoSiFeRePercepcionReadModel> ObtenerCstctbImpositivoSiFeRePercepcion(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de SiFeRe - Percepciones.");

            int grillaOrden = 3;
            var resultado = _contexto.CstctbImpositivoSiFeRePercepciones
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_SiFeRe] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoSiFeRePercepcionAduaneraReadModel> ObtenerCstctbImpositivoSiFeRePercepcionAduanera(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de SiFeRe - Percepciones Aduaneras.");

            int grillaOrden = 4;
            var resultado = _contexto.CstctbImpositivoSiFeRePercepcionesAduaneras
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_SiFeRe] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoSiFeReRecaudacionBancariaReadModel> ObtenerCstctbImpositivoSiFeReRecaudacionBancaria(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de SiFeRe - Recaudación Bancaria.");

            int grillaOrden = 5;
            var resultado = _contexto.CstctbImpositivoSiFeReRecaudacionesBancarias
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_SiFeRe] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
