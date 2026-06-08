using fyd.backend.Dominio.Seguridad;
using fyd.backend.Identidad.Entidades;
using fyd.backend.Identidad.ORM;
using fyd.backend.Identidad.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Identidad.Servicios
{
    public class SincronizadorPermisos(ContextoSeguridad contexto) : ISincronizadorPermisos
    {
        private readonly ContextoSeguridad _contexto = contexto;

        public async Task SincronizarDesdeCodigo()
        {
            // 1. Usamos Reflexión para buscar en el Assembly de la API
            var permisosEnCodigo = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type)) // Solo controladores
                .SelectMany(type => type.GetMethods())
                .SelectMany(method => method.GetCustomAttributes(typeof(RequerirPermisoAttribute), true))
                .Cast<RequerirPermisoAttribute>()
                .Select(a => a.Permiso)
                .Distinct()
                .ToList();

            // 2. Obtener permisos actuales en la DB
            var permisosEnBaseDeDatos = await _contexto.Permisos.Select(p => p.Nombre).ToListAsync();

            // 3. Determinar cuáles faltan
            var nuevosPermisos = permisosEnCodigo
                .Except(permisosEnBaseDeDatos)
                .Select(p => new Permiso { Nombre = p, Descripcion = "Generado automáticamente" })
                .ToList();

            if (nuevosPermisos.Count != 0)
            {
                _contexto.Permisos.AddRange(nuevosPermisos);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}
