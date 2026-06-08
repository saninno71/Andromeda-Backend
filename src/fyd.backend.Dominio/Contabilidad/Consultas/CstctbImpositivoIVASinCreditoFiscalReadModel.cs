using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoIVASinCreditoFiscalReadModel
    {
        [Key]
        public int ID { get; set; }
        public int Codigo { get; set; }
        public string IVAGrupo { get; set; } = string.Empty;
        public decimal CreditoFiscalTotal { get; set; }
    }
}
