using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoSujetosVinculadosCBTEReadModel
    {
        [Key]
        public int ID { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public string TipoRegistro { get; set; } = string.Empty;
        public string CodigoFiscal { get; set; } = string.Empty;
        public int PuntoVenta { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string TituloGratuito { get; set; } = string.Empty;
        public string DocNumero { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public decimal ImporteTotal { get; set; }
        public decimal ImpNoGravado { get; set; }
        public decimal ImpNetoGravado { get; set; }
        public decimal ImpExento { get; set; }
    }
}
