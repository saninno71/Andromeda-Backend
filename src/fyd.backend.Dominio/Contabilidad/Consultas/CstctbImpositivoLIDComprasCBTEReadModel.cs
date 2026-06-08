using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbImpositivoLIDComprasCBTEReadModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime FechaEmision { get; set; }
        public string TipoComprobante { get; set; }
        public int PuntoVenta { get; set; }
        public int Numero { get; set; }
        public string? DespachoNumero { get; set; }
        public string VendedorDocCodigo { get; set; } = string.Empty;
        public string VendedorDocNumero { get; set; } = string.Empty;
        public string VendedorNombre { get; set; } = string.Empty;
        public double ImpTotal { get; set; }
        public decimal ImpNetoGravado { get; set; }
        public decimal ImpNoGravado { get; set; }
        public decimal ImpExento { get; set; }
        public decimal ImpIVAPercepcion { get; set; }
        public decimal Imp_GananciasPercepcion { get; set; }
        public decimal ImpIIBBPercepcion { get; set; }
        public decimal ImpImpInternos { get; set; }
        public decimal ImpIVA { get; set; }
        public string MonedaCodigo { get; set; } = string.Empty;
        public double CotizacionLocal { get; set; }
        public int? CantAlicuota { get; set; }
        public bool ZonaFrancaOK { get; set; }
        public bool ExtranjeroOK { get; set; }
        public int? DespachoID { get; set; }
    }
}
