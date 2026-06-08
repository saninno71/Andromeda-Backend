using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Enums;

namespace fyd.backend.Dominio.Contabilidad.Errores
{
    public class CuentaContableError
    {
        public static Error NoEncontrada(int id) => new(
            CuentaContableCodigoError.NoEncontrada,
            $"La cuenta contable {id} solicitada no existe.",
            TipoDeError.NoEncontrado);

        public static Error CodigoVacio => new(
            CuentaContableCodigoError.CodigoVacio,
            "El código de la cuenta es obligatorio.",
            TipoDeError.Validacion);

        public static Error NombreVacio => new(
        CuentaContableCodigoError.NombreVacio,
        "El nombre de la cuenta es obligatorio.",
        TipoDeError.Validacion);

        public static Error CuentaMadreNoValida => new(
            CuentaContableCodigoError.CuentaMadreNoValida,
            "Una cuenta no puede tenerse a si misma como cuenta madre",
            TipoDeError.Validacion);

        public static Error CodigoDuplicado(string codigo) => new
        (
            CuentaContableCodigoError.CodigoDuplicado,
            $"El código '{codigo}' ya está en uso.",
            TipoDeError.Conflicto
        );

        public static Error ReferenciaCircular => new
        (
            CuentaContableCodigoError.ReferenciaCircular,
            "No se puede mover la cuenta debajo de uno de sus propios descendientes (Referencia Circular).",
            TipoDeError.Validacion
        );

        public static Error TieneMovimientos => new(
            CuentaContableCodigoError.TieneMovimientos,
            "La cuenta contable tiene movimientos.",
            TipoDeError.Validacion
            );

        public static Error TieneCuentasHijas => new(
            CuentaContableCodigoError.TieneCuentasHijas,
            "No se puede eliminar una cuenta que está siendo utilizada como 'Cuenta madre' de otras. \n Debe primero eliminar todas las cuentas dependientes o modificarles la dependencia.",
            TipoDeError.Validacion
            );

        public static Error SuperaNivelMaximoPermitido => new(
            CuentaContableCodigoError.SuperaNivelMaximoPermitido,
            "La cuenta contable no puede tener más de 6 niveles de profundidad.",
            TipoDeError.Validacion
            );

            public static Error GrupoNoEncontrado => new(
            CuentaContableCodigoError.GrupoNoEncontrado,
            "Uno o más grupos contables indicados no existen.",
            TipoDeError.NoEncontrado
            );
    }
}

