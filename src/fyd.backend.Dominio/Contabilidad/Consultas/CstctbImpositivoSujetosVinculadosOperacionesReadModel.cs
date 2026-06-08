using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoSujetosVinculadosOperacionesReadModel
    {
        [Key]
        public int ID { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public string TipoRegistro { get; set; } = string.Empty;
        public string CodigoFiscal { get; set; } = string.Empty;
        public int PuntoVenta { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string DocNumero { get; set; } = string.Empty;
        public string TipoOperacion { get; set; } = string.Empty;
    }
}
