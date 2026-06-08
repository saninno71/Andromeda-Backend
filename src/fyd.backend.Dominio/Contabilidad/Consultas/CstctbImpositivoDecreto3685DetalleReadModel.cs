using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoDecreto3685DetalleReadModel
    {
        [Key]
        public int ID { get; set; }
        public string TipoComprobante { get; set; } = string.Empty;
        public string? TipoAutorizacion { get; set; }
        public DateTime Fecha { get; set; }
        public int? PuntoVenta { get; set; }
        public int? Numero { get; set; }
        public int Signo { get; set; }
        public double Cantidad { get; set; }
        public int? UnidadMedida { get; set; }
        public double ImpBonificacion { get; set; }
        public int Ajuste { get; set; }
        public double SubTotal { get; set; }
        public float? AlicuotaIVA { get; set; }
        public string? TipoGravamen { get; set; }
        public string? Estado { get; set; }
        public string? DisenoLibre { get; set; }
        public int EmpresaId { get; set; }

        public int NumeraTiposTipo { get; set; }
    }
}
