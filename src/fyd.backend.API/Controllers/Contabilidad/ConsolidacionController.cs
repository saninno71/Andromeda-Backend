using fyd.backend.API.Controllers.Base;
using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace fyd.backend.API.Controllers.Contabilidad
{
    [ApiController]
    [Route("api/contabilidad/consolidacion")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo de Consolidación Contable.")]
    [AllowAnonymous]
    public class ConsolidacionController : ApiBaseController
    {
        private readonly IConsolidacionServicio _servicio;
        private readonly IServicioLogueo<ConsolidacionController> _servicioLog;

        public ConsolidacionController(
            IConsolidacionServicio servicio,
            IServicioLogueo<ConsolidacionController> servicioLog)
        {
            _servicio = servicio;
            _servicioLog = servicioLog;
        }

        /// <summary>
        /// Verifica el estado de consolidación de cada módulo para el período indicado.
        /// </summary>
        /// <param name="dto">Empresa, año y mes del período a consultar.</param>
        /// <returns>Estado de consolidación por módulo.</returns>
        [HttpPost("estado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequerirPermiso("Contabilidad.Consolidacion.Consultar")]
        public async Task<IActionResult> VerificarEstado([FromBody] VerificarEstadoDto dto)
        {
            var resultado = await _servicio.VerificarEstado(dto);
            return ManejarOkResultado(resultado);
        }

        /// <summary>
        /// Genera los asientos contables de consolidación para el período indicado.
        /// </summary>
        /// <param name="dto">Parámetros de generación: empresa, período, módulos, modo y numeración.</param>
        /// <returns>204 No Content si el proceso fue exitoso.</returns>
        [HttpPost("generar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [RequerirPermiso("Contabilidad.Consolidacion.Crear")]
        public async Task<IActionResult> Generar([FromBody] GenerarConsolidacionDto dto)
        {
            var resultado = await _servicio.Generar(dto);
            return ManejarNoContenidoResultado(resultado);
        }

        /// <summary>
        /// Elimina los asientos de consolidación generados para el período indicado.
        /// </summary>
        /// <param name="dto">Empresa, período y módulos a eliminar.</param>
        /// <returns>204 No Content si el proceso fue exitoso.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [RequerirPermiso("Contabilidad.Consolidacion.Eliminar")]
        public async Task<IActionResult> Eliminar([FromBody] EliminarConsolidacionDto dto)
        {
            var resultado = await _servicio.Eliminar(dto);
            return ManejarNoContenidoResultado(resultado);
        }
    }
}
