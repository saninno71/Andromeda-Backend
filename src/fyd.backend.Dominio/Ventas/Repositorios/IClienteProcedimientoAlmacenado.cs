using fyd.backend.Dominio.Ventas.Consultas;
using System.Linq;

namespace fyd.backend.Dominio.Ventas.Repositorios
{
    public interface IClienteProcedimientoAlmacenado
    {
        IQueryable<CstvtsClientesReadModel> ObtenerCstvtsClientes(
            int? id,
            int? codigo,
            string? nombre,
            string? numeroDoc,
            string? calle,
            string? barrio,
            int? localidadId,
            int? provinciaId,
            int? paisId,
            int? ivaCategoriaId,
            int? clienteCcId,
            int? vendedorId,
            int? cobradorId,
            int? condPagoId,
            int? listaPrecioId,
            int? cuentaId,
            int? transporteId,
            int? depositoId,
            int? situacion,
            int? categoriaId,
            int? fechaAltaDesde,
            int? fechaBajaDesde,
            int? fechaAltaHasta,
            int? fechaBajaHasta,
            int? zonaId,
            int? lineaId,
            int? calificacionId,
            string? telefono,
            string? variables,
            string? lineas,
            string? agendaEmpresaId,
            string? cuit,
            int? clienteEventual,
            bool consultaCompletaOk);
    }
}
