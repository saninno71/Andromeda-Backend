using Serilog.Context;

namespace fyd.backend.API.Middlewares
{
    public class TransaccionMiddleware
    {
        private readonly RequestDelegate _next;

        public TransaccionMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            // Generamos un ID de Transacción único para este Request
            var transactionId = Guid.NewGuid().ToString();

            // Empujamos el ID al contexto de Serilog. 
            // Cualquier log que ocurra de aquí en adelante (incluso en el Dominio) lo tendrá.
            using (LogContext.PushProperty("TransactionId", transactionId))
            {
                await _next(context);
            }
        }
    }
}
