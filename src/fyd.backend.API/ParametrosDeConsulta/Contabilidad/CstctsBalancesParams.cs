namespace fyd.backend.API.ParametrosDeConsulta.Contabilidad
{
    public class CstctbBalancesParams
    {
        public int? CuentaId { get; set; }
        public int? FechaDesde { get; set; }
        public int? FechaHasta { get; set; }
        public string? EmpresaID { get; set; }
        public bool? IncluyeImputacionesOK { get; set; }
    }
}
