using fyd.backend.Infraestructura.General.Entidades;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Compras.Entidades
{
    [Table("cpsComprobantes")]
    public class CompraComprobante
    {
        public CompraComprobante() { }

        [Key]
        public int Id { get; set; }
        public int ComprobanteId { get; set; }

        [Required]
        [MaxLength(1)]
        public string Letra { get; set; } = string.Empty;

        public int PuntoVenta { get; set; }
        public int? ProveedorId { get; set; }
        public int? ProveedorCcId { get; set; }
        public int IvaCategoriaId { get; set; }
        public int? CondicionId { get; set; }
        public float PorcDescuento1 { get; set; }
        public float PorcDescuento2 { get; set; }
        public float PorcDescuento3 { get; set; }
        public decimal ImpDescuento1 { get; set; }
        public decimal ImpDescuento2 { get; set; }
        public decimal ImpDescuento3 { get; set; }
        public decimal ImpTotal { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int? DespachoId { get; set; }

        [Required]
        [MaxLength(25)]
        public string OCompra { get; set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        public string Solicitante { get; set; } = string.Empty;

        public int Estado { get; set; }

        [Required]
        [MaxLength(14)]
        public string CodigoAutorizacion { get; set; } = string.Empty;

        public DateTime? VencCodigoAutorizacion { get; set; }
        public int? CajaId { get; set; }
        public int? AsientoId { get; set; }
        public int? SvTipoOperacionId { get; set; }

        [ForeignKey("ComprobanteId")]
        public virtual Comprobante? ComprobanteGeneral { get; set; }
    }
}
