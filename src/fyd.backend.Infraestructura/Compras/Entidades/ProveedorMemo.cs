using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Compras.Entidades
{
    [Table("cpsProMemos")]
    public class ProveedorMemo
    {
        public ProveedorMemo() { }

        [Key]
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public DateTime FechaConsulta { get; set; }

        [Required]
        [MaxLength(25)]
        public string Contacto { get; set; } = string.Empty;

        public DateTime? FechaAviso { get; set; }

        [Required]
        public string Memo { get; set; } = string.Empty;

        public virtual Proveedor? Proveedor { get; set; }
    }
}
