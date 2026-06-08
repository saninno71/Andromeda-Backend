using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.General;

namespace fyd.backend.Dominio.Contabilidad.Repositorios
{
    public interface IConsolidacionRepositorio
    {
        Task<bool> ModuloYaGenerado(int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos);

        Task<bool> TieneComprobantes(int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos);

        Task<IReadOnlyList<ImputacionConsolidacionData>> ObtenerImputaciones(int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos);

        Task<IReadOnlyList<int>> ObtenerAsientosParaEliminar(int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos);

        /// <summary>
        /// Guarda el gnrComprobantes + ctbAsientos de un asiento generado por consolidación.
        /// Llama a SaveChangesAsync internamente para obtener el ID generado.
        /// </summary>
        Task<int> GuardarAsientoDeConsolidacion(AsientoParaGuardar parametros);

        /// <summary>
        /// Inserta en ctbAsientosComprobantes y actualiza vtsComprobantes.AsientoId / cpsComprobantes.AsientoId
        /// con el ctbAsientos.Id de la línea de Deudores/Proveedores correspondiente.
        /// </summary>
        Task VincularComprobanteConAsiento(int sourceComprobanteId, int asientoGnrId, int empresaId);

        /// <summary>
        /// Inserta en ctbAsientosComprobantes todos los comprobantes del período/módulo apuntando al mismo asiento resumen.
        /// </summary>
        Task VincularResumenConComprobantes(int asientoGnrId, int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos);

        /// <summary>
        /// Elimina ctbAsientosComprobantes y limpia vtsComprobantes.AsientoId / cpsComprobantes.AsientoId
        /// para todos los comprobantes del período/módulo.
        /// </summary>
        Task DesvincularComprobantes(int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos);

        /// <summary>
        /// Verifica que todos los comprobantes del período/módulo tengan entrada en ctbAsientosComprobantes.
        /// Retorna true si el proceso fue completo (ningún comprobante sin asiento).
        /// </summary>
        Task<bool> ValidarAsientosCompletos(int empresaId, DateTime primerDia, DateTime ultimoDia, IEnumerable<int> tipos);

        Task<IReadOnlyList<AplicacionDifCambioData>> ObtenerAplicacionesParaDifCambio(int comprobanteId);

        Task<int> ObtenerSubcuentaTipoCuenta(int cuentaId);

        Task<int?> ObtenerCuentaIdParametro(string codigoVario, int empresaId);

        /// <summary>
        /// Elimina un asiento (gnrComprobantes + ctbAsientos) por su gnrComprobantes.Id.
        /// </summary>
        Task EliminarAsientoPorComprobanteId(int asientoGnrId);
    }
}
