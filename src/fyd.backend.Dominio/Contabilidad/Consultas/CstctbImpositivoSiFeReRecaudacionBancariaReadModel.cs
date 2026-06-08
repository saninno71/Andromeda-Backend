using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoSiFeReRecaudacionBancariaReadModel
    {
        [Key]
        public int ID { get; set; }
        public int? JurisdiccionIIBB { get; set; }
        public string NumeroDoc { get; set; } = string.Empty;
        public DateTime? FechaEmision { get; set; }
        public string CBU { get; set; } = string.Empty;
        public string TipoCuenta { get; set; } = string.Empty;
        public int MonedaID { get; set; }
        public string Moneda { get; set; } = string.Empty;
        public string MonedaNombre { get; set; } = string.Empty;
        public decimal ImporteLocal { get; set; }
    }
}
