namespace fyd.backend.Dominio.Seguridad
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RequerirPermisoAttribute : Attribute
    {
        public string Permiso { get; }
        public RequerirPermisoAttribute(string permiso) => Permiso = permiso;
    }
}
