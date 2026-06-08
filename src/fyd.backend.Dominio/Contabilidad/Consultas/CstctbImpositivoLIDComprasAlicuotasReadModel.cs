using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoLIDComprasAlicuotasReadModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime FechaEmision { get; set; }
        public string TipoComprobante { get; set; }
        public int PuntoVenta { get; set; }
        public int Numero { get; set; }
        public string VendedorDocCodigo { get; set; } = string.Empty;
        public string VendedorDocNumero { get; set; } = string.Empty;
        public decimal ImpNetoGravado { get; set; }
        public float? PorcIVA { get; set; }
        public string? PorcIVACodigoFiscal { get; set; }
        public decimal ImpIVA { get; set; }
        public int? CantAlicuota { get; set; }
    }
}
