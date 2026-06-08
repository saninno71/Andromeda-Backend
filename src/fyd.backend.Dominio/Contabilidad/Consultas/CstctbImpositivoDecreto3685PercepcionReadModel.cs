using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoDecreto3685PercepcionReadModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoComprobante { get; set; } = string.Empty;
        public int? PuntoVenta { get; set; }
        public int Numero { get; set; }
        public string? CodigoFiscal { get; set; }
        public decimal ImporteLocal { get; set; }
    }
}
