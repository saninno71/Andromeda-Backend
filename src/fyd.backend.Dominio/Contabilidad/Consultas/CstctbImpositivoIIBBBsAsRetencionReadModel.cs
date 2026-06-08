using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoIIBBBsAsRetencionReadModel
    {
        [Key]
        public int ID { get; set; }
        public string ProveedorNombre { get; set; } = string.Empty;
        public string ProveedorNumeroDoc { get; set; } = string.Empty;
        public int Fecha { get; set; }
        public int PuntoVenta { get; set; }
        public int Numero { get; set; }
        public decimal ImporteLocal { get; set; }
    }
}
