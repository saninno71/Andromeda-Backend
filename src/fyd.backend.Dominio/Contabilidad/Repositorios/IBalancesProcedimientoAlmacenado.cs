using fyd.backend.Dominio.Contabilidad.Consultas;
using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IBalancesProcedimientoAlmacenado
    {
        IQueryable<CstctbBalancesReadModel> ObtenerCstctbBalances(
            int? id,
            int? fechaDesde,
            int? fechaHasta,
            string? empresaId,
            bool? incluyeImputacionesOk);
    }
}
