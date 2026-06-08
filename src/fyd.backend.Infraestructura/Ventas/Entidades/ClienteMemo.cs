using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.Ventas.Entidades
{
    [Table("vtsCliMemos")]
    public class ClienteMemo
    {
        public ClienteMemo() { }

        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaConsulta { get; set; }

        [Required]
        [MaxLength(25)]
        public string Contacto { get; set; } = string.Empty;

        public DateTime? FechaAviso { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Memo { get; set; } = string.Empty;
    }
}
