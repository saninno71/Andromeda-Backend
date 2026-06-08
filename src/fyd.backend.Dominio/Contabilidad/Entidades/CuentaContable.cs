using fyd.backend.Dominio.Contabilidad.Enums;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Parametros;
using fyd.backend.Dominio.General;

namespace fyd.backend.Dominio.Contabilidad.Entidades
{
    public class CuentaContable
    {
        #region propiedades
        public int Id { get; set; }
        public string Codigo { get; private set; } = string.Empty;
        public string Nombre { get; private set; } = string.Empty;
        public int? EmpresaId { get; private set; } // NULL = Todas (Consolidado)
        public int? CuentaIdMadre { get; private set; }
        public bool AsientoOk { get; private set; }
        public bool AjustaOk { get; private set; }
        public SubcuentaTipo SubcuentaTipo { get; private set; }
        public MonedaTipo MonedaTipo { get; private set; }


        // Navegación
        public virtual CuentaContable? CuentaMadre { get; private set; }
        public virtual ICollection<CuentaContable> CuentasHijas { get; private set; } = new List<CuentaContable>();
        public virtual ICollection<Grupo> Grupos { get; set; }

        #endregion region
        // Constructor privado para EF Core
        
        private CuentaContable() { }

        public static Resultado<CuentaContable> Crear(ParametrosCuenta parametros)
        {
            // 1. Validaciones de Integridad Básica
            if (string.IsNullOrWhiteSpace(parametros.Codigo))
            {
                return Resultado<CuentaContable>.Falla(CuentaContableError.CodigoVacio);
            }

            if (string.IsNullOrWhiteSpace(parametros.Nombre))
            {
                return Resultado<CuentaContable>.Falla(CuentaContableError.NombreVacio);
            }
            
            var subcuentaTipo = parametros.SubcuentaTipo;
            var empresaId = parametros.EmpresaId;

            if (!parametros.AsientoOk)
            {
                subcuentaTipo = SubcuentaTipo.SinSubcuentas;
                empresaId = null;
            }

            // 3. Retorno de la instancia válida
            var cuenta = new CuentaContable
            {
                Codigo = parametros.Codigo,
                Nombre = parametros.Nombre,
                AsientoOk = parametros.AsientoOk,
                SubcuentaTipo = subcuentaTipo,
                MonedaTipo = parametros.MonedaTipo,
                AjustaOk = parametros.AjustaOk,
                EmpresaId = empresaId,
                CuentaIdMadre = parametros.CuentaIdMadre
            };

            return Resultado<CuentaContable>.Exito(cuenta);
        }

        // Método para actualizar (misma lógica de validación)
        public Resultado Actualizar(
            ParametrosCuenta parametros)
        {
            if (string.IsNullOrWhiteSpace(parametros.Codigo))
            {                
                return Resultado.Falla(CuentaContableError.CodigoVacio);
            }

            if (string.IsNullOrWhiteSpace(parametros.Nombre))
            {
                return Resultado.Falla(CuentaContableError.NombreVacio);
            }

            var subcuentaTipo = parametros.SubcuentaTipo;
            var empresaId = parametros.EmpresaId;

            if (!parametros.AsientoOk) // TODO: Ver si queremos forzar o rechazar. 
            {
                subcuentaTipo = SubcuentaTipo.SinSubcuentas;
                empresaId = null;
                // Al ser cuenta madre (AsientoOk = False), pierde sus grupos
                Grupos?.Clear();
            }

            Codigo = parametros.Codigo;
            Nombre = parametros.Nombre;
            AsientoOk = parametros.AsientoOk;
            SubcuentaTipo = subcuentaTipo;
            MonedaTipo = parametros.MonedaTipo;
            AjustaOk = parametros.AjustaOk;
            EmpresaId = empresaId;
            CuentaIdMadre = parametros.CuentaIdMadre;

            return Resultado.Exito();
        }

        public void AsignarGrupos(IEnumerable<Grupo> nuevosGrupos)
        {
            if (Grupos == null)
            {
                Grupos = new List<Grupo>();
            }
            
            Grupos.Clear();
            if (nuevosGrupos != null)
            {
                foreach (var grupo in nuevosGrupos)
                {
                    Grupos.Add(grupo);
                }
            }
        }
    }
}

