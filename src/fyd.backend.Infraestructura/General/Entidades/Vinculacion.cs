using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.General.Entidades
{
    [Table("gnrVinculaciones")]
    public class Vinculacion
    {
        public Vinculacion() { }

        [Key]
        public int Id { get; set; }
        public int? AplicadorId { get; set; }
        public int? AjusteId { get; set; }
        public int? AsientoId { get; set; }
        public int? CobranzaId { get; set; }
    }
}
