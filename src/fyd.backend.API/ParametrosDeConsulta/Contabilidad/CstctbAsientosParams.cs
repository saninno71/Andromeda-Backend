namespace fyd.backend.API.ParametrosDeConsulta.Contabilidad
{
    public class CstctbAsientosParams
    {
        public int? Id { get; set; }
        public int? FechaDesde { get; set; }
        public int? FechaHasta { get; set; }
        public string? Detalle { get; set; }
        public int? CuentaID { get; set; }
        public int? NumeroDesde { get; set; }
        public int? NumeroHasta { get; set; }
        public int? SubcuentaClienteID { get; set; }
        public int? SubcuentaProveedorID { get; set; }
        public int? SubcuentaCajaID { get; set; }
        public string? EmpresaID { get; set; }
        public int? NumeraTipoID { get; set; }
    }
}
