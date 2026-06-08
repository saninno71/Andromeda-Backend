using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Enums;

namespace fyd.backend.Dominio.Contabilidad.Errores
{
    public static class ConsolidacionError
    {
        public static Error PeriodoYaGenerado(string modulo) =>
            new($"Consolidacion.PeriodoYaGenerado", $"El módulo '{modulo}' ya fue contabilizado para el período indicado.", TipoDeError.Conflicto);

        public static Error SinComprobantes(string modulo) =>
            new($"Consolidacion.SinComprobantes", $"El módulo '{modulo}' no tiene comprobantes para procesar en el período indicado.", TipoDeError.Validacion);

        public static readonly Error ProcesoEnCurso =
            new("Consolidacion.ProcesoEnCurso", "Otro proceso de consolidación está en ejecución. Intente nuevamente en unos momentos.", TipoDeError.Conflicto);

        public static Error ValidacionAsientosFallida(string modulo) =>
            new("Consolidacion.ValidacionAsientosFallida", $"No todos los asientos del módulo '{modulo}' pudieron ser creados. Se revirtió el proceso del período.", TipoDeError.Conflicto);

        public static readonly Error ModuloNoValido =
            new("Consolidacion.ModuloNoValido", "El módulo indicado no es válido.", TipoDeError.Validacion);

        public static readonly Error BloqueoOcupado =
            new("Consolidacion.BloqueoOcupado", "No se puede adquirir el bloqueo del período: otro proceso lo está usando.", TipoDeError.Conflicto);

        public static readonly Error PeriodoNoPuedeEliminarse =
            new("Consolidacion.PeriodoNoPuedeEliminarse", "No existen asientos generados por consolidación para el período y módulo indicados.", TipoDeError.Validacion);
    }
}
