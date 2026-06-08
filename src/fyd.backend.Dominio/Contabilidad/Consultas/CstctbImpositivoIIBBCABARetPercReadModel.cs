using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoIIBBCABARetPercReadModel
    {
        [Key]
        public int TipoOperacion { get; set; }
        public string? CodigoRegimen { get; set; }
        public int Fecha { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string? Letra { get; set; }
        public string Numero { get; set; } = string.Empty;
        public int? FechaEmision { get; set; }
        public decimal Monto { get; set; }
        public string? CertificadoPropio { get; set; }
        public int DocTipo { get; set; }
        public string DocNumero { get; set; } = string.Empty;
        public int SituacionIB { get; set; }
        public string IB { get; set; } = string.Empty;
        public int SituacionIVA { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public decimal OtrosConceptos { get; set; }
        public decimal IVA { get; set; }
        public decimal MontoImponible { get; set; }
        public decimal PorcImpuesto { get; set; }
        public decimal ImporteRetPerc { get; set; }
        public string? Aceptacion { get; set; }
        public string? AceptacionFecha { get; set; }
    }
}
