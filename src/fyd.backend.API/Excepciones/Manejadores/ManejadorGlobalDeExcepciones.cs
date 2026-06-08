using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace fyd.backend.API.Excepciones.Manejadores
{
    public class ManejadorGlobalDeExcepciones : IExceptionHandler
    {
        private readonly ILogger<ManejadorGlobalDeExcepciones> _logger;

        public ManejadorGlobalDeExcepciones(ILogger<ManejadorGlobalDeExcepciones> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // 1. Registramos el error técnico en el log
            _logger.LogError(exception, "Error técnico no controlado: {Message}", exception.Message);

            // 2. Armamos la respuesta genérica para no filtrar datos sensibles al front
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Error Interno del Servidor",
                Detail = "Ocurrió un error inesperado en el sistema."
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}