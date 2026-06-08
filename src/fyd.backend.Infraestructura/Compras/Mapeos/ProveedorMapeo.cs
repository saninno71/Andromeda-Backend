using DominioProveedor = fyd.backend.Dominio.Compras.Entidades.Proveedor;
using InfraProveedor = fyd.backend.Infraestructura.Compras.Entidades.Proveedor;
using InfraAgenda = fyd.backend.Infraestructura.General.Entidades.Agenda;
using fyd.backend.Dominio.General.Valores;
using fyd.backend.Dominio.Compras.Valores;
using fyd.backend.Dominio.Compras.Parametros;

namespace fyd.backend.Infraestructura.Compras.Mapeos
{
    public static class ProveedorMapeo
    {
        public static InfraProveedor AInfraestructura(DominioProveedor dominio)
        {
            return new InfraProveedor
            {
                Id = dominio.Id,
                Codigo = dominio.Codigo,
                AgendaId = dominio.AgendaId,
                ProveedorCcId = dominio.ProveedorCcId,
                IvaCategoriaId = dominio.IvaCategoriaId,
                CondicionId = dominio.CondicionId,
                CategoriaId = dominio.CategoriaId,
                CalificacionId = dominio.CalificacionId,
                CuentaId = dominio.CuentaId,
                MonedaId = dominio.MonedaId,
                PorcDescuento1 = dominio.PorcDescuento1,
                PorcDescuento2 = dominio.PorcDescuento2,
                Origen = (int)dominio.Origen,
                FechaAlta = dominio.FechaAlta,
                FechaBaja = dominio.FechaBaja,
                Situacion = (int)dominio.Situacion,
                ImpCredito = dominio.ImpCredito,
                EventualOk = dominio.EventualOk,
                SujetoVinculadoOk = dominio.SujetoVinculadoOk,
                Lineas = dominio.Lineas.Select(l => new Entidades.ProveedorLinea { LineaId = l }).ToList(),
                Retenciones = dominio.Retenciones.Select(r => new Entidades.ProveedorRetencion {
                    RetencionId = r.RetencionId,
                    EmpresaId = r.EmpresaId,
                    ExencionDesde = r.ExencionDesde,
                    ExencionHasta = r.ExencionHasta
                }).ToList(),
                Agenda = dominio.AgendaId <= 0 && dominio.InfoAgenda != null ? new InfraAgenda {
                    EmpresaId = dominio.InfoAgenda.EmpresaId,
                    Nombre = dominio.InfoAgenda.Nombre,
                    Nombre2 = dominio.InfoAgenda.Nombre2,
                    Titulo = dominio.InfoAgenda.Titulo,
                    Calle = dominio.InfoAgenda.Calle,
                    Altura = dominio.InfoAgenda.Altura,
                    PisoDpto = dominio.InfoAgenda.PisoDpto,
                    Barrio = dominio.InfoAgenda.Barrio,
                    LocalidadId = dominio.InfoAgenda.LocalidadId,
                    Cp = dominio.InfoAgenda.Cp,
                    TipoDocId = dominio.InfoAgenda.TipoDocId,
                    NumeroDoc = dominio.InfoAgenda.NumeroDoc,
                    Ib = dominio.InfoAgenda.Ib,
                    Email1 = dominio.InfoAgenda.Email1,
                    Email2 = dominio.InfoAgenda.Email2,
                    Email3 = dominio.InfoAgenda.Email3,
                    Email4 = dominio.InfoAgenda.Email4,
                    Web = dominio.InfoAgenda.Web,
                    TipoTelefono1 = dominio.InfoAgenda.TipoTelefono1,
                    Telefono1 = dominio.InfoAgenda.Telefono1,
                    TipoTelefono2 = dominio.InfoAgenda.TipoTelefono2,
                    Telefono2 = dominio.InfoAgenda.Telefono2,
                    TipoTelefono3 = dominio.InfoAgenda.TipoTelefono3,
                    Telefono3 = dominio.InfoAgenda.Telefono3,
                    TipoTelefono4 = dominio.InfoAgenda.TipoTelefono4,
                    Telefono4 = dominio.InfoAgenda.Telefono4,
                    TipoTelefono5 = dominio.InfoAgenda.TipoTelefono5,
                    Telefono5 = dominio.InfoAgenda.Telefono5,
                    Variable1 = dominio.InfoAgenda.Variable1,
                    Variable2 = dominio.InfoAgenda.Variable2,
                    Variable3 = dominio.InfoAgenda.Variable3,
                    Variable4 = dominio.InfoAgenda.Variable4,
                    CorrespondenciaOk = dominio.InfoAgenda.CorrespondenciaOk,
                    CorrespondenciaId = dominio.InfoAgenda.CorrespondenciaId,
                    Atencion = dominio.InfoAgenda.Atencion,
                    CliDomEntregaOk = dominio.InfoAgenda.CliDomEntregaOk,
                    Memo = dominio.InfoAgenda.Memo
                } : null
            };
        }

        public static DominioProveedor ADominio(InfraProveedor infra)
        {
            var rpe = DominioProveedor.Crear(new ProveedorParametros
            {
                Codigo = infra.Codigo,
                AgendaId = infra.AgendaId,
                ProveedorCcId = infra.ProveedorCcId,
                IvaCategoriaId = infra.IvaCategoriaId,
                CondicionId = infra.CondicionId,
                CategoriaId = infra.CategoriaId,
                CalificacionId = infra.CalificacionId,
                CuentaId = infra.CuentaId,
                MonedaId = infra.MonedaId,
                PorcDescuento1 = infra.PorcDescuento1,
                PorcDescuento2 = infra.PorcDescuento2,
                Origen = (fyd.backend.Dominio.Compras.Enums.OrigenProveedor)infra.Origen,
                FechaAlta = infra.FechaAlta,
                FechaBaja = infra.FechaBaja,
                Situacion = (fyd.backend.Dominio.Compras.Enums.SituacionProveedor)infra.Situacion,
                ImpCredito = infra.ImpCredito,
                EventualOk = infra.EventualOk,
                SujetoVinculadoOk = infra.SujetoVinculadoOk,
                Lineas = infra.Lineas?.Select(l => l.LineaId).ToList() ?? new System.Collections.Generic.List<int>(),
                Retenciones = infra.Retenciones?.Select(r => new InfoRetencion(r.RetencionId, r.EmpresaId, r.ExencionDesde, r.ExencionHasta)).ToList() ?? new System.Collections.Generic.List<InfoRetencion>(),
                InfoAgenda = infra.Agenda != null ? new InfoAgenda(
                    infra.Agenda.EmpresaId, infra.Agenda.Nombre, infra.Agenda.Nombre2, infra.Agenda.Titulo,
                    infra.Agenda.Calle, infra.Agenda.Altura, infra.Agenda.PisoDpto, infra.Agenda.Barrio,
                    infra.Agenda.LocalidadId, infra.Agenda.Cp, infra.Agenda.TipoDocId, infra.Agenda.NumeroDoc,
                    infra.Agenda.Ib, infra.Agenda.Email1, infra.Agenda.Email2, infra.Agenda.Email3, infra.Agenda.Email4,
                    infra.Agenda.Web, infra.Agenda.TipoTelefono1, infra.Agenda.Telefono1, infra.Agenda.TipoTelefono2,
                    infra.Agenda.Telefono2, infra.Agenda.TipoTelefono3, infra.Agenda.Telefono3, infra.Agenda.TipoTelefono4,
                    infra.Agenda.Telefono4, infra.Agenda.TipoTelefono5, infra.Agenda.Telefono5, infra.Agenda.Variable1,
                    infra.Agenda.Variable2, infra.Agenda.Variable3, infra.Agenda.Variable4, infra.Agenda.CorrespondenciaOk,
                    infra.Agenda.CorrespondenciaId, infra.Agenda.Atencion, infra.Agenda.CliDomEntregaOk, infra.Agenda.Memo
                ) : null
            });

            var resultado = rpe.Valor!;
            resultado.Id = infra.Id;
            return resultado;
        }
    }
}
