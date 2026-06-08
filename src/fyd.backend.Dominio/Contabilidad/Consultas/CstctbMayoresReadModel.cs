using System;
using System.Collections.Generic;
using System.Text;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbMayoresReadModel
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public int NumeraTipoID { get; set; }
        public string NumeraTipoSimbolo { get; set; }
        public int Numero { get; set; }

        // Importes en Moneda del Comprobante
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }

        // Importes en Moneda Local
        public decimal DebeLocal { get; set; }
        public decimal HaberLocal { get; set; }

        // Importes en Moneda de Referencia
        public decimal DebeReferencia { get; set; }
        public decimal HaberReferencia { get; set; }

        // Cotizaciones
        public decimal CotizaLocalRef { get; set; }
        public decimal CotizaCompLocal { get; set; }

        // Metadatos de Moneda
        public string MonedaCompCodigo { get; set; }
        public string MonedaCompNombre { get; set; }

        // Información de la transacción
        public string? Detalle { get; set; }
        public int? ClienteID { get; set; }
        public int? ClienteCodigo { get; set; }
        public int? ProveedorID { get; set; }
        public int? ProveedorCodigo { get; set; }
        public int? CajaID { get; set; }
        public string? CajaCodigo { get; set; }
        public string? SubcuentaNombre { get; set; }

        // Información de Empresa
        public int EmpresaID { get; set; }
        public int EmpresaCodigo { get; set; }
        public string EmpresaNombre { get; set; }
    }
}
