using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoLIDComprasImportacionAlicuotasReadModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime FechaEmision { get; set; }
        public string DespachoNumero { get; set; } = string.Empty;
        public double ImpNetoGravado { get; set; }
        public float? PorcIVA { get; set; }
        public string? PorcIVACodigoFiscal { get; set; }
        public decimal ImpIVA { get; set; }
        public int? CantAlicuota { get; set; }
    }
}
