using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoSiFeRePercepcionReadModel
    {
        [Key]
        public int ID { get; set; }
        public int? JurisdiccionIIBB { get; set; }
        public string NumeroDoc { get; set; } = string.Empty;
        public DateTime? FechaEmision { get; set; }
        public int? PuntoVenta { get; set; }
        public int Numero { get; set; }
        public string CodigoFiscal { get; set; } = string.Empty;
        public string? Letra { get; set; }
        public decimal ImporteLocal { get; set; }
    }
}
