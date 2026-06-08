using fyd.backend.Dominio.General;

namespace fyd.backend.Dominio.Contabilidad.Entidades
{
    public class Grupo
    {
        private Grupo() { }

        public int Id { get; set; }
        public string Codigo { get; private set; } = string.Empty;
        public string Nombre { get; private set; } = string.Empty;

        //Navegación
        public ICollection<CuentaContable> CuentasContables { get; set; } = new List<CuentaContable>();

        public static Resultado<Grupo> Crear(string codigo, string nombre)
        {
            var grupo = new Grupo
            {
                Codigo = codigo,
                Nombre = nombre
            };

            return Resultado<Grupo>.Exito(grupo);
        }

        public Resultado Actualizar(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;

            return Resultado.Exito();
        }
    }
}
