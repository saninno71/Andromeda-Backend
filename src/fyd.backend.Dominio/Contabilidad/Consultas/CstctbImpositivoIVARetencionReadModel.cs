using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoIVARetencionReadModel
    {
        [Key]
        public int ID { get; set; }
        public string? CodigoRegimen { get; set; }
        public string ClienteNumeroDoc { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Certificado { get; set; }
        public decimal ImporteLocal { get; set; }        
    }
}
