using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyd.backend.Infraestructura.General.Entidades
{
    [Table("gnrNumeraTipos")]
    public class NumeraTipo
    {
        public NumeraTipo() { }

        [Key]
        public int Id { get; set; }

        [MaxLength(25)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(5)]
        public string Simbolo { get; set; } = string.Empty;

        public int? NumeracionId { get; set; }
        public int? Tipo { get; set; }

        [MaxLength(3)]
        public string CodigoFiscal { get; set; } = string.Empty;

        public bool? EliminarOk { get; set; }
        public bool? ControladorFiscalOk { get; set; }
        public int? NumeraTipoIdVinculado { get; set; }
        public int? ComprobanteCodigoFiscal { get; set; }
    }
}
