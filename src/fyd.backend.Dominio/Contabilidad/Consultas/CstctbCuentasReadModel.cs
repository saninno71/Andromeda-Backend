using System.ComponentModel.DataAnnotations;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbCuentasReadModel
    {
        [Key]
        public int Id { get; set; }

        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;

        public string? Orden { get; set; }
        public int Nivel { get; set; }

        public int? CuentaMadreID { get; set; }
        public string? CuentaMadreCodigo { get; set; }
        public string? CuentaMadreNombre { get; set; }

        public string? AsientoOK { get; set; }

        public int SubcuentaTipo { get; set; }
        public string? SubcuentaTipoLey { get; set; }

        public string? AjustaOK { get; set; }

        public int MonedasTipo { get; set; }
        public string? MonedasTipoLey { get; set; }

        public int? EmpresaID { get; set; }
        public string? EmpresaCodigo { get; set; }
        public string? EmpresaNombre { get; set; }
    }
}
