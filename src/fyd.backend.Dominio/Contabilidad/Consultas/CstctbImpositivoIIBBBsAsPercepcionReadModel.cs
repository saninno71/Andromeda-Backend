using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoIIBBBsAsPercepcionReadModel
    {
        [Key]
        public int ID { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteNumeroDoc { get; set; } = string.Empty;
        public int Fecha { get; set; }
        public string CodigoFiscal { get; set; } = string.Empty;
        public string? Letra { get; set; }
        public int PuntoVenta { get; set; }
        public int Numero { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal ImporteLocal { get; set; }
    }
}
