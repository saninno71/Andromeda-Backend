using fyd.backend.API.Controllers.Base;
using fyd.backend.API.ParametrosDeConsulta.Contabilidad;
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
// consulta cstctbIndices implementada con LINQ sobre ContextoLectura

namespace fyd.backend.API.Controllers.Contabilidad
{
    [ApiController]
    [Route("api/contabilidad/indicesContables")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo de Índices Contables.")]
    [AllowAnonymous]
    public class IndiceContableController : ApiBaseController
    {
        private readonly IIndiceContableServicio _servicio;
        private readonly ICstctbIndicesProcedimientoAlmacenado _repositorio;
        private readonly IServicioLogueo<IndiceContableController> _servicioLog;

        public IndiceContableController(
            IIndiceContableServicio servicio,
            ICstctbIndicesProcedimientoAlmacenado repositorio,
            IServicioLogueo<IndiceContableController> servicioLogueo)
        {
            _servicio = servicio;
            _repositorio = repositorio;
            _servicioLog = servicioLogueo;
        }

        /// <summary>
        /// Obtiene el índice contable correspondiente al ID provisto.
        /// </summary>
        /// <param name="id">Id del índice contable.</param>
        /// <returns>El índice contable especificado.</returns>
        /// <response code="200">Devuelve los datos del índice contable.</response>
        /// <response code="404">Si no existe un índice con el Id especificado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequerirPermiso("Contabilidad.IndicesContables.Consultar")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorId(id);
            return ManejarOkResultado<ConsultarIndiceDto>(resultado);
        }

        /// <summary>
        /// Crea un índice contable.
        /// </summary>
        /// <param name="crearIndiceDto">El índice contable a crear.</param>
        /// <returns>Devuelve la ruta de acceso al índice contable recién creado.</returns>
        /// <response code="201">Devuelve la confirmación de que el índice fue creado.</response>
        /// <response code="400">Devuelve un detalle de error de validación.</response>
        /// <response code="409">Devuelve un detalle de error de conflicto (período duplicado).</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [RequerirPermiso("Contabilidad.IndicesContables.Crear")]
        public async Task<IActionResult> Crear([FromBody] CrearIndiceDto crearIndiceDto)
        {
            var resultado = await _servicio.Crear(crearIndiceDto);
            return ManejarCreadoEnAccionResultado(resultado, nameof(ObtenerPorId));
        }

        /// <summary>
        /// Actualiza un índice contable.
        /// </summary>
        /// <param name="id">Identificador del índice contable a actualizar.</param>
        /// <param name="actualizarIndiceDto">Datos del índice contable a actualizar.</param>
        /// <returns>Devuelve la confirmación de que la operación fue exitosa.</returns>
        /// <response code="204">Devuelve la confirmación de que el índice fue actualizado.</response>
        /// <response code="400">Devuelve un detalle de error de validación.</response>
        /// <response code="404">No fue posible encontrar el índice contable.</response>
        /// <response code="409">Conflicto al intentar actualizar (período duplicado).</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [RequerirPermiso("Contabilidad.IndicesContables.Actualizar")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarIndiceDto actualizarIndiceDto)
        {
            if (id != actualizarIndiceDto.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el cuerpo del request." });
            }

            var resultado = await _servicio.Actualizar(actualizarIndiceDto);
            return ManejarNoContenidoResultado(resultado);
        }

        /// <summary>
        /// Elimina un índice contable.
        /// </summary>
        /// <param name="id">Identificador del índice contable a eliminar.</param>
        /// <returns>Confirmación de que el índice fue eliminado exitosamente.</returns>
        /// <response code="204">Devuelve la confirmación de que el índice fue eliminado.</response>
        /// <response code="404">No fue posible encontrar el índice contable.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequerirPermiso("Contabilidad.IndicesContables.Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _servicio.Eliminar(id);
            return ManejarNoContenidoResultado(resultado);
        }

        /// <summary>
        /// Obtiene el listado de índices contables. Soporta filtros OData ($filter, $orderby, $top, $skip).
        /// Parámetros opcionales: Id, PeriodoDesde, PeriodoHasta.
        /// </summary>
        /// <param name="key">Parámetros de filtro previos a OData.</param>
        /// <returns>Listado de índices contables.</returns>
        [EnableQuery(PageSize = 100)]
        [HttpPost("odata/CstctbIndices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.IndicesContables.Consultar")]
        public ActionResult<IQueryable<CstctbIndicesReadModel>> CstctbIndices([FromBody] CstctbIndicesParams key)
        {
            var indices = _repositorio.ObtenerCstctbIndices(key.Id, key.PeriodoDesde, key.PeriodoHasta);
            return Ok(indices);
        }
    }
}
