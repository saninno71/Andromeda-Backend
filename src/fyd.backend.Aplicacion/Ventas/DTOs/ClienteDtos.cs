using fyd.backend.Dominio.Ventas.Enums;
using System;
using System.Collections.Generic;

namespace fyd.backend.Aplicacion.Ventas.DTOs
{
    public record CrearClienteDto(
        int? Codigo,
        string Nombre,
        string? Nombre2,
        string? Titulo,
        int TipoDocId,
        string? NumeroDoc,
        string? Ib,
        int? IibbProvinciaId,
        string? Calle,
        string? Altura,
        string? PisoDpto,
        string? Barrio,
        string? Cp,
        int? LocalidadId,
        ICollection<TelefonoDto>? Telefonos,
        string? Web,
        ICollection<string>? Emails,
        int IvaCategoriaId,
        float PorcIvaLiberado,
        int VendedorId,
        int CobradorId,
        int CondicionId,
        decimal ImpCredito,
        int MonedaId,
        int? ListaId,
        float PorcDescuento1,
        float PorcDescuento2,
        float PorcDescuento3,
        int TransporteId,
        int? DepositoId,
        int? DomEntregaId,
        int CalificacionId,
        int CategoriaId,
        int ZonaId,
        DateTime? FechaAlta,
        DateTime? FechaBaja,
        SituacionCliente Situacion,
        bool EventualOk,
        bool SujetoVinculadoOk,
        int? ClienteCcId,
        int? CuentaId,
        int? AgendaEmpresaId,
        string? Variable1,
        string? Variable2,
        string? Variable3,
        string? Variable4,
        ICollection<int>? LineasIds,
        ClienteDatoAdicionalDto? DatosAdicionales,
        ICollection<ClientePercepcionDto>? Percepciones,
        string? Observaciones
    );

    public record ActualizarClienteDto(
        int Id,
        int? Codigo,
        string Nombre,
        string? Nombre2,
        string? Titulo,
        int TipoDocId,
        string? NumeroDoc,
        string? Ib,
        int? IibbProvinciaId,
        string? Calle,
        string? Altura,
        string? PisoDpto,
        string? Barrio,
        string? Cp,
        int? LocalidadId,
        ICollection<TelefonoDto>? Telefonos,
        string? Web,
        ICollection<string>? Emails,
        int IvaCategoriaId,
        float PorcIvaLiberado,
        int VendedorId,
        int CobradorId,
        int CondicionId,
        decimal ImpCredito,
        int MonedaId,
        int? ListaId,
        float PorcDescuento1,
        float PorcDescuento2,
        float PorcDescuento3,
        int TransporteId,
        int? DepositoId,
        int? DomEntregaId,
        int CalificacionId,
        int CategoriaId,
        int ZonaId,
        DateTime? FechaAlta,
        DateTime? FechaBaja,
        SituacionCliente Situacion,
        bool EventualOk,
        bool SujetoVinculadoOk,
        int? ClienteCcId,
        int? CuentaId,
        int? AgendaEmpresaId,
        string? Variable1,
        string? Variable2,
        string? Variable3,
        string? Variable4,
        ICollection<int>? LineasIds,
        ClienteDatoAdicionalDto? DatosAdicionales,
        ICollection<ClientePercepcionDto>? Percepciones,
        string? Observaciones
    );

    public record ConsultarClienteDto(
        int Id, int Codigo, string Nombre, int AgendaId, int? ClienteCcId,
        int VendedorId, int CobradorId, int IvaCategoriaId, int CondicionId,
        int CategoriaId, int ZonaId, int? IibbProvinciaId, int? ListaId,
        int CalificacionId, int? CuentaId, int TransporteId, int DomEntregaId,
        int? DepositoId, float PorcDescuento1, float PorcDescuento2, float PorcDescuento3,
        float PorcIvaLiberado, DateTime? FechaAlta, DateTime? FechaBaja, SituacionCliente Situacion,
        decimal ImpCredito, int MonedaId, bool EventualOk, bool SujetoVinculadoOk
    );

    public record TelefonoDto(string? Tipo, string? Numero);

    public record ClienteDatoAdicionalDto(
        string? Dato01, string? Dato02, string? Dato03, string? Dato04, string? Dato05,
        string? Dato06, string? Dato07, string? Dato08, string? Dato09, string? Dato10
    );

    public record ClientePercepcionDto(
        int EmpresaId,
        int PercepcionId,
        DateTime? ExencionDesde,
        DateTime? ExencionHasta
    );
}
