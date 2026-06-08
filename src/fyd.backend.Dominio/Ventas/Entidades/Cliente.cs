using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Entidades;
using fyd.backend.Dominio.Ventas.Enums;
using fyd.backend.Dominio.Ventas.Errores;
using fyd.backend.Dominio.Ventas.Parametros;
using System;

namespace fyd.backend.Dominio.Ventas.Entidades
{
    public class Cliente
    {
        public int Id { get; set; } // Permitir asignación durante el mapeo de hidratación
        public int Codigo { get; private set; }
        public int AgendaId { get; private set; }
        public int? ClienteCcId { get; private set; }
        public int VendedorId { get; private set; }
        public int CobradorId { get; private set; }
        public int IvaCategoriaId { get; private set; }
        public int CondicionId { get; private set; }
        public int CategoriaId { get; private set; }
        public int ZonaId { get; private set; }
        public int? IibbProvinciaId { get; private set; }
        public int? ListaId { get; private set; }
        public int CalificacionId { get; private set; }
        public int? CuentaId { get; private set; }
        public int TransporteId { get; private set; }
        public int DomEntregaId { get; private set; }
        public int? DepositoId { get; private set; }
        public float PorcDescuento1 { get; private set; }
        public float PorcDescuento2 { get; private set; }
        public float PorcDescuento3 { get; private set; }
        public float PorcIvaLiberado { get; private set; }
        public DateTime? FechaAlta { get; private set; }
        public DateTime? FechaBaja { get; private set; }
        public SituacionCliente Situacion { get; private set; }
        public decimal ImpCredito { get; private set; }
        public int MonedaId { get; private set; }
        public bool EventualOk { get; private set; }
        public bool SujetoVinculadoOk { get; private set; }

        private Cliente() { }

        public static Resultado<Cliente> Crear(ClienteParametros param)
        {
            var resultado = ValidarInvariantes(param);
            if (!resultado.Exitoso)
            {
                return Resultado<Cliente>.Falla(resultado.Error!);
            }

            var cliente = new Cliente
            {
                Codigo = param.Codigo,
                AgendaId = param.AgendaId,
                ClienteCcId = param.ClienteCcId,
                VendedorId = param.VendedorId,
                CobradorId = param.CobradorId,
                IvaCategoriaId = param.IvaCategoriaId,
                CondicionId = param.CondicionId,
                CategoriaId = param.CategoriaId,
                ZonaId = param.ZonaId,
                IibbProvinciaId = param.IibbProvinciaId,
                ListaId = param.ListaId,
                CalificacionId = param.CalificacionId,
                CuentaId = param.CuentaId,
                TransporteId = param.TransporteId,
                DomEntregaId = param.DomEntregaId == 0 ? param.AgendaId : param.DomEntregaId, // INV-04
                DepositoId = param.DepositoId,
                PorcDescuento1 = param.PorcDescuento1,
                PorcDescuento2 = param.PorcDescuento2,
                PorcDescuento3 = param.PorcDescuento3,
                PorcIvaLiberado = param.PorcIvaLiberado,
                FechaAlta = param.FechaAlta,
                FechaBaja = param.FechaBaja,
                Situacion = param.Situacion,
                ImpCredito = param.ImpCredito,
                MonedaId = param.MonedaId,
                EventualOk = param.EventualOk,
                SujetoVinculadoOk = param.SujetoVinculadoOk
            };

            return Resultado<Cliente>.Exito(cliente);
        }

        public Resultado Actualizar(ClienteParametros param)
        {
            var resultado = ValidarInvariantes(param);
            if (!resultado.Exitoso)
            {
                return resultado;
            }

            Codigo = param.Codigo;
            AgendaId = param.AgendaId;
            ClienteCcId = param.ClienteCcId;
            VendedorId = param.VendedorId;
            CobradorId = param.CobradorId;
            IvaCategoriaId = param.IvaCategoriaId;
            CondicionId = param.CondicionId;
            CategoriaId = param.CategoriaId;
            ZonaId = param.ZonaId;
            IibbProvinciaId = param.IibbProvinciaId;
            ListaId = param.ListaId;
            CalificacionId = param.CalificacionId;
            CuentaId = param.CuentaId;
            TransporteId = param.TransporteId;
            DomEntregaId = param.DomEntregaId == 0 ? param.AgendaId : param.DomEntregaId;
            DepositoId = param.DepositoId;
            PorcDescuento1 = param.PorcDescuento1;
            PorcDescuento2 = param.PorcDescuento2;
            PorcDescuento3 = param.PorcDescuento3;
            PorcIvaLiberado = param.PorcIvaLiberado;
            FechaAlta = param.FechaAlta;
            FechaBaja = param.FechaBaja;
            Situacion = param.Situacion;
            ImpCredito = param.ImpCredito;
            MonedaId = param.MonedaId;
            EventualOk = param.EventualOk;
            SujetoVinculadoOk = param.SujetoVinculadoOk;

            return Resultado.Exito();
        }

        private static Resultado ValidarInvariantes(ClienteParametros param)
        {
            // INV-01
            if (param.Codigo == 0)
                return Resultado.Falla(ClienteError.CodigoInvalido(param.RazonSocial));

            // INV-02
            if (string.IsNullOrWhiteSpace(param.RazonSocial))
                return Resultado.Falla(ClienteError.RazonSocialVacia);

            // INV-03
            if (param.FechaAlta.HasValue && param.FechaBaja.HasValue && param.FechaAlta.Value > param.FechaBaja.Value)
                return Resultado.Falla(ClienteError.FechaAltaPosteriorABaja);

            // INV-06
            if (!Enum.IsDefined(typeof(SituacionCliente), param.Situacion))
                return Resultado.Falla(ClienteError.SituacionInvalida);

            // INV-07
            if (param.PorcDescuento1 < 0 || param.PorcDescuento2 < 0 || param.PorcDescuento3 < 0)
                return Resultado.Falla(ClienteError.DescuentoNegativo);

            // INV-08
            if (param.PorcIvaLiberado < 0 || param.PorcIvaLiberado > 100)
                return Resultado.Falla(ClienteError.PorcentajeIvaLiberadoInvalido);

            // INV-09
            if (param.ImpCredito < 0)
                return Resultado.Falla(ClienteError.CreditoNegativo);

            return Resultado.Exito();
        }
    }
}
