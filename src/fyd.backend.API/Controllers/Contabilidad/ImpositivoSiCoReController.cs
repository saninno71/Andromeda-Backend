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
    [Route("api/contabilidad/impositivo/sicore")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo Impositivo - SiCoRe.")]
    [AllowAnonymous]
    public class ImpositivoSiCoReController : ApiBaseController
    {
        private readonly IImpositivoSiCoReProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<ImpositivoSiCoReController> _servicioLog;

        public ImpositivoSiCoReController(
            IImpositivoSiCoReProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<ImpositivoSiCoReController> servicioLog)
        {
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLog;
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoSiCoReRetPercs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoSiCoRe.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoSiCoReRetPercReadModel>> CstctbImpositivoSiCoReRetPercs([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoSiCoReRetPerc(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoSiCoReSujetosRetenidos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoSiCoRe.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoSiCoReSujetosRetenidosReadModel>> CstctbImpositivoSiCoReSujetosRetenidos([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoSiCoReSujetosRetenidos(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }
    }
}
