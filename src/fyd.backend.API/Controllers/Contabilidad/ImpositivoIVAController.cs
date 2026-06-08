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
    [Route("api/contabilidad/impositivo/iva")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo Impositivo - IVA.")]
    [AllowAnonymous]
    public class ImpositivoIVAController : ApiBaseController
    {
        private readonly IImpositivoIVAProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<ImpositivoIVAController> _servicioLog;

        public ImpositivoIVAController(
            IImpositivoIVAProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<ImpositivoIVAController> servicioLog)
        {
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLog;
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIVAVentasDebitoFiscales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIVA.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIVAVentasDebitoFiscalReadModel>> CstctbImpositivoIVAVentasDebitoFiscales([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIVAVentasDebitoFiscal(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIVAComprasCreditoFiscal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIVA.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIVAGrupoReadModel>> CstctbImpositivoIVAComprasCreditoFiscal([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIVAComprasCreditoFiscal(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIVAComprasCreditoFiscalConsolidado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIVA.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIVAGrupoReadModel>> CstctbImpositivoIVAComprasCreditoFiscalConsolidado([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIVAComprasCreditoFiscalConsolidado(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIVASinCreditoFiscales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIVA.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIVASinCreditoFiscalReadModel>> CstctbImpositivoIVASinCreditoFiscales([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIVASinCreditoFiscal(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIVARestitucionDebitoFiscales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIVA.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIVARestitucionDebitoFiscalReadModel>> CstctbImpositivoIVARestitucionDebitoFiscales([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIVARestitucionDebitoFiscal(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIVARetenciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIVA.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIVARetencionReadModel>> CstctbImpositivoIVARetenciones([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIVARetencion(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIVAPercepciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIVA.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIVAPercepcionReadModel>> CstctbImpositivoIVAPercepciones([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIVAPercepcion(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoIVAComprasExteriores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoIVA.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoIVAComprasExteriorReadModel>> CstctbImpositivoIVAComprasExteriores([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoIVAComprasExterior(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }
    }
}
