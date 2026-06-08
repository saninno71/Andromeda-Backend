using fyd.backend.API.Permisos.Manejadores;
using fyd.backend.API.Permisos.Politicas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;

namespace fyd.backend.API.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AgregarSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FyD Andromeda Web - API", Version = "v1" });

                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("bearer", document)] = []
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });

            return services;
        }

        public static IServiceCollection AgregarPermisos(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, ProveedorDePoliticasDePermisos>();
            services.AddScoped<IAuthorizationHandler, ManejadorDeAutorizacionDePermisos>();
            return services;
        }
    }
}
