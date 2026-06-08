using fyd.backend.API.Controllers.Base;
using fyd.backend.API.ParametrosDeConsulta.Contabilidad;
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
    [Route("api/contabilidad/impositivo/iibb-bsas")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo Impositivo - IIBB Buenos Aires.")]
    [AllowAnonymous]
    public class ImpositivoIIBBBsAsController : ApiBaseController
    {
        private readonly IImpositivoIIBBBsAsProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<ImpositivoIIBBBsAsController> _servicioLog;

        public ImpositivoIIBBBsAsController(
            IImpositivoIIBBBsAsProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<ImpositivoIIBBBsAsController> servicioLog)
        {
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLog;
        }

        /// <summary>
        /// Consulta retenciones de IIBB Buenos Aires.
        /// </summary>
        /// <param name="key">Parámetros de búsqueda para el SP.</param>
        /// <returns>Listado de retenciones.</returns>
        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIIBBBsAsRetenciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIIBBBsAs.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIIBBBsAsRetencionReadModel>> CstctbImpositivoIIBBBsAsRetenciones([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIIBBBsAsRetencion(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        /// <summary>
        /// Consulta percepciones de IIBB Buenos Aires.
        /// </summary>
        /// <param name="key">Parámetros de búsqueda para el SP.</param>
        /// <returns>Listado de percepciones.</returns>
        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIIBBBsAsPercepciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIIBBBsAs.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIIBBBsAsPercepcionReadModel>> CstctbImpositivoIIBBBsAsPercepciones([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIIBBBsAsPercepcion(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }
    }
}
