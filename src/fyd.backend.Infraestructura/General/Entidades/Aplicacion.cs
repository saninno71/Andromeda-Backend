using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.General.Entidades
{
    [Table("gnrAplicaciones")]
    public class Aplicacion
    {
        public Aplicacion() { }

        [Key]
        public int Id { get; set; }
        public int? ComprobanteId { get; set; }
        public int? VentaCtaCteIdAplicador { get; set; }
        public int? VentaCtaCteIdAfectado { get; set; }
        public int? CompraCtaCteIdAplicador { get; set; }
        public int? CompraCtaCteIdAfectado { get; set; }
        public int? ComisionCtaCteIdAplicador { get; set; }
        public int? ComisionCtaCteIdAfectado { get; set; }
        public int? ValorIdAfectado { get; set; }

        [Column(TypeName = "money")]
        public decimal? ImpAplicador { get; set; }

        [Column(TypeName = "money")]
        public decimal? ImpAfectado { get; set; }
    }
}
