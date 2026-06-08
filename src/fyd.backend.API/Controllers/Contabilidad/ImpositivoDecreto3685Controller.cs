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
    [Route("api/contabilidad/impositivo/decreto-3685")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo Impositivo - Decreto 3685 Título 2.")]
    [AllowAnonymous]
    public class ImpositivoDecreto3685Controller : ApiBaseController
    {
        private readonly IImpositivoDecreto3685ProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<ImpositivoDecreto3685Controller> _servicioLog;

        public ImpositivoDecreto3685Controller(
            IImpositivoDecreto3685ProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<ImpositivoDecreto3685Controller> servicioLog)
        {
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLog;
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoDecreto3685Cabeceras")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoDecreto3685.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoDecreto3685CabeceraReadModel>> CstctbImpositivoDecreto3685Cabeceras([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoDecreto3685Cabecera(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoDecreto3685Detalles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoDecreto3685.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoDecreto3685DetalleReadModel>> CstctbImpositivoDecreto3685Detalles([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoDecreto3685Detalle(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }

        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbImpositivoDecreto3685Percepciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.ImpositivoDecreto3685.Consultar")]
        public ActionResult<IQueryable<CstctbImpositivoDecreto3685PercepcionReadModel>> CstctbImpositivoDecreto3685Percepciones([FromBody] CstctbImpositivoParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbImpositivoDecreto3685Percepcion(key.EmpresaID, key.FechaInicio, key.FechaFin);
            return Ok(resultado);
        }
    }
}
