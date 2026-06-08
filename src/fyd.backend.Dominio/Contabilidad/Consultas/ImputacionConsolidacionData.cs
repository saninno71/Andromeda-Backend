namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public record LineaAsientoParaGuardar(
        int CuentaId,
        int Tipo,
        decimal Importe,
        decimal ImpLocal,
        decimal ImpReferencia,
        string Detalle
    );

    public record AsientoParaGuardar(
        DateTime Fecha,
        DateTime FechaEmision,
        int NumeraTipoId,
        int EmpresaId,
        int MonedaId,
        decimal CotizacionLocal,
        decimal CotizacionReferencia,
        decimal ImpTotal,
        string Detalle,
        string? Memo,
        IReadOnlyList<LineaAsientoParaGuardar> Lineas
    );

    public record ImputacionConsolidacionData(
        int ComprobanteId,
        DateTime Fecha,
        int MonedaId,
        decimal CotizacionLocal,
        decimal CotizacionReferencia,
        decimal ImpTotal,
        int CuentaId,
        decimal Importe,
        decimal ImporteLocal,
        decimal ImporteReferencia,
        string DetalleComprobante,
        string? DetalleImputacion,
        string NumeraTipoSimbolo,
        int NumeraTipoTipo,
        int? PuntoVenta,
        int Numero,
        string? AgendaNombre,
        int SubcuentaTipoCuenta
    );

    public record AplicacionDifCambioData(
        int ComprobanteAfectadoId,
        decimal ImpAfectado,
        decimal CotizacionLocalOriginal,
        decimal CotizacionReferenciaOriginal
    );
}
