using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoSiFeReFacturacionTipoImporteReadModel
    {
        [Key]
        public int TipoImporteID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal ImporteLocal { get; set; }
    }
}
