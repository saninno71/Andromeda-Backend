using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Ventas.Entidades
{
    [Table("vtsCtasCtes")]
    public class VentaCuentaCorriente
    {
        public VentaCuentaCorriente() { }

        [Key]
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int CuotaNumero { get; set; }
        public DateTime FechaVto { get; set; }
        public decimal Importe { get; set; }

        public virtual VentaComprobante? Venta { get; set; }
    }
}
