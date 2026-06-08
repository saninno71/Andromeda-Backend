using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoIVAPercepcionReadModel
    {
        [Key]
        public int ID { get; set; }
        public string? CodigoRegimen { get; set; }
        public string ProveedorNumeroDoc { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int? PuntoVenta { get; set; }
        public int NumeroComprobante { get; set; }
        public decimal ImporteLocal { get; set; }
    }
}
