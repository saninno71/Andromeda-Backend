using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class ImpositivoDecreto3685ProcedimientoAlmacenado : IImpositivoDecreto3685ProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<ImpositivoDecreto3685ProcedimientoAlmacenado> _log;

        public ImpositivoDecreto3685ProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<ImpositivoDecreto3685ProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbImpositivoDecreto3685CabeceraReadModel> ObtenerCstctbImpositivoDecreto3685Cabecera(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de Decreto 3685 - Cabecera.");

            int grillaOrden = 0;
            var resultado = _contexto.CstctbImpositivoDecreto3685Cabeceras
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_Decreto3685Titulo2] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoDecreto3685DetalleReadModel> ObtenerCstctbImpositivoDecreto3685Detalle(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de Decreto 3685 - Detalle.");

            int grillaOrden = 1;
            var resultado = _contexto.CstctbImpositivoDecreto3685Detalles
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_Decreto3685Titulo2] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }

        public IQueryable<CstctbImpositivoDecreto3685PercepcionReadModel> ObtenerCstctbImpositivoDecreto3685Percepcion(int empresaId, int fechaInicio, int fechaFin)
        {
            _log.LoguearInformacion("Ejecutando consulta de Decreto 3685 - Percepciones.");

            int grillaOrden = 2;
            var resultado = _contexto.CstctbImpositivoDecreto3685Percepciones
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbImpositivo_Decreto3685Titulo2] {grillaOrden}, {empresaId}, {fechaInicio}, {fechaFin}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
