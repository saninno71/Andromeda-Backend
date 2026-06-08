using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoSiFeReRetencionReadModel
    {
        [Key]
        public int ID { get; set; }
        public int? JurisdiccionIIBB { get; set; }
        public string NumeroDoc { get; set; } = string.Empty;
        public DateTime? Fecha { get; set; }
        public int PuntoVentaCertificado { get; set; }
        public string? Certificado { get; set; }
        public string CodigoFiscal { get; set; } = string.Empty;
        public string? Letra { get; set; }
        public string Numero { get; set; } = string.Empty;
        public decimal ImporteLocal { get; set; }
    }
}
