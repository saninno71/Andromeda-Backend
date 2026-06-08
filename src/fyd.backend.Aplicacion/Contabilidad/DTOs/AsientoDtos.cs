using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Aplicacion.Contabilidad.DTOs
{
    public record CrearAsientoLineaDto(
            int CuentaId,
            int Tipo, // 1=Debe, -1=Haber
            decimal Importe,
            string Detalle,
            int? ClienteId,
            int? ProveedorId,
            int? CajaId
        );

    public record CrearAsientoDto(
        DateTime Fecha,
        DateTime FechaEmision,
        int NumeraTipoId,
        int? Numero,
        int EmpresaId,
        int MonedaId,
        decimal CotizacionLocal,
        decimal CotizacionReferencia,
        string Detalle,
        string? Observaciones,
        List<CrearAsientoLineaDto> Lineas
    );

    public record ActualizarAsientoLineaDto(
        int? Id, // null = línea nueva
        int CuentaId,
        int Tipo,
        decimal Importe,
        string Detalle,
        int? ClienteId,
        int? ProveedorId,
        int? CajaId
    );

    public record ActualizarAsientoDto(
        int Id,
        DateTime Fecha,
        DateTime FechaEmision,
        int NumeraTipoId,
        int? Numero,
        int EmpresaId,
        int MonedaId,
        decimal CotizacionLocal,
        decimal CotizacionReferencia,
        string Detalle,
        string? Observaciones,
        List<ActualizarAsientoLineaDto> Lineas
    );

    public record ConsultarAsientoLineaDto(
        int Id,
        int CuentaId,
        string CuentaCodigo,
        string CuentaNombre,
        int Tipo,
        decimal Importe,
        decimal ImpLocal,
        decimal ImpReferencia,
        string Detalle,
        int? ClienteId,
        string? ClienteNombre,
        int? ProveedorId,
        string? ProveedorNombre,
        int? CajaId,
        string? CajaNombre
    );

    public record ConsultarAsientoDto(
        int Id,
        DateTime Fecha,
        DateTime FechaEmision,
        int NumeraTipoId,
        string NumeraTipoSimbolo,
        string Letra,
        int? PuntoVenta,
        int? Numero,
        int EmpresaId,
        string EmpresaNombre,
        int MonedaId,
        string MonedaNombre,
        decimal CotizacionLocal,
        decimal CotizacionReferencia,
        decimal ImpTotal,
        string Detalle,
        string? Observaciones,
        ICollection<ConsultarAsientoLineaDto> Lineas
    );
}
