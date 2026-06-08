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
    [Route("api/contabilidad/planDeCuentas")]
    [Produces(MediaTypeNames.Application.Json)] // Define que siempre devuelve JSON
    [Consumes(MediaTypeNames.Application.Json)] // Define que espera JSON
    [SwaggerTag("Módulo de Plan de Cuentas.")]
    [AllowAnonymous]
    public class PlanDeCuentaController : ApiBaseController
    {

        private readonly IPlanDeCuentaServicio _servicio;
        private readonly IPlanDeCuentaProcedimientoAlmacenado _repositorio;
        private readonly IServicioLogueo<PlanDeCuentaController> _servicioLog;

        public PlanDeCuentaController(IPlanDeCuentaServicio servicio, IPlanDeCuentaProcedimientoAlmacenado repositorio, IServicioLogueo<PlanDeCuentaController> servicioLogueo)
        {
            _servicio = servicio;
            _repositorio = repositorio;
            _servicioLog = servicioLogueo;
        }

        /// <summary>
        /// Obtiene la cuenta contable correspondiente al ID provisto.
        /// </summary>
        /// <param name="id">Id de la cuenta contable.</param>
        /// <returns>La cuenta contable especificada.</returns>
        /// <response code="200">Devuelve los datos de la cuenta y sus grupos asignados.</response>
        /// <response code="404">Si no existe una cuenta con el Id especificado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequerirPermiso("Contabilidad.PlanDeCuentas.Consultar")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorId(id);

            return ManejarOkResultado<ConsultarCuentaDto>(resultado);
        }

        /// <summary>
        /// Crea una cuenta contable.
        /// </summary>
        /// <param name="crearCuentaDto">La cuenta contable a crear.</param>
        /// <returns>Devuelve la ruta de acceso a la cuenta contable recién creada.</returns>
        /// <response code="201">Devuelve la confirmación de que la cuenta contable fue creada, con la ruta para poder consultarla.</response>
        /// <response code="400">Devuelve un detalle de error de validación al intentar crear la cuenta contable.</response>
        /// <response code="409">Devuelve un detalle de error indicando un conflicto al intentar crear la cuenta contable.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [RequerirPermiso("Contabilidad.PlanDeCuentas.Crear")]
        public async Task<IActionResult> Crear([FromBody] CrearCuentaDto crearCuentaDto)
        {

            var resultado = await _servicio.CrearCuenta(crearCuentaDto);

            return ManejarCreadoEnAccionResultado(resultado, nameof(ObtenerPorId));
        }

        /// <summary>
        /// Actualiza una cuenta contable
        /// </summary>
        /// <param name="id">Identificador de la cuenta contable a actualizar</param>
        /// <param name="actualizarCuentaDto"></param>
        /// <returns>Devuelve la confirmación de que la operación fue exitosa.</returns>
        /// <response code="204">Devuelve la confirmación de que la cuenta contable fue actualizada.</response>
        /// <response code="400">Devuelve un detalle de error de validación al intentar actualizar la cuenta contable.</response>
        /// <response code="404">Devuelve un detalle de error indicando que no fue posible encontrar la cuenta contable.</response>
        /// <response code="409">Devuelve un detalle de error indicando un conflicto al intentar actualizar la cuenta contable.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [RequerirPermiso("Contabilidad.PlanDeCuentas.Actualizar")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarCuentaDto actualizarCuentaDto)
        {
            if (id != actualizarCuentaDto.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el cuerpo del request." });
            }

            var resultado = await _servicio.ActualizarCuenta(actualizarCuentaDto);

            return ManejarNoContenidoResultado(resultado);

        }

        /// <summary>
        /// Elimina una cuenta contable
        /// </summary>
        /// <param name="id">Identificador de la cuenta contable a eliminar</param>
        /// <returns>Confirmación de que la cuenta contable fue eliminada exitosamente</returns>
        /// <response code="204">Devuelve la confirmación de que la cuenta contable fue eliminada.</response>
        /// <response code="400">Devuelve un detalle de error de validación al intentar eliminar la cuenta contable.</response>
        /// <response code="404">Devuelve un detalle de error indicando que no fue posible encontrar la cuenta contable.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequerirPermiso("Contabilidad.PlanDeCuentas.Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _servicio.EliminarCuenta(id);
            
            return ManejarNoContenidoResultado(resultado);
        }

        /// <summary>
        /// Obtiene el listado de cuentas contables.
        /// </summary>
        /// <param name="key">Parámetros por los cuales es posible filtrar el listado de cuentas contables</param>
        /// <returns>Un listado de cuentas contables filtradas y ordenadas de acuerdo a los parámetros indicados.</returns>
        [EnableQuery(PageSize = 10)]
        [HttpPost("odata/CstctbCuentas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.PlanDeCuentas.Consultar")]
        public ActionResult<IQueryable<CstctbCuentasReadModel>> CstctbCuentas([FromBody] CstctbCuentasParams key)
        {
            //TODO: Ver como manejar esto con el formato de respuesta estandar.
            var cuentas = _repositorio.ObtenerCstctbCuentas(key.Id, key.Codigo, key.Nombre, key.CuentaMadreID, key.AsientoOK, (int?)key.SubcuentaTipo, key.AjustaOK, (int?)key.MonedasTipo, key.EmpresaID);
            return Ok(cuentas);
        }


    }
}
