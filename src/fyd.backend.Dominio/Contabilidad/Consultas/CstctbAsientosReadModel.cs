using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace fyd.backend.Dominio.Contabilidad.Consultas
{
    public class CstctbAsientosReadModel
    {
        [Key]
        public int ComprobanteID { get; set; }
        public DateTime Fecha { get; set; }
        public string MonedaCodigo { get; set; }
        public int NumeraTipoID { get; set; }
        public string NumeraTipoSimbolo { get; set; }
        public int NumeracionID { get; set; }
        public int Numero { get; set; }

        //Debe cuenta
        public int? DebeCuentaID { get; set; }
        public string? DebeCuentaCodigo { get; set; }
        public string? DebeCuentaNombre { get; set; }
        public decimal? DebeImporte { get; set; }
        public decimal? DebeImpLocal { get; set; }
        public decimal? DebeImpReferencia { get; set; }

        //Debe cliente
        public int? DebeClienteID { get; set; }
        public int? DebeClienteCodigo { get; set; }
        public string? DebeClienteNombre { get; set; }
        
        
        //Debe proveedor
        public int? DebeProveedorID { get; set; }
        public int? DebeProveedorCodigo { get; set; }
        public string? DebeProveedorNombre { get; set; }
        
        //Debe caja
        public int? DebeCajaID { get; set; }
        public string? DebeCajaCodigo { get; set; }
        public string? DebeCajaNombre { get; set; }

        public int? HaberProveedorID { get; set; }
        public int? HaberProveedorCodigo { get; set; }
        public string? HaberProveedorNombre { get; set; }
        public int? HaberCajaID { get; set; }
        public string? HaberCajaCodigo { get; set; }
        public string? HaberCajaNombre { get; set; }
        //Haber
        public int? HaberCuentaID { get; set; }
        public string? HaberCuentaCodigo { get; set; }
        public string? HaberCuentaNombre { get; set; }
        public decimal? HaberImporte { get; set; }
        public decimal? HaberImpLocal { get; set; }
        public decimal? HaberImpReferencia { get; set; }
        
        // Haber Cliente
        public int? HaberClienteID { get; set; }
        public int? HaberClienteCodigo { get; set; }
        public string? HaberClienteNombre { get; set; }

        public string? Detalle { get; set; }

        //Empresa
        public int? EmpresaID { get; set; }
        public int? EmpresaCodigo { get; set; }
        public string? EmpresaNombre { get; set; }
    }
}
