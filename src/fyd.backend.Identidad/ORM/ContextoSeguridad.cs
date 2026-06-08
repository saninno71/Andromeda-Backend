
using fyd.backend.Identidad.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace fyd.backend.Identidad.ORM
{
    public class ContextoSeguridad(DbContextOptions<ContextoSeguridad> options) : IdentityDbContext(options)
    {
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de la llave compuesta para la tabla intermedia
            builder.Entity<RolPermiso>()
                .HasKey(rp => new { rp.RoleId, rp.PermisoId });

            // Relaciones
            builder.Entity<RolPermiso>()
                .HasOne(rp => rp.Rol)
                .WithMany() // No necesitamos la colección inversa en Role por ahora
                .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolPermiso>()
                .HasOne(rp => rp.Permiso)
                .WithMany()
                .HasForeignKey(rp => rp.PermisoId);
        }
    }
}
