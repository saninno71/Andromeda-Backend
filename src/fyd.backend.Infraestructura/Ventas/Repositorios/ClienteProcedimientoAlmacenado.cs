using fyd.backend.Dominio.Ventas.Consultas;
using fyd.backend.Dominio.Ventas.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace fyd.backend.Infraestructura.Ventas.Repositorios
{
    public class ClienteProcedimientoAlmacenado : IClienteProcedimientoAlmacenado
    {
        private readonly ContextoLectura _contexto;

        public ClienteProcedimientoAlmacenado(ContextoLectura contexto)
        {
            _contexto = contexto;
        }

        public IQueryable<CstvtsClientesReadModel> ObtenerCstvtsClientes(
            int? id = null,
            int? codigo = null,
            string? nombre = null,
            string? numeroDoc = null,
            string? calle = null,
            string? barrio = null,
            int? localidadId = null,
            int? provinciaId = null,
            int? paisId = null,
            int? ivaCategoriaId = null,
            int? clienteCcId = null,
            int? vendedorId = null,
            int? cobradorId = null,
            int? condPagoId = null,
            int? listaPrecioId = null,
            int? cuentaId = null,
            int? transporteId = null,
            int? depositoId = null,
            int? situacion = null,
            int? categoriaId = null,
            int? fechaAltaDesde = null,
            int? fechaBajaDesde = null,
            int? fechaAltaHasta = null,
            int? fechaBajaHasta = null,
            int? zonaId = null,
            int? lineaId = null,
            int? calificacionId = null,
            string? telefono = null,
            string? variables = null,
            string? lineas = null,
            string? agendaEmpresaId = null,
            string? cuit = null,
            int? clienteEventual = null,
            bool consultaCompletaOk = true)
        {

            var resultado = _contexto.CstvtsClientesReadModel
                .FromSqlInterpolated($@"EXECUTE [dbo].[cstvtsClientes]
                    {id}, {codigo}, {nombre}, {numeroDoc}, {calle}, {barrio},
                    {localidadId}, {provinciaId}, {paisId}, {ivaCategoriaId}, {clienteCcId},
                    {vendedorId}, {cobradorId}, {condPagoId}, {listaPrecioId}, {cuentaId},
                    {transporteId}, {depositoId}, {situacion}, {categoriaId},
                    {fechaAltaDesde}, {fechaBajaDesde}, {fechaAltaHasta}, {fechaBajaHasta},
                    {zonaId}, {lineaId}, {calificacionId}, {telefono}, {variables}, {lineas},
                    {agendaEmpresaId}, {cuit}, {clienteEventual}, {consultaCompletaOk}")
                .AsNoTracking()
                .ToList();

            return resultado.AsQueryable();
        }
    }
}
