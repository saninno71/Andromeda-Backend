using System.Reflection;
using FluentValidation;
using fyd.backend.Aplicacion.Contabilidad.Servicios;
using fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces;
using fyd.backend.Aplicacion.Ventas.Servicios;
using fyd.backend.Aplicacion.Ventas.Servicios.Interfaces;
using fyd.backend.Aplicacion.Compras.Servicios;
using fyd.backend.Aplicacion.Compras.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fyd.backend.Aplicacion
{
    public static class ExtensionParaRegistrarDependencias
    {
        public static IServiceCollection AgregarServiciosDeAplicacion(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddScoped<IPlanDeCuentaServicio, PlanDeCuentaServicio>();
            servicios.AddScoped<IAsientoServicio, AsientoServicio>();
            servicios.AddScoped<IConsolidacionServicio, ConsolidacionServicio>();
            servicios.AddScoped<IClienteServicio,ClienteServicio>();
            servicios.AddScoped<IProveedorServicio, ProveedorServicio>();
            servicios.AddScoped<IIndiceContableServicio, IndiceContableServicio>();

            // Registramos todos los validadores automáticamente (busca clases que hereden de AbstractValidator)
            servicios.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return servicios;
        }
    }
}
