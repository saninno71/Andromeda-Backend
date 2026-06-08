using fyd.backend.API.Permisos.Requisitos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace fyd.backend.API.Permisos.Politicas
{
    public class ProveedorDePoliticasDePermisos(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
    {
        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if (policy == null)
            {
                // Si la política no existe, la creamos usando el nombre del permiso
                policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new RequisitoDePermiso(policyName))
                    .Build();
            }

            return policy;
        }
    }
}
