using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Compras.Entidades
{
    [Table("cpsCtasCtes")]
    public class CompraCuentaCorriente
    {
        public CompraCuentaCorriente() { }

        [Key]
        public int Id { get; set; }
        public int CompraId { get; set; }
        public int CuotaNumero { get; set; }
        public DateTime FechaVto { get; set; }
        public decimal Importe { get; set; }

        public virtual CompraComprobante? Compra { get; set; }
    }
}
