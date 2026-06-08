using fyd.backend.Infraestructura.General.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Fondos.Entidades
{
    [Table("fdsComprobantes")]
    public class FondoComprobante
    {
        public FondoComprobante() { }

        [Key]
        public int Id { get; set; }
        public int ComprobanteId { get; set; }
        public int Clearing { get; set; }
        public decimal ImpTotal { get; set; }
        public int? AsientoId { get; set; }

        [ForeignKey("ComprobanteId")]
        public virtual Comprobante? ComprobanteGeneral { get; set; }
    }
}
