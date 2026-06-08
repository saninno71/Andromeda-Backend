namespace fyd.backend.API.ParametrosDeConsulta.Contabilidad
{
    public class CstctbMayoresParams
    {
        public int? Id { get; set; }
        public int? CuentaID { get; set; }
        public string? EmpresaID { get; set; }
        public int? FechaDesde { get; set; }
        public int? FechaHasta { get; set; }
        public bool? ArrastraSaldoOK { get; set; }
        public bool? IncluyeImputacionesOK { get; set; }
        public int? FechaDesdeInicial { get; set; }
    }
}
