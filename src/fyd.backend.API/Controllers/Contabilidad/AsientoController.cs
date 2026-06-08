using fyd.backend.API.Controllers.Base;
using fyd.backend.API.ParametrosDeConsulta.Contabilidad;
using fyd.backend.Aplicacion.Contabilidad;
using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Dominio.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace fyd.backend.API.Controllers.Contabilidad
{
    [ApiController]
    [Route("api/contabilidad/asientos")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo de Asientos Contables.")]
    [AllowAnonymous]
    public class AsientoController : ApiBaseController
    {
        private readonly IAsientoServicio _servicio;
        private readonly IAsientoProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<AsientoController> _servicioLog;

        public AsientoController(
            IAsientoServicio servicio,
            IAsientoProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<AsientoController> servicioLogueo)
        {
            _servicio = servicio;
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLogueo;
        }

        /// <summary>
        /// Obtiene un asiento contable por su ID.
        /// </summary>
        /// <param name="id">ID del asiento.</param>
        /// <returns>El asiento solicitado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequerirPermiso("Contabilidad.Asientos.Consultar")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorId(id);
            return ManejarOkResultado<ConsultarAsientoDto>(resultado);
        }

        /// <summary>
        /// Crea un nuevo asiento contable.
        /// </summary>
        /// <param name="dto">Datos del asiento a crear.</param>
        /// <returns>El ID del asiento creado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequerirPermiso("Contabilidad.Asientos.Crear")]
        public async Task<IActionResult> Crear([FromBody] CrearAsientoDto dto)
        {
            var resultado = await _servicio.CrearAsiento(dto);
            return ManejarCreadoEnAccionResultado(resultado, nameof(ObtenerPorId));
        }

        /// <summary>
        /// Actualiza un asiento contable existente.
        /// </summary>
        /// <param name="id">ID del asiento a actualizar.</param>
        /// <param name="dto">Nuevos datos del asiento.</param>
        /// <returns>No Content si es exitoso.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequerirPermiso("Contabilidad.Asientos.Actualizar")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarAsientoDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el cuerpo del request." });
            }

            var resultado = await _servicio.ActualizarAsiento(dto);
            return ManejarNoContenidoResultado(resultado);
        }

        /// <summary>
        /// Elimina un asiento contable por su ID.
        /// </summary>
        /// <param name="id">ID del asiento a eliminar.</param>
        /// <returns>No Content si es exitoso.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequerirPermiso("Contabilidad.Asientos.Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _servicio.EliminarAsiento(id);
            return ManejarNoContenidoResultado(resultado);
        }

        /// <summary>
        /// Consulta listado de asientos mediante OData disparado por un SP.
        /// </summary>
        /// <param name="key">Parámetros de búsqueda para el SP.</param>
        /// <returns>Listado de asientos filtrados.</returns>
        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbAsientos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.Asientos.Consultar")]
        public ActionResult<IQueryable<CstctbAsientosReadModel>> CstctbAsientos([FromBody] CstctbAsientosParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbAsientos(
                key.Id,
                key.FechaDesde,
                key.FechaHasta,
                key.Detalle,
                key.CuentaID,
                key.NumeroDesde,
                key.NumeroHasta,
                key.SubcuentaClienteID,
                key.SubcuentaProveedorID,
                key.SubcuentaCajaID,
                key.EmpresaID,
                key.NumeraTipoID);

            return Ok(resultado);
        }
    }
}
