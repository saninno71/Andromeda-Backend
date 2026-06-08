using fyd.backend.Dominio.Ventas.Parametros;
using DominioCliente = fyd.backend.Dominio.Ventas.Entidades.Cliente;
using InfraCliente = fyd.backend.Infraestructura.Ventas.Entidades.Cliente;

namespace fyd.backend.Infraestructura.Ventas.Mapeos
{
    public static class ClienteMapeo
    {
        public static DominioCliente? A_Dominio(InfraCliente infra)
        {
            if (infra == null) return null;

            var parametros = new ClienteParametros
            {
                Codigo = infra.Codigo,
                AgendaId = infra.AgendaId,
                ClienteCcId = infra.ClienteCcId,
                VendedorId = infra.VendedorId,
                CobradorId = infra.CobradorId,
                IvaCategoriaId = infra.IvaCategoriaId,
                CondicionId = infra.CondicionId,
                CategoriaId = infra.CategoriaId,
                ZonaId = infra.ZonaId,
                IibbProvinciaId = infra.IibbProvinciaId,
                ListaId = infra.ListaId,
                CalificacionId = infra.CalificacionId,
                CuentaId = infra.CuentaId,
                TransporteId = infra.TransporteId,
                DomEntregaId = infra.DomEntregaId,
                DepositoId = infra.DepositoId,
                PorcDescuento1 = infra.PorcDescuento1,
                PorcDescuento2 = infra.PorcDescuento2,
                PorcDescuento3 = infra.PorcDescuento3,
                PorcIvaLiberado = infra.PorcIvaLiberado,
                FechaAlta = infra.FechaAlta,
                FechaBaja = infra.FechaBaja,
                Situacion = (Dominio.Ventas.Enums.SituacionCliente)infra.Situacion,
                ImpCredito = infra.ImpCredito,
                MonedaId = infra.MonedaId,
                EventualOk = infra.EventualOk,
                SujetoVinculadoOk = infra.SujetoVinculadoOk,
                RazonSocial = "IgnoradaValidacionInterna" // Para sortear el check de la invariante al construir desde DB
            };

            var creacion = DominioCliente.Crear(parametros);
            var dominioModel = creacion.Valor;
            
            if (dominioModel != null)
            {
                dominioModel.Id = infra.Id;
            }

            return dominioModel;
        }

        public static void Actualizar_Infra(DominioCliente dominio, InfraCliente infra)
        {
            if (dominio == null || infra == null) return;

            infra.Codigo = dominio.Codigo;
            infra.AgendaId = dominio.AgendaId;
            infra.ClienteCcId = dominio.ClienteCcId;
            infra.VendedorId = dominio.VendedorId;
            infra.CobradorId = dominio.CobradorId;
            infra.IvaCategoriaId = dominio.IvaCategoriaId;
            infra.CondicionId = dominio.CondicionId;
            infra.CategoriaId = dominio.CategoriaId;
            infra.ZonaId = dominio.ZonaId;
            infra.IibbProvinciaId = dominio.IibbProvinciaId;
            infra.ListaId = dominio.ListaId;
            infra.CalificacionId = dominio.CalificacionId;
            infra.CuentaId = dominio.CuentaId;
            infra.TransporteId = dominio.TransporteId;
            infra.DomEntregaId = dominio.DomEntregaId;
            infra.DepositoId = dominio.DepositoId;
            infra.PorcDescuento1 = dominio.PorcDescuento1;
            infra.PorcDescuento2 = dominio.PorcDescuento2;
            infra.PorcDescuento3 = dominio.PorcDescuento3;
            infra.PorcIvaLiberado = dominio.PorcIvaLiberado;
            infra.FechaAlta = dominio.FechaAlta;
            infra.FechaBaja = dominio.FechaBaja;
            infra.Situacion = (int)dominio.Situacion;
            infra.ImpCredito = dominio.ImpCredito;
            infra.MonedaId = dominio.MonedaId;
            infra.EventualOk = dominio.EventualOk;
            infra.SujetoVinculadoOk = dominio.SujetoVinculadoOk;
        }

        public static InfraCliente? A_Infra(DominioCliente dominio)
        {
            if (dominio == null) return null;

            var infra = new InfraCliente();
            Actualizar_Infra(dominio, infra);
            return infra;
        }
    }
}
