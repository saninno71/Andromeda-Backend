using fyd.backend.API.Excepciones.Manejadores;
using fyd.backend.API.Extensions;
using fyd.backend.API.Middlewares;
using fyd.backend.API.OData;
using fyd.backend.Aplicacion;
using fyd.backend.Identidad;
using fyd.backend.Identidad.Servicios.Interfaces;
using fyd.backend.Infraestructura;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.OData;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AgregarDependenciasDeIdentidad(builder.Configuration);
builder.Services.AgregarServiciosDeAplicacion(builder.Configuration);
builder.Services.AgregarInfraestructura(builder.Configuration);
builder.Services.AgregarPermisos();
builder.Services.AgregarSwagger();

//INICIO agregado por SAN
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
//FIN agregado por SAN

builder.Services.AddControllers(opciones =>
{
    var politicaDeAutorizacion = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    opciones.Filters.Add(new AuthorizeFilter(politicaDeAutorizacion));
}).AddOData(opciones => opciones
        .AddRouteComponents("odata", ModeloEdm.ObtenerModelo())
        .Select()
        .Filter()
        .OrderBy()
        .SetMaxTop(20)
        .Count()
        .Expand()
    ); ;

builder.Services.AddExceptionHandler<ManejadorGlobalDeExcepciones>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

//se crean los nuevos permisos a demanda en la base de datos.
using (var scope = app.Services.CreateScope())
{
    var sincronizador = scope.ServiceProvider.GetRequiredService<ISincronizadorPermisos>();
    await sincronizador.SincronizarDesdeCodigo();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TransaccionMiddleware>();

app.UseHttpsRedirection();

//INICIO agregado SAN
app.UseCors("ReactPolicy");
//FIN agregado SAN


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
