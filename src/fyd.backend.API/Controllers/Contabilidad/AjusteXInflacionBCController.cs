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
    [Route("api/contabilidad/ajustexinflacionbc")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerTag("Módulo de Ajuste por Inflación (Bienes de Cambio).")]
    [AllowAnonymous] // Ajustar según las políticas de seguridad reales
    public class AjusteXInflacionBCController : ApiBaseController
    {
        private readonly IAjusteXInflacionBCProcedimientoAlmacenado _repositorioSp;
        private readonly IServicioLogueo<AjusteXInflacionBCController> _servicioLog;

        public AjusteXInflacionBCController(
            IAjusteXInflacionBCProcedimientoAlmacenado repositorioSp,
            IServicioLogueo<AjusteXInflacionBCController> servicioLog)
        {
            _repositorioSp = repositorioSp;
            _servicioLog = servicioLog;
        }

        /// <summary>
        /// Consulta el detalle de comprobantes para el Ajuste por Inflación (Solapa Datos).
        /// </summary>
        /// <param name="key">Parámetros de búsqueda para el SP.</param>
        /// <returns>Listado detallado de los ingresos al stock.</returns>
        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbAjusteXInflacionBCDetalle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.AjusteXInflacionBC.Consultar")]
        public ActionResult<IQueryable<CstctbAjusteXInflacionBCDetalleReadModel>> CstctbAjusteXInflacionBCDetalle([FromBody] CstctbAjusteXInflacionBCParams key)
        {
            _servicioLog.LoguearInformacion("Procesando solicitud OData para AjusteXInflacionBC (Detalle).");

            var resultado = _repositorioSp.ObtenerDetalle(
                key.FechaCierre,
                key.FechaCierreAnterior,
                key.IncluyeAjustesOK,
                key.EmpresaID,
                key.ArticuloID,
                key.Metodo
            );

            return Ok(resultado);
        }

        /// <summary>
        /// Consulta los totales acumulados por período para el Ajuste por Inflación (Solapa Totales).
        /// </summary>
        /// <param name="key">Parámetros de búsqueda para el SP.</param>
        /// <returns>Listado de totales agrupados por período e índice.</returns>
        [EnableQuery(PageSize = 50)]
        [HttpPost("odata/CstctbAjusteXInflacionBCTotales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RequerirPermiso("Contabilidad.AjusteXInflacionBC.Consultar")]
        public ActionResult<IQueryable<CstctbAjusteXInflacionBCTotalesReadModel>> CstctbAjusteXInflacionBCTotales([FromBody] CstctbAjusteXInflacionBCParams key)
        {
            _servicioLog.LoguearInformacion("Procesando solicitud OData para AjusteXInflacionBC (Totales).");

            var resultado = _repositorioSp.ObtenerTotales(
                key.FechaCierre,
                key.FechaCierreAnterior,
                key.IncluyeAjustesOK,
                key.EmpresaID,
                key.ArticuloID,
                key.Metodo
            );

            return Ok(resultado);
        }
    }
}
