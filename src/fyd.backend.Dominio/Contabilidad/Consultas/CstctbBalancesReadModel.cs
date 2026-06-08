using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbBalancesReadModel
    {
        [Key]
        public int CuentaID { get; set; }
        public string CuentaCodigo { get; set; } = string.Empty;
        public string CuentaNombre { get; set; } = string.Empty;

        // Totales en Moneda Local
        public decimal DebeLocal { get; set; }
        public decimal HaberLocal { get; set; }
        public decimal SaldoLocal { get; set; }

        // Totales en Moneda de Referencia
        public decimal DebeReferencia { get; set; }
        public decimal HaberReferencia { get; set; }
        public decimal SaldoReferencia { get; set; }

        // Cálculo de gestión
        public decimal CotizacionPromedio { get; set; }
    }
}
