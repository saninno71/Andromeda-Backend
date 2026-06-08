using fyd.backend.Infraestructura.General.Entidades;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Contabilidad.Entidades
{
    [Table("ctbAsientos")]
    public class Asiento
    {
        public Asiento() { }

        [Key]
        public int Id { get; set; }
        public int ComprobanteId { get; set; }
        public int CuentaId { get; set; }
        public int? ClienteId { get; set; }
        public int? ProveedorId { get; set; }
        public int? CajaId { get; set; }
        public int Tipo { get; set; }

        [Precision(18, 4)]
        public decimal Importe { get; set; }

        [Precision(18, 4)]
        public decimal ImpLocal { get; set; }

        [Precision(18, 4)]
        public decimal ImpReferencia { get; set; }

        [Required]
        [MaxLength(50)]
        public string Detalle { get; set; } = string.Empty;

        public bool EliminarOk { get; set; }

        [ForeignKey("ComprobanteId")]
        public virtual Comprobante? Comprobante { get; set; }
    }
}
