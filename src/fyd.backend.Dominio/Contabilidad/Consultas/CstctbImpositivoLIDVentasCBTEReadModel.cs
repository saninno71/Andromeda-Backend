using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoLIDVentasCBTEReadModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoComprobante { get; set; }
        public int PuntoVenta { get; set; }
        public int Numero { get; set; }
        public string ClienteDocCodigo { get; set; } = string.Empty;
        public string ClienteDocNumero { get; set; } = string.Empty;
        public string ClienteNombre { get; set; } = string.Empty;
        public double ImpTotal { get; set; }
        public decimal ImpNetoGravado { get; set; }
        public decimal ImpNoGravado { get; set; }
        public decimal ImpExento { get; set; }
        public decimal ImpIVAPercepcion_GananciasPercepcion { get; set; }
        public decimal ImpIVARNI_IVANoCateg { get; set; }
        public decimal ImpIVALiberado { get; set; }
        public decimal ImpIIBBPercepcion { get; set; }
        public decimal ImpImpInternos { get; set; }
        public string MonedaCodigo { get; set; } = string.Empty;
        public double CotizacionLocal { get; set; }
        public int? CantAlicuota { get; set; }
        public bool ZonaFrancaOK { get; set; }
        public bool ExtranjeroOK { get; set; }
        public string TipoResponsable { get; set; } = string.Empty;
    }
}
