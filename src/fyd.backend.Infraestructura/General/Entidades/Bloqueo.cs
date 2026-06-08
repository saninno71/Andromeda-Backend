using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.General.Entidades
{
    [Table("sgrBloqueos")]
    public class Bloqueo
    {
        public Bloqueo() { }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Tabla { get; set; } = string.Empty;

        public int PeriodoId { get; set; }
    }
}
