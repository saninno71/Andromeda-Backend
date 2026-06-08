using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Contabilidad.Entidades
{
    [Table("ctbAsientosComprobantes")]
    public class AsientoComprobante
    {
        public AsientoComprobante() { }

        [Key]
        public int Id { get; set; }
        public int? ComprobanteId { get; set; }
        public int? AsientoId { get; set; }
    }
}
