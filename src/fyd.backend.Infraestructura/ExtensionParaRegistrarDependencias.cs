using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Dominio.General.Repositorios;
using fyd.backend.Dominio.Ventas.Repositorios;
using fyd.backend.Dominio.Compras.Repositorios;
using fyd.backend.Infraestructura.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.General.Repositorios;
using fyd.backend.Infraestructura.Ventas.Repositorios;
using fyd.backend.Infraestructura.Compras.Repositorios;
using fyd.backend.Infraestructura.Infraestructura;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


namespace fyd.backend.Infraestructura
{
    public static class ExtensionParaRegistrarDependencias
    {
        public static IServiceCollection AgregarInfraestructura(this IServiceCollection servicios, IConfiguration configuracion)
        {

            servicios.AddDbContextFactory<ContextoAplicacion>(options =>
            {
                options.UseSqlServer(configuracion.GetConnectionString("DbGestion"));
            });

            servicios.AddDbContextFactory<ContextoLectura>(options =>
            {
                options.UseSqlServer(configuracion.GetConnectionString("DbGestion"), option => option.CommandTimeout(300));
            });

            servicios.AddSingleton(typeof(IServicioLogueo<>), typeof(ServicioLogueo<>));
            servicios.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();

            //contexto aplicación
            servicios.AddScoped<IPlanDeCuentaRepositorio, PlanDeCuentaRepositorio>();
            servicios.AddScoped<IAsientoRepositorio, AsientoRepositorio>();
            servicios.AddScoped<IConsolidacionRepositorio, ConsolidacionRepositorio>();
            servicios.AddScoped<IBloqueoRepositorio, BloqueoRepositorio>();
            servicios.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            servicios.AddScoped<IProveedorRepositorio, ProveedorRepositorio>();
            servicios.AddScoped<IIndiceContableRepositorio, IndiceContableRepositorio>();
            servicios.AddScoped<fyd.backend.Dominio.Ventas.Repositorios.IClienteRepositorio, fyd.backend.Infraestructura.Ventas.Repositorios.ClienteRepositorio>();



            //contexto lectura
            servicios.AddScoped<IPlanDeCuentaProcedimientoAlmacenado, PlanDeCuentaProcedimientoAlmacenado>();
            servicios.AddScoped<IAsientoProcedimientoAlmacenado, AsientoProcedimientoAlmacenado>();
            servicios.AddScoped<IMayoresProcedimientoAlmacenado, MayoresProcedimientoAlmacenado>();
            servicios.AddScoped<IBalancesProcedimientoAlmacenado, BalancesProcedimientoAlmacenado>();
            servicios.AddScoped<IAjusteXInflacionBCProcedimientoAlmacenado, AjusteXInflacionBCProcedimientoAlmacenado>();
            servicios.AddScoped<IClienteProcedimientoAlmacenado, ClienteProcedimientoAlmacenado>();
            servicios.AddScoped<IProveedoresProcedimientoAlmacenado, ProveedoresProcedimientoAlmacenado>();
            servicios.AddScoped<IImpositivoIIBBBsAsProcedimientoAlmacenado, ImpositivoIIBBBsAsProcedimientoAlmacenado>();
            servicios.AddScoped<IImpositivoIIBBCABAProcedimientoAlmacenado, ImpositivoIIBBCABAProcedimientoAlmacenado>();
            servicios.AddScoped<IImpositivoIVAProcedimientoAlmacenado, ImpositivoIVAProcedimientoAlmacenado>();
            servicios.AddScoped<IImpositivoLIDProcedimientoAlmacenado, ImpositivoLIDProcedimientoAlmacenado>();
            servicios.AddScoped<IImpositivoSiCoReProcedimientoAlmacenado, ImpositivoSiCoReProcedimientoAlmacenado>();
            servicios.AddScoped<IImpositivoSiFeReProcedimientoAlmacenado, ImpositivoSiFeReProcedimientoAlmacenado>();
            servicios.AddScoped<IImpositivoSujetosVinculadosProcedimientoAlmacenado, ImpositivoSujetosVinculadosProcedimientoAlmacenado>();
            servicios.AddScoped<IImpositivoDecreto3685ProcedimientoAlmacenado, ImpositivoDecreto3685ProcedimientoAlmacenado>();
            servicios.AddScoped<ICstctbIndicesProcedimientoAlmacenado, CstctbIndicesProcedimientoAlmacenado>();
            servicios.AddScoped<fyd.backend.Dominio.Ventas.Repositorios.IClienteProcedimientoAlmacenado, fyd.backend.Infraestructura.Ventas.Repositorios.ClienteProcedimientoAlmacenado>();

            var logger = new LoggerConfiguration()
                           .Enrich.FromLogContext()
                           .ReadFrom.Configuration(configuracion)
                           .CreateLogger();

            logger.Information($"Logger inicializado exitosamente");

            servicios.AddSerilog(logger);
            return servicios;
        }
    }
}
