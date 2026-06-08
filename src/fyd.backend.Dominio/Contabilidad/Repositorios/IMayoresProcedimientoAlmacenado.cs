using fyd.backend.Dominio.Contabilidad.Consultas;
using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IMayoresProcedimientoAlmacenado
    {
        IQueryable<CstctbMayoresReadModel> ObtenerCstctbMayores(
            int? id,
            int? cuentaId,
            string? empresaId,
            int? fechaDesde,
            int? fechaHasta,
            bool? arrastraSaldoOk,
            bool? incluyeImputacionesOk,
            int? fechaDesdeInicial);
    }
}
