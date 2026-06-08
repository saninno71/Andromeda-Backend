using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.General.Entidades
{
    [Table("gnrImputaciones")]
    public class Imputacion
    {
        public Imputacion() { }

        [Key]
        public int Id { get; set; }
        public int ComprobanteId { get; set; }
        public int? TipoImporteId { get; set; }
        public int? CuentaId { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteLocal { get; set; }
        public decimal ImporteReferencia { get; set; }

        [MaxLength(50)]
        public string? Detalle { get; set; }

        public bool ContrapartidaOk { get; set; }
    }
}
