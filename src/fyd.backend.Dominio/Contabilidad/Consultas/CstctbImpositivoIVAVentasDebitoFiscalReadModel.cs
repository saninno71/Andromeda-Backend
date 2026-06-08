using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoIVAVentasDebitoFiscalReadModel
    {
        [Key]
        public int ID { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Cuenta { get; set; } = string.Empty;
        public int ImporteID { get; set; }
        public int TipoCalculo { get; set; }
        public string IVACategoriaNombre { get; set; } = string.Empty;
        public float Tasa { get; set; }
        public decimal ImporteLocal { get; set; }
        public double ImpIVA { get; set; }
         public double ImpNeto { get; set; }
    }
}
