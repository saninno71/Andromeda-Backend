using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoIVARestitucionDebitoFiscalReadModel
    {
        [Key]
        public int ID { get; set; }
        public string Codigo { get; set; }
        public string Cuenta { get; set; } = string.Empty;
        public int ImporteID { get; set; }
        public int IVACategoria { get; set; }
        public string IVACategoriaNombre { get; set; } = string.Empty;
        public float Tasa { get; set; }
        public decimal ImporteLocal { get; set; }
        public double ImpIVA { get; set; }
        public double ImpNeto { get; set; } // o double?
    }
}
