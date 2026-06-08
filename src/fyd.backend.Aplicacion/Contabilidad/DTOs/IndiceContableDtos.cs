using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Parametros;

namespace fyd.backend.Aplicacion.Contabilidad.DTOs
{
    public record CrearIndiceDto(
        DateTime Periodo,
        decimal Indice)
    {
        public ParametrosIndice AParametrosIndice()
        {
            return new ParametrosIndice(Periodo, Indice);
        }
    };

    public record ActualizarIndiceDto(
        int Id,
        DateTime Periodo,
        decimal Indice)
    {
        public ParametrosIndice AParametrosIndice()
        {
            return new ParametrosIndice(Periodo, Indice);
        }
    };

    public record ConsultarIndiceDto(
        int Id,
        DateTime Periodo,
        decimal Indice)
    {
        public static ConsultarIndiceDto DesdeEntidad(IndiceContable indice)
        {
            return new ConsultarIndiceDto(
                indice.Id,
                indice.Periodo,
                indice.Indice);
        }
    };
}
