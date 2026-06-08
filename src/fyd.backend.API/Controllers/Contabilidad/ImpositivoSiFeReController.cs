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
    [Route("api/contabilidad/impositivo/sifere")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo Impositivo - SiFeRe.")]
    [AllowAnonymous]
    public class ImpositivoSiFeReController : ApiBaseController
    {
        private readonly IImpositivoSiFeReProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<ImpositivoSiFeReController> _servicioLog;

        public ImpositivoSiFeReController(
            IImpositivoSiFeReProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<ImpositivoSiFeReController> servicioLog)
        {
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLog;
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoSiFeReFacturacionJurisdicciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoSiFeRe.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoSiFeReFacturacionJurisdiccionReadModel>> CstctbImpositivoSiFeReFacturacionJurisdicciones([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoSiFeReFacturacionJurisdiccion(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoSiFeReFacturacionTipoImportes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoSiFeRe.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoSiFeReFacturacionTipoImporteReadModel>> CstctbImpositivoSiFeReFacturacionTipoImportes([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoSiFeReFacturacionTipoImporte(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoSiFeReRetenciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoSiFeRe.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoSiFeReRetencionReadModel>> CstctbImpositivoSiFeReRetenciones([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoSiFeReRetencion(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoSiFeRePercepciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoSiFeRe.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoSiFeRePercepcionReadModel>> CstctbImpositivoSiFeRePercepciones([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoSiFeRePercepcion(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoSiFeRePercepcionesAduaneras")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoSiFeRe.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoSiFeRePercepcionAduaneraReadModel>> CstctbImpositivoSiFeRePercepcionesAduaneras([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoSiFeRePercepcionAduanera(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoSiFeReRecaudacionesBancarias")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoSiFeRe.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoSiFeReRecaudacionBancariaReadModel>> CstctbImpositivoSiFeReRecaudacionesBancarias([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoSiFeReRecaudacionBancaria(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }
    }
}
