using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoIVAComprasExteriorReadModel
    {
        [Key]
        public int ID { get; set; }
        public int Tipo { get; set; }
        public int DespachoID { get; set; }
        public string DespachoNombre { get; set; } = string.Empty;
        public decimal ImporteLocal { get; set; }
    }
}
