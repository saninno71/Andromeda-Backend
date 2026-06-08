namespace fyd.backend.Dominio.Ventas.Consultas
{
    public class CstvtsCtaCteMovimientosReadModel
    {
        public int Id { get; set; }
        public int ComprobanteID { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaVto { get; set; }
        public int NumeraTipoID { get; set; }
        public int NumeraTipoTipo { get; set; }
        public string NumeraTipoSimbolo { get; set; } = String.Empty;
        public int NumeracionID { get; set; }
        public string Numero { get; set; } = String.Empty;
        public int CuotaNumero { get; set; }
        public int MonedaID { get; set; }
        public string MonedaCodigo { get; set; } = String.Empty;
        public string MonedaNombre { get; set; } = String.Empty;
        public decimal ImpTotal { get; set; }
        public int MonedaCCID { get; set; }
        public string MonedaCCCodigo { get; set; } = String.Empty;
        public string MonedaCCNombre { get; set; } = String.Empty;
        public decimal Importe { get; set; }
        public string Detalle { get; set; } = String.Empty;
        public decimal VtsCtasCtesImporte { get; set; }
        public int ClienteID { get; set; }
        public int ClienteCodigo { get; set; }
        public string ClienteNombre { get; set; } = String.Empty;
    }
}
