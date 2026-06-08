using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoSiCoReRetPercReadModel
    {
        [Key]
        public int ID { get; set; }
        public string? Tipo { get; set; }
        public string? Simbolo { get; set; }
        public int Fecha { get; set; }
        public string Numero { get; set; } = string.Empty;
        public decimal Importe { get; set; }
        public string? CodigoImpuesto { get; set; }
        public string? CodigoRegimen { get; set; }
        public string? CodigoOperacion { get; set; }
        public decimal BaseImponible { get; set; }
        public string? CodigoCondicion { get; set; }
        public string? RetSujSusp { get; set; }
        public decimal ImporteRetencion { get; set; }
        public string? PorcExclusion { get; set; }
        public string? FechaBoletin { get; set; }
        public string? DocTipo { get; set; }
        public string DocNumero { get; set; } = string.Empty;
        public string? CertificadoOriginal { get; set; }
        public string? DenominacionOrdenante { get; set; }
        public string? Acrecentamiento { get; set; }
        public string? CUITPais { get; set; }
        public string? CUITOrdenante { get; set; }
    }
}
