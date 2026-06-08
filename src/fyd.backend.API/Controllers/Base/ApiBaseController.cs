using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Enums;
using Microsoft.AspNetCore.Mvc;

namespace fyd.backend.API.Controllers.Base
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        // 200 OK - Para consultas (GET) o acciones que devuelven datos
        protected IActionResult ManejarOkResultado<T>(Resultado<T> resultado)
        {
            if (resultado.Exitoso)
                return Ok(resultado.Valor);

            return ManejarError(resultado.Error!);
        }

        // 200 OK - Para acciones que no devuelven datos
        protected IActionResult ManejarOkResultado(Resultado resultado)
        {
            if (resultado.Exitoso)
                return Ok();

            return ManejarError(resultado.Error!);
        }

        // 201 Created - Para creación de recursos (POST). Requiere la URI donde se puede consultar el recurso creado.
        protected IActionResult ManejarCreadoResultado<T>(Resultado<T> resultado, string uri)
        {
            if (resultado.Exitoso)
                return Created(uri, resultado.Valor);

            return ManejarError(resultado.Error!);
        }

        // 201 Created - Alternativa muy usada en .NET pasando el nombre del endpoint (CreatedAtAction)
        protected IActionResult ManejarCreadoEnAccionResultado<T>(Resultado<T> resultado, string actionName)
        {
            if (resultado.Exitoso)
                return CreatedAtAction(actionName, new { id = resultado.Valor }, resultado.Valor);

            return ManejarError(resultado.Error!);
        }

        // 204 No Content - Ideal para actualizaciones (PUT) o borrados (DELETE) exitosos
        protected IActionResult ManejarNoContenidoResultado(Resultado resultado)
        {
            if (resultado.Exitoso)
                return NoContent();

            return ManejarError(resultado.Error!);
        }

        private IActionResult ManejarError(Error error)
        {
            var problemDetails = new ProblemDetails
            {
                Title = error.Codigo,
                Detail = error.Descripción
            };

            return error!.Tipo switch
            {
                TipoDeError.NoEncontrado => NotFound(problemDetails),
                TipoDeError.Conflicto => Conflict(problemDetails),
                _ => BadRequest(problemDetails)
            };
        }
    }
}
