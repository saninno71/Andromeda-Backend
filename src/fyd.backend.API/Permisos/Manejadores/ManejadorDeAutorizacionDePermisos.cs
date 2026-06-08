namespace fyd.backend.API.Permisos.Manejadores
{
    using fyd.backend.API.Permisos.Requisitos;
    using fyd.backend.Identidad.ORM;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;

    public class ManejadorDeAutorizacionDePermisos(IServiceScopeFactory scopeFactory) : AuthorizationHandler<RequisitoDePermiso>
    {
        // Usamos IServiceScopeFactory porque los Handlers son Singleton por defecto
        // y el DbContext es Scoped.
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext contexto,
            RequisitoDePermiso requisito)
        {
            var userId = contexto.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return;

            using (var scope = _scopeFactory.CreateScope())
            {
                // OJO: Aquí es donde más adelante meteremos CACHÉ (Redis/Memory)               
                var contextoBD = scope.ServiceProvider.GetRequiredService<ContextoSeguridad>();

                // 1. Buscamos los roles del usuario
                // Nota: Podríamos leer los roles del Token JWT si ya los metimos ahí, 
                // ahorrando esta consulta. Asumamos que el JWT tiene roles.
                var rolesDeUsuario = contexto.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

                // 2. Consultamos la Matriz: ¿Alguno de mis roles tiene este permiso?
                var tienePermiso = await contextoBD.RolPermisos
                    .AnyAsync(rp => rolesDeUsuario.Contains(rp.Rol!.Name!) &&
                                    rp.Permiso!.Nombre == requisito.Permiso);

                if (tienePermiso)
                {
                    contexto.Succeed(requisito);
                }
            }
        }
    }
}
