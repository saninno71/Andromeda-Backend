using fyd.backend.Dominio.Compras.Consultas;
using System.Linq;

namespace fyd.backend.Dominio.Compras.Repositorios
{
    public interface IProveedoresProcedimientoAlmacenado
    {
        IQueryable<CstProveedoresReadModel> ObtenerDatos(
            int? id, int? codigo, string? nombre, string? nombre2, string? numeroDoc, string? empresaId, string? calle, string? barrio,
            int? localidadId, int? provinciaId, int? paisId, string? telefono, string? variables, int? ivaCategoriaId, int? cuentaId,
            int? proveedorCcId, int? condPagoId, int? origen, int? categoriaId, int? calificacionId, int? fechaAltaDesde, int? fechaBajaDesde,
            int? fechaAltaHasta, int? fechaBajaHasta, int? situacion, int? contactoId, int? lineaId, int? proveedorEventual, int? ivaGrupoId);
    }
}
