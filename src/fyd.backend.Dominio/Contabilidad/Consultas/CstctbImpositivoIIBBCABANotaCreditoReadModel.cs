using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoIIBBCABANotaCreditoReadModel
    {
        [Key]
        public int TipoOperacion { get; set; }
        public string Numero { get; set; } = string.Empty;
        public int Fecha { get; set; }
        public decimal Monto { get; set; }
        public string? CertificadoPropio { get; set; }
        public string ComprobanteOriginalTipo { get; set; } = string.Empty;
        public string? Letra { get; set; }
        public string ComprobanteOriginalNumero { get; set; } = string.Empty;
        public string ClienteNumeroDoc { get; set; } = string.Empty;
        public string? CodigoRegimenOriginal { get; set; }
        public int? FechaEmisionOriginal { get; set; }
        public decimal ImporteRetPerc { get; set; }
        public decimal PorcImpuesto { get; set; }
    }
}
