using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Fondos.Entidades
{
    [Table("fdsMovimientos")]
    public class FondoMovimiento
    {
        public FondoMovimiento() { }

        [Key]
        public int Id { get; set; }
        public int ComprobanteId { get; set; }
        public int TipoValorId { get; set; }
        public int? ValorId { get; set; }
        public decimal Importe { get; set; }
        public int MonedaId { get; set; }
        public decimal Cotizacion { get; set; }
        public int CajaId { get; set; }
        public DateTime FechaExtracto { get; set; }
        public bool ConciliadoOk { get; set; }
        public int? CierreId { get; set; }
        public int Estado { get; set; }

        [ForeignKey("ComprobanteId")]
        public virtual FondoComprobante? FondoComprobante { get; set; }
    }
}
