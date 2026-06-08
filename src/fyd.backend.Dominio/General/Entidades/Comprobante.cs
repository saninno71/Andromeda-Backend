using fyd.backend.Dominio.Contabilidad.Entidades;
using System;
using System.Collections.Generic;

namespace fyd.backend.Dominio.General.Entidades
{
    public class Comprobante
    {
        private Comprobante() { }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaEmision { get; set; }
        public int NumeraTipoId { get; set; }
        public int Numero { get; set; }
        public int EmpresaId { get; set; }
        public int MonedaId { get; set; }
        public decimal CotizacionLocal { get; set; }
        public decimal CotizacionReferencia { get; set; }
        public decimal ImpTotal { get; set; }
        public string Detalle { get; set; } = string.Empty;
        public string? Memo { get; set; }

        public int? AsientoId { get; set; }

        public virtual ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();

        public static Resultado<Comprobante> Crear(
            DateTime fecha,
            DateTime fechaEmision,
            int numeraTipoId,
            int numero,
            int empresaId,
            int monedaId,
            decimal cotizacionLocal,
            decimal cotizacionReferencia,
            decimal impTotal,
            string detalle,
            string? memo)
        {
            var comprobante = new Comprobante
            {
                Fecha = fecha,
                FechaEmision = fechaEmision,
                NumeraTipoId = numeraTipoId,
                Numero = numero,
                EmpresaId = empresaId,
                MonedaId = monedaId,
                CotizacionLocal = cotizacionLocal,
                CotizacionReferencia = cotizacionReferencia,
                ImpTotal = impTotal,
                Detalle = detalle,
                Memo = memo
            };

            return Resultado<Comprobante>.Exito(comprobante);
        }

        public Resultado Actualizar(
            DateTime fecha,
            DateTime fechaEmision,
            int numeraTipoId,
            int numero,
            int empresaId,
            int monedaId,
            decimal cotizacionLocal,
            decimal cotizacionReferencia,
            decimal impTotal,
            string detalle,
            string? memo)
        {
            Fecha = fecha;
            FechaEmision = fechaEmision;
            NumeraTipoId = numeraTipoId;
            Numero = numero;
            EmpresaId = empresaId;
            MonedaId = monedaId;
            CotizacionLocal = cotizacionLocal;
            CotizacionReferencia = cotizacionReferencia;
            ImpTotal = impTotal;
            Detalle = detalle;
            Memo = memo;

            return Resultado.Exito();
        }
    }
}
