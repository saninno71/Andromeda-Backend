using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    // Modelo para @DevolverTotalesOk = 1 (Totales)
    public class CstctbAjusteXInflacionBCTotalesReadModel
    {
        [Key] // El período identifica de forma unívoca la fila en la vista agrupada
        public DateTime Periodo { get; set; }
        public double? IndiceAplicado { get; set; }
        public double Total { get; set; }
        public double? Ajustado { get; set; }
        public double? Diferencia { get; set; }
    }
}
