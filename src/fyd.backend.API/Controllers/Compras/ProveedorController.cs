using fyd.backend.Aplicacion.Compras.DTOs;
using fyd.backend.Aplicacion.Compras.Servicios.Interfaces;
using fyd.backend.Dominio.Compras.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using fyd.backend.API.ParametrosDeConsulta.Compras;
using fyd.backend.API.Controllers.Base;
using fyd.backend.Dominio.Seguridad;
using Microsoft.AspNetCore.Authorization;

namespace fyd.backend.API.Controllers.Compras
{
    [Route("api/compras/proveedores")]
    [AllowAnonymous]
    public class ProveedorController : ApiBaseController
    {
        private readonly IProveedorServicio _proveedorServicio;
        private readonly IProveedoresProcedimientoAlmacenado _procedimientoAlmacenado;

        public ProveedorController(
            IProveedorServicio proveedorServicio,
            IProveedoresProcedimientoAlmacenado procedimientoAlmacenado)
        {
            _proveedorServicio = proveedorServicio;
            _procedimientoAlmacenado = procedimientoAlmacenado;
        }

        // ==========================================================
        //  OData GET
        // ==========================================================

    /// <summary>
    /// Obtiene el listado de proveedores según los parámetros de búsqueda.
    /// </summary>
    /// <param name="parametros">Parámetros de filtrado para la consulta de proveedores.</param>
    /// <returns>Listado de proveedores que cumplen con los filtros.</returns>
    /// <response code="200">Devuelve el listado de proveedores.</response>
        [HttpPost("odata/cstcpsProveedores")]
        [RequerirPermiso("Compras.Proveedores.Consultar")]
        [EnableQuery]
        public IActionResult ObtenerProveedoresOData([FromBody] CstProveedoresParams parametros)
        {
            var query = _procedimientoAlmacenado.ObtenerDatos(
                parametros.ID, parametros.Codigo, parametros.Nombre, parametros.Nombre2, parametros.NumeroDoc,
                parametros.EmpresaID, parametros.Calle, parametros.Barrio, parametros.LocalidadID, parametros.ProvinciaID,
                parametros.PaisID, parametros.Telefono, parametros.Variables, parametros.IVACategoriaID, parametros.CuentaID,
                parametros.ProveedorCCID, parametros.CondPagoID, parametros.Origen, parametros.CategoriaID, parametros.CalificacionID,
                parametros.FechaAltaDesde, parametros.FechaBajaDesde, parametros.FechaAltaHasta, parametros.FechaBajaHasta,
                parametros.Situacion, parametros.ContactoID, parametros.LineaID, parametros.ProveedorEventual, parametros.IVAGrupoID);

            return Ok(query);
        }

        // ==========================================================
        //  ABM
        // ==========================================================

            /// <summary>
            /// Obtiene un proveedor por su identificador único.
            /// </summary>
            /// <param name="id">Identificador del proveedor.</param>
            /// <returns>El proveedor solicitado.</returns>
            /// <response code="200">Devuelve los datos del proveedor.</response>
            /// <response code="404">Si no existe un proveedor con el Id especificado.</response>
        [HttpGet("{id}")]
        [RequerirPermiso("Compras.Proveedores.Consultar")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _proveedorServicio.ObtenerPorId(id);
            return ManejarOkResultado<ConsultarProveedorDto>(resultado);
        }

            /// <summary>
            /// Crea un nuevo proveedor.
            /// </summary>
            /// <param name="dto">Datos del proveedor a crear.</param>
            /// <returns>Ruta de acceso al proveedor recién creado.</returns>
            /// <response code="201">Proveedor creado correctamente.</response>
            /// <response code="400">Error de validación al crear el proveedor.</response>
            /// <response code="409">Conflicto al intentar crear el proveedor.</response>
        [HttpPost]
        [RequerirPermiso("Compras.Proveedores.Crear")]
        public async Task<IActionResult> Crear([FromBody] CrearProveedorDto dto)
        {
            var resultado = await _proveedorServicio.Crear(dto);
            return ManejarCreadoEnAccionResultado(resultado, nameof(ObtenerPorId));
        }

            /// <summary>
            /// Actualiza los datos de un proveedor existente.
            /// </summary>
            /// <param name="id">Identificador del proveedor a actualizar.</param>
            /// <param name="dto">Datos actualizados del proveedor.</param>
            /// <returns>Confirmación de que la operación fue exitosa.</returns>
            /// <response code="204">Proveedor actualizado correctamente.</response>
            /// <response code="400">Error de validación o el ID de la ruta no coincide con el del cuerpo.</response>
            /// <response code="404">Proveedor no encontrado.</response>
            /// <response code="409">Conflicto al intentar actualizar el proveedor.</response>
        [HttpPut("{id}")]
        [RequerirPermiso("Compras.Proveedores.Actualizar")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarProveedorDto dto)
        {
            if (id != dto.Id) return BadRequest("El ID de la ruta no coincide con el del cuerpo.");
            
            var resultado = await _proveedorServicio.Actualizar(dto);
            return ManejarNoContenidoResultado(resultado);
        }

            /// <summary>
            /// Elimina un proveedor por su identificador.
            /// </summary>
            /// <param name="id">Identificador del proveedor a eliminar.</param>
            /// <returns>Confirmación de que el proveedor fue eliminado exitosamente.</returns>
            /// <response code="204">Proveedor eliminado correctamente.</response>
            /// <response code="400">Error de validación al intentar eliminar el proveedor.</response>
            /// <response code="404">Proveedor no encontrado.</response>
        [HttpDelete("{id}")]
        [RequerirPermiso("Compras.Proveedores.Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _proveedorServicio.Eliminar(id);
            return ManejarNoContenidoResultado(resultado);
        }

            /// <summary>
            /// Indica si el proveedor tiene movimientos en cuenta corriente.
            /// </summary>
            /// <param name="id">Identificador del proveedor.</param>
            /// <returns>True si tiene movimientos, false en caso contrario.</returns>
            /// <response code="200">Devuelve true si el proveedor tiene movimientos en cuenta corriente.</response>
        [HttpGet("{id}/tiene-movimientos-cc")] //TODO: Casing
        [RequerirPermiso("Compras.Proveedores.Actualizar")]
        public async Task<IActionResult> TieneMovimientosCc(int id)
        {
            var resultado = await _proveedorServicio.TieneMovimientosEnCc(id);
            return ManejarOkResultado<bool>(resultado);
        }
    }
}
