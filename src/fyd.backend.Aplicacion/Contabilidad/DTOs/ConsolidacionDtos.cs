namespace fyd.backend.Aplicacion.Contabilidad.DTOs
{
    public record VerificarEstadoDto(
        int EmpresaId,
        int Anio,
        int Mes
    );

    public record ModuloEstadoDto(
        string Modulo,
        bool YaGenerado,
        bool TieneComprobantes
    );

    public record GenerarConsolidacionDto(
        int EmpresaId,
        int Anio,
        int Mes,
        bool PorComprobante,
        int NumeraTipoId,
        string Detalle,
        string? Observaciones,
        ICollection<string> Modulos
    );

    public record EliminarConsolidacionDto(
        int EmpresaId,
        int Anio,
        int Mes,
        ICollection<string> Modulos
    );
}
