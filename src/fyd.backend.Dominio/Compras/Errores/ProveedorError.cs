using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Enums;

namespace fyd.backend.Dominio.Compras.Errores
{
    public static class ProveedorError
    {
        public static Error NoEncontrado(int id) => new(
            ProveedorCodigoError.NoEncontrado,
            $"No se encontró el proveedor con ID {id}.",
            TipoDeError.NoEncontrado);

        public static readonly Error DatosInvalidos = new(
            ProveedorCodigoError.DatosInvalidos,
            "Los datos de creación o actualización del proveedor son inválidos.",
            TipoDeError.Validacion);

        public static readonly Error CodigoDuplicado = new(
            ProveedorCodigoError.CodigoDuplicado,
            "Ya existe un proveedor con ese código.",
            TipoDeError.Conflicto);
        public static readonly Error FechasInvalidas = new(
            ProveedorCodigoError.FechasInvalidas,
            "Las fechas son invalidas.",
            TipoDeError.Conflicto);

        public static readonly Error PorcentajeDescuentoInvalido = new(
            ProveedorCodigoError.PorcentajeDescuentoInvalido,
            "El porcentaje de descuento debe estar entre 0 y 100.",
            TipoDeError.Validacion);

        public static readonly Error CodigoInvalido = new(
            ProveedorCodigoError.CodigoInvalido,
            "El código no puede ser negativo.",
            TipoDeError.Validacion);

        public static readonly Error CircularidadCc = new(
            ProveedorCodigoError.CircularidadCc,
            "Un proveedor no puede ser su propio Proveedor de Cta. Cte.",
            TipoDeError.Validacion);

        public static readonly Error EnUso = new(
            ProveedorCodigoError.EnUso,
            "El proveedor no puede eliminarse porque está en uso o tiene comprobantes.",
            TipoDeError.Conflicto);
    }
}
