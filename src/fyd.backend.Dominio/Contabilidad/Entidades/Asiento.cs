using fyd.backend.Dominio.Contabilidad.Enums;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Entidades;

namespace fyd.backend.Dominio.Contabilidad.Entidades
{
    public class Asiento
    {
        public int Id { get; set; }
        public int ComprobanteId { get; private set; }
        public int CuentaId { get; private set; }
        public int? ClienteId { get; private set; }
        public int? ProveedorId { get; private set; }
        public int? CajaId { get; private set; }
        public int Tipo { get; private set; } // 1=Debe, -1=Haber
        public decimal Importe { get; private set; }
        public decimal ImpLocal { get; private set; }
        public decimal ImpReferencia { get; private set; }
        public string Detalle { get; private set; } = string.Empty;
        public bool EliminarOk { get; private set; }

        // Navegación
        public virtual Comprobante? Comprobante { get; private set; }

        private Asiento() { } // EF Core

        public static Resultado<Asiento> Crear(
            int comprobanteId, int cuentaId, int tipo, decimal importe,
            decimal impLocal, decimal impReferencia, string detalle,
            int? clienteId = null, int? proveedorId = null, int? cajaId = null)
        {
            if (comprobanteId < 0)
                return Resultado<Asiento>.Falla(AsientoError.ComprobanteRequerido);

            if (cuentaId <= 0)
                return Resultado<Asiento>.Falla(AsientoError.CuentaRequerida);

            if (tipo != (int)AsientoTipo.Debe && tipo != (int)AsientoTipo.Haber)
                return Resultado<Asiento>.Falla(AsientoError.TipoInvalido);

            if (importe <= 0)
                return Resultado<Asiento>.Falla(AsientoError.ImporteInvalido);

            var asiento = new Asiento
            {
                ComprobanteId = comprobanteId,
                CuentaId = cuentaId,
                Tipo = tipo,
                Importe = importe,
                ImpLocal = impLocal,
                ImpReferencia = impReferencia,
                Detalle = detalle,
                ClienteId = clienteId,
                ProveedorId = proveedorId,
                CajaId = cajaId,
                EliminarOk = false
            };

            return Resultado<Asiento>.Exito(asiento);
        }

        public Resultado Actualizar(
            int cuentaId, int tipo, decimal importe,
            decimal impLocal, decimal impReferencia, string detalle,
            int? clienteId = null, int? proveedorId = null, int? cajaId = null)
        {
            if (cuentaId <= 0)
                return Resultado.Falla(AsientoError.CuentaRequerida);

            if (tipo != (int)AsientoTipo.Debe && tipo != (int)AsientoTipo.Haber)
                return Resultado.Falla(AsientoError.TipoInvalido);

            if (importe <= 0)
                return Resultado.Falla(AsientoError.ImporteInvalido);

            CuentaId = cuentaId;
            Tipo = tipo;
            Importe = importe;
            ImpLocal = impLocal;
            ImpReferencia = impReferencia;
            Detalle = detalle;
            ClienteId = clienteId;
            ProveedorId = proveedorId;
            CajaId = cajaId;

            return Resultado.Exito();
        }
    }
}
