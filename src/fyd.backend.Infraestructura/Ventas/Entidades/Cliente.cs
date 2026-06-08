using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Ventas.Entidades
{
    [Table("vtsClientes")]
    public class Cliente
    {
        public Cliente() { }

        [Key]
        public int Id { get; set; }
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
        public int Situacion { get; set; }
        public decimal ImpCredito { get; set; }
        public int MonedaId { get; set; }
        public bool EventualOk { get; set; } = false;
        public bool SujetoVinculadoOk { get; set; } = false;

        [ForeignKey("ClienteId")]
        public virtual ICollection<ClienteLinea> Lineas { get; set; } = new List<ClienteLinea>();
        
        [ForeignKey("ClienteId")]
        public virtual ICollection<ClientePercepcion> Percepciones { get; set; } = new List<ClientePercepcion>();
        
        [ForeignKey("ClienteId")]
        public virtual ICollection<ClienteMemo> Memos { get; set; } = new List<ClienteMemo>();
        
        [ForeignKey("ClienteId")]
        public virtual ClienteDatoAdicional? DatosAdicionales { get; set; }
    }
}
