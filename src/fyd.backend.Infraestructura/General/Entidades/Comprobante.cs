using fyd.backend.Infraestructura.Contabilidad.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.General.Entidades
{
    [Table("gnrComprobantes")]
    public class Comprobante
    {
        public Comprobante() { }

        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaEmision { get; set; }
        public int NumeraTipoId { get; set; }
        public int Numero { get; set; }
        public int EmpresaId { get; set; }
        public int MonedaId { get; set; }
        public decimal CotizacionLocal { get; set; }
        public decimal CotizacionReferencia { get; set; }
        public decimal ImpTotal { get; set; }

        [Required]
        [MaxLength(50)]
        public string Detalle { get; set; } = string.Empty;

        [Column(TypeName = "varchar(max)")]
        public string? Memo { get; set; }

        public int? AsientoId { get; set; }

        public virtual ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();
    }
}
