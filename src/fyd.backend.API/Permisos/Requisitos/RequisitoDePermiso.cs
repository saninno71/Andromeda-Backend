using Microsoft.AspNetCore.Authorization;

namespace fyd.backend.API.Permisos.Requisitos
{
    public class RequisitoDePermiso : IAuthorizationRequirement
    {
        public string Permiso { get; }
        public RequisitoDePermiso(string permission) => Permiso = permission;
    }
}
