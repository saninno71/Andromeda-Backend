using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class MayoresProcedimientoAlmacenado : IMayoresProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<MayoresProcedimientoAlmacenado> _log;

        public MayoresProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<MayoresProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbMayoresReadModel> ObtenerCstctbMayores(
            int? id,
            int? cuentaId,
            string? empresaId,
            int? fechaDesde,
            int? fechaHasta,
            bool? arrastraSaldoOk,
            bool? incluyeImputacionesOk,
            int? fechaDesdeInicial)
        {
            _log.LoguearInformacion("Ejecutando consulta de Mayores Contables mediante procedimiento almacenado.");

            // Ejecución del SP con interpolación de parámetros
            var resultado = _contexto.CstctbMayores
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbMayores] 
                    {id}, 
                    {cuentaId}, 
                    {empresaId}, 
                    {fechaDesde}, 
                    {fechaHasta}, 
                    {arrastraSaldoOk}, 
                    {incluyeImputacionesOk}, 
                    {fechaDesdeInicial}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
