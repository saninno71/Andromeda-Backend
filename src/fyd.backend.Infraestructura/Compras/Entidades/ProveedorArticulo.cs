using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Compras.Entidades
{
    [Table("cpsProveedorArticulos")]
    public class ProveedorArticulo
    {
        public ProveedorArticulo() { }

        [Key]
        public int Id { get; set; }
        public int ProveedorId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public int? ArticuloId { get; set; }
        public decimal Precio { get; set; }
        public float? PorcDescuento1 { get; set; }
        public float? PorcDescuento2 { get; set; }
        public DateTime? FechaPrecio { get; set; }
        public bool? ProveedorElegidoOk { get; set; }

        public virtual Proveedor? Proveedor { get; set; }
    }
}
