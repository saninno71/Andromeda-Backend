using fyd.backend.Infraestructura.General.Entidades;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Ventas.Entidades
{
    [Table("vtsComprobantes")]
    public class VentaComprobante
    {
        public VentaComprobante() { }

        [Key]
        public int Id { get; set; }
        public int ComprobanteId { get; set; }
        public int ClienteId { get; set; }
        public int? ClienteCcId { get; set; }
        public int IvaCategoriaId { get; set; }
        public int LocalidadId { get; set; }
        public int? IibbProvinciaId { get; set; }
        public int? VendedorId { get; set; }
        public int? CobradorId { get; set; }
        public int? CondicionId { get; set; }
        public int? ListaId { get; set; }
        public float PorcDescuento1 { get; set; }
        public float PorcDescuento2 { get; set; }
        public float PorcDescuento3 { get; set; }
        public float PorcDescuento4 { get; set; }
        public decimal ImpDescuento1 { get; set; }
        public decimal ImpDescuento2 { get; set; }
        public decimal ImpDescuento3 { get; set; }
        public decimal ImpDescuento4 { get; set; }
        public decimal ImpTotal { get; set; }
        public DateTime? FechaEntrega { get; set; }

        [Required]
        [MaxLength(25)]
        public string OCompra { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Remito { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Solicitante { get; set; } = string.Empty;

        public int Estado { get; set; }
        public int? AnulacionMotivoId { get; set; }
        public bool? PendienteRemitirOk { get; set; }

        [Required]
        [MaxLength(14)]
        public string CodigoAutorizacion { get; set; } = string.Empty;

        public DateTime? VencCodigoAutorizacion { get; set; }
        public DateTime? FechaServicioDesde { get; set; }
        public DateTime? FechaServicioHasta { get; set; }
        public int? IncotermId { get; set; }
        public int? AsientoId { get; set; }
        public int? SvTipoOperacionId { get; set; }
        public int? ComprobanteOriginalId { get; set; }

        [ForeignKey("ComprobanteId")]
        public virtual Comprobante? ComprobanteGeneral { get; set; }
    }
}
