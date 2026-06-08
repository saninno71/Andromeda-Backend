namespace fyd.backend.Dominio.Abstracciones
{
    public interface IServicioLogueo<T>
    {
        void LoguearInformacion(string mensaje);
        void LoguearAdvertencia(string mensaje);
        void LoguearError(string mensaje, Exception excepcion);
    }
}
