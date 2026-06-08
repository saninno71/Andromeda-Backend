using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Compras.Entidades
{
    [Table("cpsProLineas")]
    public class ProveedorLinea
    {
        public ProveedorLinea() { }

        [Key]
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public int LineaId { get; set; }

        public virtual Proveedor? Proveedor { get; set; }
    }
}
