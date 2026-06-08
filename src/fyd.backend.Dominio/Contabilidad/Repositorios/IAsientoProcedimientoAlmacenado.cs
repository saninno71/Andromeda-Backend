using fyd.backend.Dominio.Contabilidad.Consultas;
using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IAsientoProcedimientoAlmacenado
    {
        public IQueryable<CstctbAsientosReadModel> ObtenerCstctbAsientos(
            int? id,
            int? fechaDesde,
            int? fechaHasta,
            string? detalle,
            int? cuentaId,
            int? numeroDesde,
            int? numeroHasta,
            int? subcuentaClienteId,
            int? subcuentaProveedorId,
            int? subcuentaCajaId,
            string? empresaId,
            int? numeraTipoId);
    }
}
