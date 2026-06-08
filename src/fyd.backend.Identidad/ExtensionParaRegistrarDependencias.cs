using fyd.backend.Identidad.ORM;
using fyd.backend.Identidad.Servicios;
using fyd.backend.Identidad.Servicios.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace fyd.backend.Identidad
{
    public static class ExtensionParaRegistrarDependencias
    {
        public static IServiceCollection AgregarDependenciasDeIdentidad(this IServiceCollection servicios, IConfiguration configuration)
        {
            var adminConnectionString = configuration.GetConnectionString("DbIdentidad") ?? throw new InvalidOperationException("Connection string 'DbIdentidad' not found.");
            servicios.AddDbContext<ContextoSeguridad>(options =>
                options.UseSqlServer(adminConnectionString));// Identity services registration logic goes here

            // 1. Identity para la gestión de datos (Core + Roles)
            servicios.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false; // Configura según tu necesidad
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ContextoSeguridad>();

            // 2. Configuración de JWT
            const string SecretKey = "TU_CLAVE_SUPER_SECRETA_DE_MINIMO_32_CARACTERES"; // Usa variables de entorno
            var key = Encoding.ASCII.GetBytes(SecretKey);

            servicios.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // Setealo en true en prod
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
            });

            servicios.AddScoped<ISincronizadorPermisos, SincronizadorPermisos>();

            return servicios;
        }
    }
}
