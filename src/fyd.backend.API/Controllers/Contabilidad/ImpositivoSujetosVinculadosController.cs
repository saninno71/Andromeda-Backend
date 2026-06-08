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
    [Route("api/contabilidad/impositivo/sujetos-vinculados")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo Impositivo - Sujetos Vinculados.")]
    [AllowAnonymous]
    public class ImpositivoSujetosVinculadosController : ApiBaseController
    {
        private readonly IImpositivoSujetosVinculadosProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<ImpositivoSujetosVinculadosController> _servicioLog;

        public ImpositivoSujetosVinculadosController(
            IImpositivoSujetosVinculadosProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<ImpositivoSujetosVinculadosController> servicioLog)
        {
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLog;
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoSujetosVinculadosCBTEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoSujetosVinculados.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoSujetosVinculadosCBTEReadModel>> CstctbImpositivoSujetosVinculadosCBTEs([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoSujetosVinculadosCBTE(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoSujetosVinculadosAlicuotas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoSujetosVinculados.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoSujetosVinculadosAlicuotasReadModel>> CstctbImpositivoSujetosVinculadosAlicuotas([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoSujetosVinculadosAlicuotas(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoSujetosVinculadosOperaciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoSujetosVinculados.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoSujetosVinculadosOperacionesReadModel>> CstctbImpositivoSujetosVinculadosOperaciones([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoSujetosVinculadosOperaciones(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }
    }
}
