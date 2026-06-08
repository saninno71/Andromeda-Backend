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
    [Route("api/contabilidad/impositivo/iibb-caba")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo Impositivo - IIBB Ciudad de Buenos Aires.")]
    [AllowAnonymous]
    public class ImpositivoIIBBCABAController : ApiBaseController
    {
        private readonly IImpositivoIIBBCABAProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<ImpositivoIIBBCABAController> _servicioLog;

        public ImpositivoIIBBCABAController(
            IImpositivoIIBBCABAProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<ImpositivoIIBBCABAController> servicioLog)
        {
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLog;
        }

        /// <summary>
        /// Consulta retenciones y percepciones de IIBB CABA.
        /// </summary>
        /// <param name="key">Parámetros de búsqueda para el SP.</param>
        /// <returns>Listado de retenciones y percepciones.</returns>
        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIIBBCABARetPercs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIIBBCABA.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIIBBCABARetPercReadModel>> CstctbImpositivoIIBBCABARetPercs([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIIBBCABARetPerc(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        /// <summary>
        /// Consulta notas de crédito de IIBB CABA.
        /// </summary>
        /// <param name="key">Parámetros de búsqueda para el SP.</param>
        /// <returns>Listado de notas de crédito.</returns>
        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIIBBCABANotasCredito")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIIBBCABA.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIIBBCABANotaCreditoReadModel>> CstctbImpositivoIIBBCABANotasCredito([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIIBBCABANotaCredito(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }
    }
}
