using Microsoft.AspNetCore.Identity;

namespace fyd.backend.Identidad.Entidades
{
    public class RolPermiso
    {

        public string RoleId { get; set; } = String.Empty; // FK a AspNetRoles
        public int PermisoId { get; set; } // FK a Permisos

        // Navegación
        public Permiso? Permiso { get; set; }
        public IdentityRole? Rol { get; set; }
    }
}
