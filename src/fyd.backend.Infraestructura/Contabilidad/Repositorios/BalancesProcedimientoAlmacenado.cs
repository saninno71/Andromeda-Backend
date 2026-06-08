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
    public class BalancesProcedimientoAlmacenado : IBalancesProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;
        private readonly IServicioLogueo<BalancesProcedimientoAlmacenado> _log;

        public BalancesProcedimientoAlmacenado(ContextoLectura contexto, IServicioLogueo<BalancesProcedimientoAlmacenado> log)
        {
            _contexto = contexto;
            _log = log;
        }

        public IQueryable<CstctbBalancesReadModel> ObtenerCstctbBalances(
            int? id,
            int? fechaDesde,
            int? fechaHasta,
            string? empresaId,
            bool? incluyeImputacionesOk)
        {
            _log.LoguearInformacion("Ejecutando consulta de Balances Contables.");

            var resultado = _contexto.CstctbBalances
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstctbBalances] 
                    {id}, 
                    {fechaDesde}, 
                    {fechaHasta}, 
                    {empresaId}, 
                    {incluyeImputacionesOk}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
