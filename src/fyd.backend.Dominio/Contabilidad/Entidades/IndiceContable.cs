using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Parametros;
using fyd.backend.Dominio.General;

namespace fyd.backend.Dominio.Contabilidad.Entidades
{
    public class IndiceContable
    {
        public int Id { get; set; }
        public DateTime Periodo { get; private set; }
        public decimal Indice { get; private set; }

        // Constructor privado para EF Core
        private IndiceContable() { }

        public static Resultado<IndiceContable> Crear(ParametrosIndice parametros)
        {
            if (parametros.Periodo == default)
            {
                return Resultado<IndiceContable>.Falla(IndiceContableError.PeriodoVacio);
            }

            if (parametros.Indice <= 0)
            {
                return Resultado<IndiceContable>.Falla(IndiceContableError.IndiceInvalido);
            }

            var indice = new IndiceContable
            {
                Periodo = new DateTime(parametros.Periodo.Year, parametros.Periodo.Month, 1),
                Indice = parametros.Indice
            };

            return Resultado<IndiceContable>.Exito(indice);
        }

        public Resultado Actualizar(ParametrosIndice parametros)
        {
            if (parametros.Periodo == default)
            {
                return Resultado.Falla(IndiceContableError.PeriodoVacio);
            }

            if (parametros.Indice <= 0)
            {
                return Resultado.Falla(IndiceContableError.IndiceInvalido);
            }

            Periodo = new DateTime(parametros.Periodo.Year, parametros.Periodo.Month, 1);
            Indice = parametros.Indice;

            return Resultado.Exito();
        }
    }
}
