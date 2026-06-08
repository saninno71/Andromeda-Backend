using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Enums;

namespace fyd.backend.Dominio.Contabilidad.Errores
{
    public class IndiceContableError
    {
        public static Error NoEncontrado(int id) => new(
            IndiceContableCodigoError.NoEncontrado,
            $"El índice contable con ID {id} no existe.",
            TipoDeError.NoEncontrado);

        public static Error PeriodoVacio => new(
            IndiceContableCodigoError.PeriodoVacio,
            "El período del índice es obligatorio.",
            TipoDeError.Validacion);

        public static Error IndiceInvalido => new(
            IndiceContableCodigoError.IndiceInvalido,
            "El índice debe ser mayor a cero.",
            TipoDeError.Validacion);

        public static Error PeriodoDuplicado(DateTime periodo) => new(
            IndiceContableCodigoError.PeriodoDuplicado,
            $"Ya existe un índice cargado para el período {periodo:MM/yyyy}.",
            TipoDeError.Conflicto);
    }
}
