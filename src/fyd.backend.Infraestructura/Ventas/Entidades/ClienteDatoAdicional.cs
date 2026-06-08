using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Ventas.Entidades
{
    [Table("vtsCliDatosAdicionales")]
    public class ClienteDatoAdicional
    {
        public ClienteDatoAdicional() { }

        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Dato01 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Dato02 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Dato03 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Dato04 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Dato05 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Dato06 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Dato07 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Dato08 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Dato09 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Dato10 { get; set; } = string.Empty;
    }
}
