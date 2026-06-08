using fyd.backend.Aplicacion.Ventas.DTOs;
using fyd.backend.Dominio.Ventas.Entidades;
using fyd.backend.Dominio.Ventas.Parametros;

namespace fyd.backend.Aplicacion.Ventas.Mapeos
{
    public static class ClienteDtoMapeo
    {
        public static ClienteParametros AlParametro(CrearClienteDto dto, int agendaId)
        {
            return new ClienteParametros
            {
                Codigo = dto.Codigo ?? 0, // Se autonumerará en el servicio si es 0
                AgendaId = agendaId,
                ClienteCcId = dto.ClienteCcId,
                VendedorId = dto.VendedorId,
                CobradorId = dto.CobradorId,
                IvaCategoriaId = dto.IvaCategoriaId,
                CondicionId = dto.CondicionId,
                CategoriaId = dto.CategoriaId,
                ZonaId = dto.ZonaId,
                IibbProvinciaId = dto.IibbProvinciaId,
                ListaId = dto.ListaId,
                CalificacionId = dto.CalificacionId,
                CuentaId = dto.CuentaId,
                TransporteId = dto.TransporteId,
                DomEntregaId = dto.DomEntregaId ?? agendaId,
                DepositoId = dto.DepositoId,
                PorcDescuento1 = dto.PorcDescuento1,
                PorcDescuento2 = dto.PorcDescuento2,
                PorcDescuento3 = dto.PorcDescuento3,
                PorcIvaLiberado = dto.PorcIvaLiberado,
                FechaAlta = dto.FechaAlta,
                FechaBaja = dto.FechaBaja,
                Situacion = dto.Situacion,
                ImpCredito = dto.ImpCredito,
                MonedaId = dto.MonedaId,
                EventualOk = dto.EventualOk,
                SujetoVinculadoOk = dto.SujetoVinculadoOk,
                RazonSocial = dto.Nombre 
            };
        }

        public static ClienteParametros AlParametro(ActualizarClienteDto dto, int agendaId)
        {
            return new ClienteParametros
            {
                Codigo = dto.Codigo ?? 0,
                AgendaId = agendaId,
                ClienteCcId = dto.ClienteCcId,
                VendedorId = dto.VendedorId,
                CobradorId = dto.CobradorId,
                IvaCategoriaId = dto.IvaCategoriaId,
                CondicionId = dto.CondicionId,
                CategoriaId = dto.CategoriaId,
                ZonaId = dto.ZonaId,
                IibbProvinciaId = dto.IibbProvinciaId,
                ListaId = dto.ListaId,
                CalificacionId = dto.CalificacionId,
                CuentaId = dto.CuentaId,
                TransporteId = dto.TransporteId,
                DomEntregaId = dto.DomEntregaId ?? agendaId,
                DepositoId = dto.DepositoId,
                PorcDescuento1 = dto.PorcDescuento1,
                PorcDescuento2 = dto.PorcDescuento2,
                PorcDescuento3 = dto.PorcDescuento3,
                PorcIvaLiberado = dto.PorcIvaLiberado,
                FechaAlta = dto.FechaAlta,
                FechaBaja = dto.FechaBaja,
                Situacion = dto.Situacion,
                ImpCredito = dto.ImpCredito,
                MonedaId = dto.MonedaId,
                EventualOk = dto.EventualOk,
                SujetoVinculadoOk = dto.SujetoVinculadoOk,
                RazonSocial = dto.Nombre
            };
        }

        public static ConsultarClienteDto A_ConsultarDto(Cliente cliente, string nombreDeAgenda = "")
        {
            if (cliente == null) return null!;

            return new ConsultarClienteDto(
                Id: cliente.Id,
                Codigo: cliente.Codigo,
                Nombre: nombreDeAgenda, // Para completarse utilizando datos de la Agenda compartida
                AgendaId: cliente.AgendaId,
                ClienteCcId: cliente.ClienteCcId,
                VendedorId: cliente.VendedorId,
                CobradorId: cliente.CobradorId,
                IvaCategoriaId: cliente.IvaCategoriaId,
                CondicionId: cliente.CondicionId,
                CategoriaId: cliente.CategoriaId,
                ZonaId: cliente.ZonaId,
                IibbProvinciaId: cliente.IibbProvinciaId,
                ListaId: cliente.ListaId,
                CalificacionId: cliente.CalificacionId,
                CuentaId: cliente.CuentaId,
                TransporteId: cliente.TransporteId,
                DomEntregaId: cliente.DomEntregaId,
                DepositoId: cliente.DepositoId,
                PorcDescuento1: cliente.PorcDescuento1,
                PorcDescuento2: cliente.PorcDescuento2,
                PorcDescuento3: cliente.PorcDescuento3,
                PorcIvaLiberado: cliente.PorcIvaLiberado,
                FechaAlta: cliente.FechaAlta,
                FechaBaja: cliente.FechaBaja,
                Situacion: cliente.Situacion,
                ImpCredito: cliente.ImpCredito,
                MonedaId: cliente.MonedaId,
                EventualOk: cliente.EventualOk,
                SujetoVinculadoOk: cliente.SujetoVinculadoOk
            );
        }
    }
}
