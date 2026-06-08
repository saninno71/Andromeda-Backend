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
    [Route("api/contabilidad/balances")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo de Balances.")]
    [AllowAnonymous]
    public class BalancesController : ApiBaseController
    {
        private readonly IBalancesProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<BalancesController> _servicioLog;

        public BalancesController(
            IBalancesProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<BalancesController> servicioLog) 
        {
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLog;
        }

        /// <summary>
        /// Consulta listado de balances.
        /// </summary>
        /// <param name="key">Parámetros de búsqueda para el SP.</param>
        /// <returns>Listado de balances filtrados.</returns>
        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbBalances")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.Balances.Consultar")]
        public ActionResult<IQueryable<CstctbBalancesReadModel>> CstctbBalances([FromBody] CstctbBalancesParams key)
        {
            var resultado = _repositorioSp.ObtenerCstctbBalances(
                key.CuentaId,
                key.FechaDesde,
                key.FechaHasta,
                key.EmpresaID,
                key.IncluyeImputacionesOK
                );

            return Ok(resultado);
        }

    }
}
