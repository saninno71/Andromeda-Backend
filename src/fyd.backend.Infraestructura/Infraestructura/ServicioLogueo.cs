using fyd.backend.Dominio.Abstracciones;
using Microsoft.Extensions.Logging;

namespace fyd.backend.Infraestructura.Infraestructura
{
    public class ServicioLogueo<T> : IServicioLogueo<T>
    {
        private readonly ILogger<T> _log;

        public ServicioLogueo(ILogger<T> log)
        {
            _log = log;
        }

        public void LoguearError(string mensaje, Exception excepcion)
        {
            _log.LogError($"{mensaje} - {excepcion.Message}");
        }

        public void LoguearInformacion(string mensaje)
        {
            _log.LogInformation($"{mensaje}");
        }

        public void LoguearAdvertencia(string mensaje)
        {
            _log.LogWarning($"{mensaje}");
        }
    }
}
