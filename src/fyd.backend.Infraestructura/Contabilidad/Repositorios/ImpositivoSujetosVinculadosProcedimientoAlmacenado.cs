using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class ImpositivoSujetosVinculadosProcedimientoAlmacenado : IImpositivoSujetosVinculadosProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<ImpositivoSujetosVinculadosProcedimientoAlmacenado> _log;

        public ImpositivoSujetosVinculadosProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<ImpositivoSujetosVinculadosProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbImpositivoSujetosVinculadosCBTEReadModel> ObtenerCstctbImpositivoSujetosVinculadosCBTE(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de Sujetos Vinculados - Comprobantes.");

            int grillaOrden = 0;
            var resultado = _contexto.CstctbImpositivoSujetosVinculadosCBTEs
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_SujetosVinculados] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoSujetosVinculadosAlicuotasReadModel> ObtenerCstctbImpositivoSujetosVinculadosAlicuotas(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de Sujetos Vinculados - Alícuotas.");

            int grillaOrden = 1;
            var resultado = _contexto.CstctbImpositivoSujetosVinculadosAlicuotas
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_SujetosVinculados] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoSujetosVinculadosOperacionesReadModel> ObtenerCstctbImpositivoSujetosVinculadosOperaciones(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de Sujetos Vinculados - Operaciones.");

            int grillaOrden = 2;
            var resultado = _contexto.CstctbImpositivoSujetosVinculadosOperaciones
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_SujetosVinculados] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
