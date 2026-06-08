using fyd.backend.API.Controllers.Base;
using fyd.backend.API.ParametrosDeConsulta.Ventas;
using fyd.backend.Aplicacion.Ventas.DTOs;
using fyd.backend.Aplicacion.Ventas.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Seguridad;
using fyd.backend.Dominio.Ventas.Consultas;
using fyd.backend.Dominio.Ventas.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace fyd.backend.API.Controllers.Ventas
{
    [ApiController]
    [Route("api/ventas/clientes")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo de Clientes.")]
    [AllowAnonymous]
    public class ClienteController : ApiBaseController
    {
        private readonly IClienteServicio _servicio;
        private readonly IClienteProcedimientoAlmacenado _repositorio;
        private readonly IServicioLogueo<ClienteController> _servicioLog;

        public ClienteController(
            IClienteServicio servicio, 
            IClienteProcedimientoAlmacenado repositorio, 
            IServicioLogueo<ClienteController> servicioLogueo)
        {
            _servicio = servicio;
            _repositorio = repositorio;
            _servicioLog = servicioLogueo;
        }

        /// <summary>
        /// Obtiene el cliente correspondiente al ID provisto.
        /// </summary>
        /// <param name="id">Id del cliente.</param>
        /// <returns>Los datos del cliente especificado.</returns>
        /// <response code="200">Devuelve los datos del cliente.</response>
        /// <response code="404">Si no existe un cliente con el Id especificado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequerirPermiso("Ventas.Clientes.Consultar")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorId(id);
            return ManejarOkResultado<ConsultarClienteDto>(resultado);
        }

        /// <summary>
        /// Crea un cliente.
        /// </summary>
        /// <param name="crearClienteDto">Los datos del cliente a crear.</param>
        /// <returns>Devuelve la ruta de acceso al cliente recién creado.</returns>
        /// <response code="201">Devuelve la confirmación de que el cliente fue creado.</response>
        /// <response code="400">Devuelve un detalle de error de validación.</response>
        /// <response code="409">Devuelve un detalle de error indicando conflicto (i.e. código duplicado).</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [RequerirPermiso("Ventas.Clientes.Crear")]
        public async Task<IActionResult> Crear([FromBody] CrearClienteDto crearClienteDto)
        {
            var resultado = await _servicio.Crear(crearClienteDto);
            return ManejarCreadoEnAccionResultado(resultado, nameof(ObtenerPorId));
        }

        /// <summary>
        /// Actualiza un cliente.
        /// </summary>
        /// <param name="id">Identificador del cliente a actualizar.</param>
        /// <param name="actualizarClienteDto">Los datos a actualizar.</param>
        /// <returns>Confirmación de operación exitosa.</returns>
        /// <response code="204">El cliente fue actualizado.</response>
        /// <response code="400">Error de validación.</response>
        /// <response code="404">No fue posible encontrar el cliente.</response>
        /// <response code="409">Conflicto en la base de datos.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [RequerirPermiso("Ventas.Clientes.Actualizar")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarClienteDto actualizarClienteDto)
        {
            if (id != actualizarClienteDto.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el request." });
            }

            var resultado = await _servicio.Actualizar(actualizarClienteDto);
            return ManejarNoContenidoResultado(resultado);
        }

        /// <summary>
        /// Elimina un cliente.
        /// </summary>
        /// <param name="id">Identificador del cliente a eliminar.</param>
        /// <returns>Confirmación de eliminación.</returns>
        /// <response code="204">El cliente fue eliminado.</response>
        /// <response code="400">El cliente tiene dependencias (comprobantes, memos).</response>
        /// <response code="404">No se encontró el cliente.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequerirPermiso("Ventas.Clientes.Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _servicio.Eliminar(id);
            return ManejarNoContenidoResultado(resultado);
        }

        /// <summary>
        /// Obtiene el listado de clientes mediante OData.
        /// </summary>
        /// <param name="key">Filtros de búsqueda OData SP.</param>
        /// <returns>Listado OData de clientes.</returns>
        [EnableQuery(PageSize = 10)]
        [HttpPost("odata/CstvtsClientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Ventas.Clientes.Consultar")]
        public ActionResult<IQueryable<CstvtsClientesReadModel>> CstvtsClientes([FromBody] CstvtsClientesParams key)
        {
            var clientes = _repositorio.ObtenerCstvtsClientes(
                key.Id, key.Codigo, key.Nombre, key.NumeroDoc, key.Calle, key.Barrio,
                key.LocalidadID, key.ProvinciaID, key.PaisID, key.IVACategoriaID, key.ClienteCCID,
                key.VendedorID, key.CobradorID, key.CondPagoID, key.ListaPrecioID, key.CuentaID,
                key.TransporteID, key.DepositoID, key.Situacion, key.CategoriaID,
                key.FechaAltaDesde, key.FechaBajaDesde, key.FechaAltaHasta, key.FechaBajaHasta,
                key.ZonaID, key.LineaID, key.CalificacionID, key.Telefono, key.Variables, key.Lineas,
                key.AgendaEmpresaID, key.CUIT, key.ClienteEventual, key.ConsultaCompletaOK);
            return Ok(clientes);
        }
    }
}
