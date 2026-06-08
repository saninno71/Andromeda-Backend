using fyd.backend.Aplicacion.Compras.DTOs;
using fyd.backend.Dominio.Compras.Enums;
using fyd.backend.Dominio.Compras.Parametros;
using fyd.backend.Dominio.Compras.Valores;
using fyd.backend.Dominio.General.Valores;
using System.Linq;

namespace fyd.backend.Aplicacion.Compras.Mapeos
{
    public static class ProveedorDtoMapeo
    {
        public static ProveedorParametros AParametros(this CrearProveedorDto dto)
        {
            return new ProveedorParametros
            {
                Codigo = dto.Codigo ?? 0,
                AgendaId = dto.AgendaId,
                ProveedorCcId = dto.ProveedorCcId,
                IvaCategoriaId = dto.IvaCategoriaId,
                CondicionId = dto.CondicionId,
                CategoriaId = dto.CategoriaId,
                CalificacionId = dto.CalificacionId,
                CuentaId = dto.CuentaId,
                MonedaId = dto.MonedaId,
                PorcDescuento1 = dto.PorcDescuento1,
                PorcDescuento2 = dto.PorcDescuento2,
                Origen = (OrigenProveedor)dto.Origen,
                Situacion = (SituacionProveedor)dto.Situacion,
                ImpCredito = dto.ImpCredito,
                EventualOk = dto.EventualOk,
                SujetoVinculadoOk = dto.SujetoVinculadoOk,
                Lineas = dto.Lineas ?? System.Array.Empty<int>(),
                Retenciones = dto.Retenciones?.Select(r => new InfoRetencion(r.RetencionId, r.EmpresaId, r.ExencionDesde, r.ExencionHasta)) ?? System.Array.Empty<InfoRetencion>(),
                InfoAgenda = dto.AgendaDatos != null ? new InfoAgenda(
                    dto.AgendaDatos.EmpresaId, dto.AgendaDatos.Nombre, dto.AgendaDatos.Nombre2, dto.AgendaDatos.Titulo,
                    dto.AgendaDatos.Calle, dto.AgendaDatos.Altura, dto.AgendaDatos.PisoDpto, dto.AgendaDatos.Barrio,
                    dto.AgendaDatos.LocalidadId, dto.AgendaDatos.Cp, dto.AgendaDatos.TipoDocId, dto.AgendaDatos.NumeroDoc,
                    dto.AgendaDatos.Ib, dto.AgendaDatos.Email1, dto.AgendaDatos.Email2, dto.AgendaDatos.Email3,
                    dto.AgendaDatos.Email4, dto.AgendaDatos.Web, dto.AgendaDatos.TipoTelefono1, dto.AgendaDatos.Telefono1,
                    dto.AgendaDatos.TipoTelefono2, dto.AgendaDatos.Telefono2, dto.AgendaDatos.TipoTelefono3,
                    dto.AgendaDatos.Telefono3, dto.AgendaDatos.TipoTelefono4, dto.AgendaDatos.Telefono4,
                    dto.AgendaDatos.TipoTelefono5, dto.AgendaDatos.Telefono5, dto.AgendaDatos.Variable1,
                    dto.AgendaDatos.Variable2, dto.AgendaDatos.Variable3, dto.AgendaDatos.Variable4,
                    dto.AgendaDatos.CorrespondenciaOk, dto.AgendaDatos.CorrespondenciaId, dto.AgendaDatos.Atencion,
                    dto.AgendaDatos.CliDomEntregaOk, dto.AgendaDatos.Memo
                ) : null
            };
        }

        public static ProveedorParametros AParametros(this ActualizarProveedorDto dto)
        {
            return new ProveedorParametros
            {
                Codigo = dto.Codigo ?? 0,
                AgendaId = dto.AgendaId,
                ProveedorCcId = dto.ProveedorCcId,
                IvaCategoriaId = dto.IvaCategoriaId,
                CondicionId = dto.CondicionId,
                CategoriaId = dto.CategoriaId,
                CalificacionId = dto.CalificacionId,
                CuentaId = dto.CuentaId,
                MonedaId = dto.MonedaId,
                PorcDescuento1 = dto.PorcDescuento1,
                PorcDescuento2 = dto.PorcDescuento2,
                Origen = (OrigenProveedor)dto.Origen,
                FechaBaja = dto.FechaBaja,
                Situacion = (SituacionProveedor)dto.Situacion,
                ImpCredito = dto.ImpCredito,
                EventualOk = dto.EventualOk,
                SujetoVinculadoOk = dto.SujetoVinculadoOk,
                Lineas = dto.Lineas ?? System.Array.Empty<int>(),
                Retenciones = dto.Retenciones?.Select(r => new InfoRetencion(r.RetencionId, r.EmpresaId, r.ExencionDesde, r.ExencionHasta)) ?? System.Array.Empty<InfoRetencion>(),
                InfoAgenda = dto.AgendaDatos != null ? new InfoAgenda(
                    dto.AgendaDatos.EmpresaId, dto.AgendaDatos.Nombre, dto.AgendaDatos.Nombre2, dto.AgendaDatos.Titulo,
                    dto.AgendaDatos.Calle, dto.AgendaDatos.Altura, dto.AgendaDatos.PisoDpto, dto.AgendaDatos.Barrio,
                    dto.AgendaDatos.LocalidadId, dto.AgendaDatos.Cp, dto.AgendaDatos.TipoDocId, dto.AgendaDatos.NumeroDoc,
                    dto.AgendaDatos.Ib, dto.AgendaDatos.Email1, dto.AgendaDatos.Email2, dto.AgendaDatos.Email3,
                    dto.AgendaDatos.Email4, dto.AgendaDatos.Web, dto.AgendaDatos.TipoTelefono1, dto.AgendaDatos.Telefono1,
                    dto.AgendaDatos.TipoTelefono2, dto.AgendaDatos.Telefono2, dto.AgendaDatos.TipoTelefono3,
                    dto.AgendaDatos.Telefono3, dto.AgendaDatos.TipoTelefono4, dto.AgendaDatos.Telefono4,
                    dto.AgendaDatos.TipoTelefono5, dto.AgendaDatos.Telefono5, dto.AgendaDatos.Variable1,
                    dto.AgendaDatos.Variable2, dto.AgendaDatos.Variable3, dto.AgendaDatos.Variable4,
                    dto.AgendaDatos.CorrespondenciaOk, dto.AgendaDatos.CorrespondenciaId, dto.AgendaDatos.Atencion,
                    dto.AgendaDatos.CliDomEntregaOk, dto.AgendaDatos.Memo
                ) : null
            };
        }

        public static ConsultarProveedorDto AConsultarDto(this fyd.backend.Dominio.Compras.Entidades.Proveedor dominio)
        {
            return new ConsultarProveedorDto(
                dominio.Id,
                dominio.Codigo,
                dominio.AgendaId,
                dominio.ProveedorCcId,
                dominio.IvaCategoriaId,
                dominio.CondicionId,
                dominio.CategoriaId,
                dominio.CalificacionId,
                dominio.CuentaId,
                dominio.MonedaId,
                dominio.PorcDescuento1,
                dominio.PorcDescuento2,
                (int)dominio.Origen,
                dominio.FechaAlta,
                dominio.FechaBaja,
                (int)dominio.Situacion,
                dominio.ImpCredito,
                dominio.EventualOk,
                dominio.SujetoVinculadoOk,
                dominio.InfoAgenda != null ? new InfoAgendaDto(
                    dominio.InfoAgenda.EmpresaId, dominio.InfoAgenda.Nombre, dominio.InfoAgenda.Nombre2, dominio.InfoAgenda.Titulo,
                    dominio.InfoAgenda.Calle, dominio.InfoAgenda.Altura, dominio.InfoAgenda.PisoDpto, dominio.InfoAgenda.Barrio,
                    dominio.InfoAgenda.LocalidadId, dominio.InfoAgenda.Cp, dominio.InfoAgenda.TipoDocId, dominio.InfoAgenda.NumeroDoc,
                    dominio.InfoAgenda.Ib, dominio.InfoAgenda.Email1, dominio.InfoAgenda.Email2, dominio.InfoAgenda.Email3,
                    dominio.InfoAgenda.Email4, dominio.InfoAgenda.Web, dominio.InfoAgenda.TipoTelefono1, dominio.InfoAgenda.Telefono1,
                    dominio.InfoAgenda.TipoTelefono2, dominio.InfoAgenda.Telefono2, dominio.InfoAgenda.TipoTelefono3,
                    dominio.InfoAgenda.Telefono3, dominio.InfoAgenda.TipoTelefono4, dominio.InfoAgenda.Telefono4,
                    dominio.InfoAgenda.TipoTelefono5, dominio.InfoAgenda.Telefono5, dominio.InfoAgenda.Variable1,
                    dominio.InfoAgenda.Variable2, dominio.InfoAgenda.Variable3, dominio.InfoAgenda.Variable4,
                    dominio.InfoAgenda.CorrespondenciaOk, dominio.InfoAgenda.CorrespondenciaId, dominio.InfoAgenda.Atencion,
                    dominio.InfoAgenda.CliDomEntregaOk, dominio.InfoAgenda.Memo
                ) : null,
                dominio.Lineas,
                dominio.Retenciones?.Select(r => new InfoRetencionDto(r.RetencionId, r.EmpresaId, r.ExencionDesde, r.ExencionHasta)) ?? System.Linq.Enumerable.Empty<InfoRetencionDto>()
            );
        }
    }
}
