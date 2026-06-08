using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.General.Entidades
{
    [Table("gnrAgenda")]
    public class Agenda
    {
        public Agenda() { }

        [Key]
        public int Id { get; set; }
        public int? EmpresaId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Nombre2 { get; set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Calle { get; set; } = string.Empty;

        public int? Altura { get; set; }

        [Required]
        [MaxLength(10)]
        public string PisoDpto { get; set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        public string Barrio { get; set; } = string.Empty;

        public int LocalidadId { get; set; }

        [Required]
        [MaxLength(8)]
        public string Cp { get; set; } = string.Empty;

        public int TipoDocId { get; set; }

        [Required]
        [MaxLength(11)]
        public string NumeroDoc { get; set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        public string Ib { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Email1 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Email2 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Email3 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Email4 { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Web { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string TipoTelefono1 { get; set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        public string Telefono1 { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string TipoTelefono2 { get; set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        public string Telefono2 { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string TipoTelefono3 { get; set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        public string Telefono3 { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string TipoTelefono4 { get; set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        public string Telefono4 { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string TipoTelefono5 { get; set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        public string Telefono5 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Variable1 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Variable2 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Variable3 { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Variable4 { get; set; } = string.Empty;

        public bool CorrespondenciaOk { get; set; }
        public int CorrespondenciaId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Atencion { get; set; } = string.Empty;

        public bool CliDomEntregaOk { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Memo { get; set; } = string.Empty;
    }
}
