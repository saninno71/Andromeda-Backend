using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Enums;
using fyd.backend.Dominio.Contabilidad.Parametros;


namespace fyd.backend.Aplicacion.Contabilidad.DTOs
{
    public record CrearCuentaDto(
                string Codigo,
                string Nombre,
                bool AsientoOk,
                SubcuentaTipo SubcuentaTipo,
                MonedaTipo MonedaTipo,
                bool AjustaOk,
                int? EmpresaId,
                int? CuentaIdMadre,
                List<int>? GruposIds // Lista de IDs de grupos seleccionados
            )
    {
        public ParametrosCuenta AParametrosCuenta()
        {
            return new ParametrosCuenta(
                Codigo,
                Nombre,
                AsientoOk,
                SubcuentaTipo,
                MonedaTipo,
                AjustaOk,
                EmpresaId,
                CuentaIdMadre
                );
        }
    };

    // Para editar
    public record ActualizarCuentaDto(
        int Id,
        string Codigo,
        string Nombre,
        bool AsientoOk,
        SubcuentaTipo SubcuentaTipo,
        MonedaTipo MonedaTipo,
        bool AjustaOk,
        int? EmpresaId,
        int? CuentaIdMadre,
        List<int>? GruposIds


    )
    {
        public ParametrosCuenta AParametrosCuenta()
        {
            return new ParametrosCuenta(
                Codigo,
                Nombre,
                AsientoOk,
                SubcuentaTipo,
                MonedaTipo,
                AjustaOk,
                EmpresaId,
                CuentaIdMadre
                );
        }
    };

    public record ConsultarCuentaDto
    (int Id,
        string? Codigo,
        string? Nombre,
        int? CuentaMadreId,
        bool AsientoOk,
        SubcuentaTipo SubcuentaTipo,
        bool AjustaOk,
        MonedaTipo MonedasTipo,
        int? EmpresaId,
        ICollection<GrupoDto> Grupos
    )
    {
        public static ConsultarCuentaDto DesdeEntidad(CuentaContable cuenta)
        {
            return new ConsultarCuentaDto(
                cuenta.Id,
                cuenta.Codigo,
                cuenta.Nombre,
                cuenta.CuentaIdMadre,
                cuenta.AsientoOk,
                cuenta.SubcuentaTipo,
                cuenta.AjustaOk,
                cuenta.MonedaTipo,
                cuenta.EmpresaId,
                cuenta.Grupos.Select(g => new GrupoDto(g.Id, g.Nombre)).ToList());
        }
    };

    public record GrupoDto
    (int Id,
        string Nombre
    );

}
