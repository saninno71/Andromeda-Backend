using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Identidad.Entidades
{
    [Table("Permisos")]
    public class Permiso
    {
        [Key]
        public int Id { get; set; }

        [Required] //TODO: definir longitud.
        public string Nombre { get; set; } = string.Empty; // Ej: "Clientes.Consultar"
        [Required]
        public string Modulo { get; set; } = string.Empty; // Ej: "Ventas"        
        public string Descripcion { get; set; } = string.Empty;
    }
}
