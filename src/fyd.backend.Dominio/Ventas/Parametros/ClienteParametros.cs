using fyd.backend.Dominio.Ventas.Enums;
using System;

namespace fyd.backend.Dominio.Ventas.Parametros
{
    public class ClienteParametros
    {
        public int Codigo { get; set; }
        public int AgendaId { get; set; }
        public int? ClienteCcId { get; set; }
        public int VendedorId { get; set; }
        public int CobradorId { get; set; }
        public int IvaCategoriaId { get; set; }
        public int CondicionId { get; set; }
        public int CategoriaId { get; set; }
        public int ZonaId { get; set; }
        public int? IibbProvinciaId { get; set; }
        public int? ListaId { get; set; }
        public int CalificacionId { get; set; }
        public int? CuentaId { get; set; }
        public int TransporteId { get; set; }
        public int DomEntregaId { get; set; }
        public int? DepositoId { get; set; }
        public float PorcDescuento1 { get; set; }
        public float PorcDescuento2 { get; set; }
        public float PorcDescuento3 { get; set; }
        public float PorcIvaLiberado { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public SituacionCliente Situacion { get; set; }
        public decimal ImpCredito { get; set; }
        public int MonedaId { get; set; }
        public bool EventualOk { get; set; }
        public bool SujetoVinculadoOk { get; set; }
        
        // Usado para validaciones de nombre obligatorio dentro de la entidad
        public string RazonSocial { get; set; } = string.Empty; 
    }
}
