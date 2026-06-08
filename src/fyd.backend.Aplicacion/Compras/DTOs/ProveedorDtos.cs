using System;
using System.Collections.Generic;

namespace fyd.backend.Aplicacion.Compras.DTOs
{
    public record InfoAgendaDto(
        int? EmpresaId,
        string Nombre,
        string Nombre2,
        string Titulo,
        string Calle,
        int? Altura,
        string PisoDpto,
        string Barrio,
        int LocalidadId,
        string Cp,
        int TipoDocId,
        string NumeroDoc,
        string Ib,
        string Email1,
        string Email2,
        string Email3,
        string Email4,
        string Web,
        string TipoTelefono1,
        string Telefono1,
        string TipoTelefono2,
        string Telefono2,
        string TipoTelefono3,
        string Telefono3,
        string TipoTelefono4,
        string Telefono4,
        string TipoTelefono5,
        string Telefono5,
        string Variable1,
        string Variable2,
        string Variable3,
        string Variable4,
        bool CorrespondenciaOk,
        int CorrespondenciaId,
        string Atencion,
        bool CliDomEntregaOk,
        string Memo
    );

    public record InfoRetencionDto(
        int RetencionId,
        int EmpresaId,
        DateTime? ExencionDesde,
        DateTime? ExencionHasta
    );

    public record CrearProveedorDto(
        int? Codigo,
        int AgendaId,
        int ProveedorCcId,
        int IvaCategoriaId,
        int CondicionId,
        int CategoriaId,
        int CalificacionId,
        int? CuentaId,
        int? MonedaId,
        float PorcDescuento1,
        float PorcDescuento2,
        int Origen,
        decimal ImpCredito,
        int Situacion,
        bool EventualOk,
        bool SujetoVinculadoOk,
        InfoAgendaDto? AgendaDatos,
        IEnumerable<int> Lineas,
        IEnumerable<InfoRetencionDto> Retenciones
    );

    public record ActualizarProveedorDto(
        int Id,
        int? Codigo,
        int AgendaId,
        int ProveedorCcId,
        int IvaCategoriaId,
        int CondicionId,
        int CategoriaId,
        int CalificacionId,
        int? CuentaId,
        int? MonedaId,
        float PorcDescuento1,
        float PorcDescuento2,
        int Origen,
        DateTime? FechaBaja,
        int Situacion,
        decimal ImpCredito,
        bool EventualOk,
        bool SujetoVinculadoOk,
        InfoAgendaDto? AgendaDatos,
        IEnumerable<int> Lineas,
        IEnumerable<InfoRetencionDto> Retenciones
    );

    public record ConsultarProveedorDto(
        int Id,
        int Codigo,
        int AgendaId,
        int ProveedorCcId,
        int IvaCategoriaId,
        int CondicionId,
        int CategoriaId,
        int CalificacionId,
        int? CuentaId,
        int? MonedaId,
        float PorcDescuento1,
        float PorcDescuento2,
        int Origen,
        DateTime? FechaAlta,
        DateTime? FechaBaja,
        int Situacion,
        decimal ImpCredito,
        bool EventualOk,
        bool SujetoVinculadoOk,
        InfoAgendaDto? AgendaDatos,
        IEnumerable<int> Lineas,
        IEnumerable<InfoRetencionDto> Retenciones
    );
}
