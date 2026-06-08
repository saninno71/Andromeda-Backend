using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoSiFeRePercepcionAduaneraReadModel
    {
        [Key]
        public int ID { get; set; }
        public int? JurisdiccionIIBB { get; set; }
        public string NumeroDoc { get; set; } = string.Empty;
        public DateTime? FechaEmision { get; set; }
        public int? DespachoID { get; set; }
        public string Despacho { get; set; } = string.Empty;
        public string DespachoNombre { get; set; } = string.Empty;
        public decimal ImporteLocal { get; set; }
    }
}
