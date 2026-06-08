using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoLIDVentasAlicuotasReadModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoComprobante { get; set; }
        public int PuntoVenta { get; set; }
        public int Numero { get; set; }
        public decimal ImpNetoGravado { get; set; }
        public float? PorcIVA { get; set; }
        public int? PorcIVACodigoFiscal { get; set; }
        public decimal ImpIVA { get; set; }
        public int? CantAlicuota { get; set; }
    }
}
