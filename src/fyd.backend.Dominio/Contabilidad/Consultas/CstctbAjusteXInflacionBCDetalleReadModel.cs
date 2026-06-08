using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    // Modelo para @DevolverTotalesOk = 0 (Detalle)
    public class CstctbAjusteXInflacionBCDetalleReadModel
    {
        public int ArticuloID { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
         
        [Key] // ESTO NO SIRVE, VER COMO LO CORRIJO
        public int Orden { get; set; }

        public DateTime Fecha { get; set; }
        public DateTime Periodo { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Comprobante { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;
        public float Cantidad { get; set; }
        public string Moneda { get; set; } = string.Empty;
        //public float PrecioUnitario { get; set; }
        //public float Cotizacion { get; set; }
        //public float? IndiceNacionalizacion { get; set; }
        //public double Precio { get; set; }
        //public double Total { get; set; }

        public double? PrecioUnitario { get; set; }
        public double? Cotizacion { get; set; }
        public double? IndiceNacionalizacion { get; set; }
        public double? Precio { get; set; }
        public double? Total { get; set; }
    }
}
