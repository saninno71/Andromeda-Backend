using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoIVAGrupoReadModel
    {
        [Key]
        public int ID { get; set; }
        public int Codigo { get; set; }
        public string IVAGrupo { get; set; } = string.Empty;
        public float? PorcIVA { get; set; }
        public double? Importe { get; set; }
        public decimal CreditoFiscalTotal { get; set; }
    }
}
