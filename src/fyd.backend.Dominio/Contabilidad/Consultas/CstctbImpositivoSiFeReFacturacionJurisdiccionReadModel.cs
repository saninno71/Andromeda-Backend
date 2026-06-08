using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoSiFeReFacturacionJurisdiccionReadModel
    {
        [Key]
        public int CuentaID { get; set; }
        public string CuentaNombre { get; set; } = string.Empty;
        public int? ProvinciaID { get; set; }
        public string ProvinciaNombre { get; set; } = string.Empty;
        public decimal Importe { get; set; }
    }
}
