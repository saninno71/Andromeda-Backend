using fyd.backend.Dominio.Compras.Consultas;
using fyd.backend.Dominio.Compras.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace fyd.backend.Infraestructura.Compras.Repositorios
{
    public class ProveedoresProcedimientoAlmacenado : IProveedoresProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;

        public ProveedoresProcedimientoAlmacenado(ContextoLectura contexto)
        {
            _contexto = contexto;
        }

        public IQueryable<CstProveedoresReadModel> ObtenerDatos(
            int? id, int? codigo, string? nombre, string? nombre2, string? numeroDoc, string? empresaId, string? calle, string? barrio,
            int? localidadId, int? provinciaId, int? paisId, string? telefono, string? variables, int? ivaCategoriaId, int? cuentaId,
            int? proveedorCcId, int? condPagoId, int? origen, int? categoriaId, int? calificacionId, int? fechaAltaDesde, int? fechaBajaDesde,
            int? fechaAltaHasta, int? fechaBajaHasta, int? situacion, int? contactoId, int? lineaId, int? proveedorEventual, int? ivaGrupoId)
        {
            var resultado = _contexto.CstProveedoresReadModel
                .FromSqlInterpolated($"EXECUTE [dbo].[cstcpsProveedores] {id}, {codigo}, {nombre}, {nombre2}, {numeroDoc}, {empresaId}, {calle}, {barrio}, {localidadId}, {provinciaId}, {paisId}, {telefono}, {variables}, {ivaCategoriaId}, {cuentaId}, {proveedorCcId}, {condPagoId}, {origen}, {categoriaId}, {calificacionId}, {fechaAltaDesde}, {fechaBajaDesde}, {fechaAltaHasta}, {fechaBajaHasta}, {situacion}, {contactoId}, {lineaId}, {proveedorEventual}, {ivaGrupoId}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
