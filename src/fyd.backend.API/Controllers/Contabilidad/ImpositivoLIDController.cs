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
    [Route("api/contabilidad/impositivo/lid")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo Impositivo - Libro IVA Digital (LID).")]
    [AllowAnonymous]
    public class ImpositivoLIDController : ApiBaseController
    {
        private readonly IImpositivoLIDProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<ImpositivoLIDController> _servicioLog;

        public ImpositivoLIDController(
            IImpositivoLIDProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<ImpositivoLIDController> servicioLog)
        {
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLog;
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoLIDVentasCBTEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoLID.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoLIDVentasCBTEReadModel>> CstctbImpositivoLIDVentasCBTEs([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoLIDVentasCBTE(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoLIDVentasAlicuotas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoLID.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoLIDVentasAlicuotasReadModel>> CstctbImpositivoLIDVentasAlicuotas([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoLIDVentasAlicuotas(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoLIDComprasCBTEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoLID.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoLIDComprasCBTEReadModel>> CstctbImpositivoLIDComprasCBTEs([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoLIDComprasCBTE(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoLIDComprasAlicuotas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoLID.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoLIDComprasAlicuotasReadModel>> CstctbImpositivoLIDComprasAlicuotas([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoLIDComprasAlicuotas(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoLIDComprasImportacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoLID.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoLIDComprasCBTEReadModel>> CstctbImpositivoLIDComprasImportacion([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoLIDComprasImportacion(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoLIDComprasImportacionAlicuotas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoLID.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoLIDComprasImportacionAlicuotasReadModel>> CstctbImpositivoLIDComprasImportacionAlicuotas([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoLIDComprasImportacionAlicuotas(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }
    }
}
