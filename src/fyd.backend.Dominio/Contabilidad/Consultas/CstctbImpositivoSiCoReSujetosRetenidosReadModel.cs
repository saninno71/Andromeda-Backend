using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoSiCoReSujetosRetenidosReadModel
    {
        [Key]
        public int ID { get; set; }
        public string DocNumero { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Domicilio { get; set; }
        public string? Localidad { get; set; }
        public string? Provincia { get; set; }
        public string? CodigoPostal { get; set; }
        public string? DocTipo { get; set; }
    }
}
