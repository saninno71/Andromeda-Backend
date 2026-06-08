using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Enums;

namespace fyd.backend.Dominio.Contabilidad.Errores
{
    public class AsientoError
    {
        // --- Validaciones de Entidad (Dominio) ---

        public static Error NoEncontrado(int id) => new(
            AsientoCodigoError.NoEncontrado,
            $"El asiento {id} solicitado no existe.",
            TipoDeError.NoEncontrado);

        public static Error TipoInvalido => new(
            AsientoCodigoError.TipoInvalido,
            "El tipo de asiento debe ser Debe (1) o Haber (-1).",
            TipoDeError.Validacion);

        public static Error ImporteInvalido => new(
            AsientoCodigoError.ImporteInvalido,
            "El importe debe ser mayor a cero.",
            TipoDeError.Validacion);

        public static Error CuentaRequerida => new(
            AsientoCodigoError.CuentaRequerida,
            "La cuenta contable es obligatoria.",
            TipoDeError.Validacion);

        public static Error ComprobanteRequerido => new(
            AsientoCodigoError.ComprobanteRequerido,
            "El comprobante es obligatorio.",
            TipoDeError.Validacion);

        // --- Validaciones de Servicio (Aplicación) ---

        public static Error CuentaNoExiste(int cuentaId) => new(
            AsientoCodigoError.CuentaNoExiste,
            $"La cuenta contable {cuentaId} no existe.",
            TipoDeError.NoEncontrado);

        public static Error CuentaNoPerteneceAEmpresa(int cuentaId, int empresaId) => new(
            AsientoCodigoError.CuentaNoPerteneceAEmpresa,
            $"La cuenta contable {cuentaId} no pertenece a la empresa {empresaId} del comprobante.",
            TipoDeError.Validacion);

        public static Error CuentaNoPermiteAsiento(int cuentaId) => new(
            AsientoCodigoError.CuentaNoPermiteAsiento,
            $"La cuenta contable {cuentaId} no permite registrar asientos (es cuenta madre).",
            TipoDeError.Validacion);

        public static Error ClienteNoExiste(int clienteId) => new(
            AsientoCodigoError.ClienteNoExiste,
            $"El cliente {clienteId} no existe.",
            TipoDeError.NoEncontrado);

        public static Error ClienteNoPerteneceAEmpresa(int clienteId, int empresaId) => new(
            AsientoCodigoError.ClienteNoPerteneceAEmpresa,
            $"El cliente {clienteId} no pertenece a la empresa {empresaId} del comprobante.",
            TipoDeError.Validacion);

        public static Error ProveedorNoExiste(int proveedorId) => new(
            AsientoCodigoError.ProveedorNoExiste,
            $"El proveedor {proveedorId} no existe.",
            TipoDeError.NoEncontrado);

        public static Error ProveedorNoPerteneceAEmpresa(int proveedorId, int empresaId) => new(
            AsientoCodigoError.ProveedorNoPerteneceAEmpresa,
            $"El proveedor {proveedorId} no pertenece a la empresa {empresaId} del comprobante.",
            TipoDeError.Validacion);

        public static Error CajaNoExiste(int cajaId) => new(
            AsientoCodigoError.CajaNoExiste,
            $"La caja {cajaId} no existe.",
            TipoDeError.NoEncontrado);

        public static Error CajaNoPerteneceAEmpresa(int cajaId, int empresaId) => new(
            AsientoCodigoError.CajaNoPerteneceAEmpresa,
            $"La caja {cajaId} no pertenece a la empresa {empresaId} del comprobante.",
            TipoDeError.Validacion);

        public static Error AsientoNoBalancea => new(
            AsientoCodigoError.AsientoNoBalancea,
            "El asiento no balancea. La suma del Debe debe ser igual a la suma del Haber.",
            TipoDeError.Validacion);

        public static Error PeriodoCerrado => new(
            AsientoCodigoError.PeriodoCerrado,
            "El asiento corresponde a un período contablemente cerrado.",
            TipoDeError.Validacion);

        public static Error GeneradoPorComprobantePadre => new(
            AsientoCodigoError.GeneradoPorComprobantePadre,
            "El asiento fue generado por otro comprobante. No puede modificarse ni eliminarse desde este programa.",
            TipoDeError.Conflicto);

        public static Error VinculadoAOtroComprobante => new(
            AsientoCodigoError.VinculadoAOtroComprobante,
            "El asiento está vinculado a otros comprobantes por medio del programa aplicador.",
            TipoDeError.Conflicto);

        public static Error AplicadoEnCtaCte => new(
            AsientoCodigoError.AplicadoEnCtaCte,
            "El comprobante está aplicado en cuentas corrientes y no puede eliminarse.",
            TipoDeError.Conflicto);

        public static Error CotizacionInvalida => new(
            AsientoCodigoError.CotizacionInvalida,
            "La cotización local y la cotización de referencia deben ser mayores a cero.",
            TipoDeError.Validacion);

        public static Error SubcuentaCajaInvalida(int cuentaId) => new(
            AsientoCodigoError.SubcuentaCajaInvalida,
            $"La cuenta contable {cuentaId} no puede tener subcuenta de caja para este tipo de movimiento.",
            TipoDeError.Validacion);

        public static Error CajaBancariaConciliada(int cajaId) => new(
            AsientoCodigoError.CajaBancariaConciliada,
            $"El movimiento en la caja bancaria {cajaId} ya se encuentra conciliado y no puede modificarse.",
            TipoDeError.Conflicto);

        public static Error PeriodoRenumerado => new(
            AsientoCodigoError.PeriodoRenumerado,
            "No se pueden agregar, modificar ni eliminar asientos en períodos en los que ya se hayan renumerado los asientos.",
            TipoDeError.Conflicto);

        public static Error ComprobanteConNumeroAsiento => new(
            AsientoCodigoError.ComprobanteConNumeroAsiento,
            "No se puede modificar la empresa, numeración ni fecha de un comprobante que ya tiene asignado un número de asiento definitivo.",
            TipoDeError.Conflicto);
    }
}
