using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Ventas.Entidades
{
    [Table("vtsCliLineas")]
    public class ClienteLinea
    {
        public ClienteLinea() { }

        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int LineaId { get; set; }
    }
}
