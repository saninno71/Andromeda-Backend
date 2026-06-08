using fyd.backend.Dominio.Contabilidad.Enums;

namespace fyd.backend.API.ParametrosDeConsulta.Contabilidad
{
    public class CstctbCuentasParams
    {
        public int? Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int? CuentaMadreID { get; set; }
        public int? AsientoOK { get; set; }
        public SubcuentaTipo? SubcuentaTipo { get; set; }
        public bool? AjustaOK { get; set; }
        public MonedaTipo? MonedasTipo { get; set; }
        public int? EmpresaID { get; set; }
    }
}
