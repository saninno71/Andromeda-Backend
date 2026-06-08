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
    [Route("api/contabilidad/mayores")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo de Mayores.")]
    [AllowAnonymous]
    public class MayoresController : ApiBaseController
    {
        private readonly IMayoresProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<MayoresController> _servicioLog;

        public MayoresController(
            IMayoresProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<MayoresController> servicioLogueo)
        {
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLogueo;
        }

        /// <summary>
        /// Consulta listado de mayores.
        /// </summary>
        /// <param name="key">Parámetros de búsqueda para el SP.</param>
        /// <returns>Listado de mayores filtrados.</returns>
        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbMayores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.Mayores.Consultar")]
        public ActionResult<IQueryable<CstctbMayoresReadModel>> CstctbMayores([FromBody] CstctbMayoresParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbMayores(
                key.Id,
                key.CuentaID,
                key.EmpresaID,
                key.FechaDesde,
                key.FechaHasta,
                key.ArrastraSaldoOK,
                key.IncluyeImputacionesOK,
                key.FechaDesdeInicial
                );

            return Ok(resultado);
        }
    }
}
