using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoDecreto3685CabeceraReadModel
    {
        [Key]
        public int ID { get; set; }
        public int TipoRegistro { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoComprobante { get; set; } = string.Empty;
        public string? TipoAutorizacion { get; set; }
        public int PuntoVenta { get; set; }
        public int Numero { get; set; }
        public int CantidadHojas { get; set; }
        public string ClienteDocCodigo { get; set; } = string.Empty;
        public string ClienteDocNumero { get; set; } = string.Empty;
        public string ClienteNombre { get; set; } = string.Empty;
        public double ImpTotal { get; set; }
        public float? PorcIVA { get; set; }
        public decimal? ImpTotal1 { get; set; }
        public decimal? ImpNetoGravado { get; set; }
        public decimal? ImpNoGravado { get; set; }
        public decimal? ImpExento { get; set; }
        public decimal? ImpIVA { get; set; }
        public decimal? ImpIVAPercepcion_GananciasPercepcion { get; set; }
        public decimal? ImpIVARNI_IVANoCateg { get; set; }
        public decimal? ImpIVALiberado { get; set; }
        public decimal? ImpIIBBPercepcion { get; set; }
        public decimal? ImpImpInternos { get; set; }
        public string TipoResponsable { get; set; } = string.Empty;
        public string MonedaCodigo { get; set; } = string.Empty;
        public double CotizacionLocal { get; set; }
        public int CantAlicuota { get; set; }
        public string? CodigoOperacion { get; set; }
        public string? CAI { get; set; }
        public DateTime? CAIVencimiento { get; set; }
        public int Estado { get; set; }
        public DateTime? FechaAnulacion { get; set; }
    }
}
