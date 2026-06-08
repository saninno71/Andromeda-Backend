# Directivas para Agentes AI - FyD Backend

Este archivo contiene las convenciones, patrones y reglas que un agente AI debe seguir al trabajar en este proyecto.

## Arquitectura

Este proyecto usa **Clean Architecture** con 4 capas principales + un módulo de Identidad:

1. **Dominio** → Entidades, Interfaces, Errores, Enums
2. **Aplicación** → Servicios, DTOs, Validadores (FluentValidation)
3. **Infraestructura** → EF Core (DbContext, Configs, Repositorios)
4. **API** → Controllers, Middlewares
5. **Identidad** → ASP.NET Identity, JWT Bearer, ContextoSeguridad, Permisos

**Regla de dependencia:** Dominio ← Aplicación ← Infraestructura ← API. Nunca al revés.

### Estructura de Capas

#### Dominio (`fyd.backend.Dominio`)
- Contiene: **Entidades**, **Enums**, **Errores**, **Interfaces de Repositorio**, **Abstracciones**, **Seguridad**.
- Las entidades tienen **lógica de validación propia** (Rich Domain Model).
- Usa **constructor privado** para EF Core y un **método estático `Crear()`** que devuelve `Resultado<T>`.
- Las interfaces de repositorio se definen aquí para que Dominio no dependa de Infraestructura.

#### Aplicación (`fyd.backend.Aplicacion`)
- Contiene: **Servicios**, **DTOs**, **Validadores** (FluentValidation).
- Los DTOs se definen como `record` con constructor primario (positional records).
- Los servicios orquestan las reglas de negocio que exceden al conocimiento de la entidad.
- Los validadores de FluentValidation se registran automáticamente con `AddValidatorsFromAssembly`.

#### Infraestructura (`fyd.backend.Infraestructura`)
- Contiene: **Repositorios**, **Configuraciones de EF Core**, **DbContexts**, **Unit of Work**.
- Las configuraciones de EF Core usan `IEntityTypeConfiguration<T>` y se aplican con `ApplyConfigurationsFromAssembly`.
- El `ContextoAplicacion` agrupa los `DbSet` por módulo usando `#region`.

#### Identidad (`fyd.backend.Identidad`)
- Contiene: **ContextoSeguridad** (hereda de `IdentityDbContext`), **Migraciones**, **Servicios de autenticación/autorización**.
- Usa su propio `DbContext` (`ContextoSeguridad`) conectado a la base `DbAdministrador`.

#### API (`fyd.backend.API`)
- Contiene: **Controllers**, **Middlewares**, **Extensiones**, **Config de Swagger/OData**.
- Los controllers heredan de `ApiBaseController` que provee métodos: `ManejarOkResultado()`, `ManejarCreadoEnAccionResultado()`, `ManejarNoContenidoResultado()`. 
  - **Importante:** Al usar `ManejarCreadoEnAccionResultado`, se debe pasar el nombre de la acción GET (ej. `nameof(ObtenerPorId)`), no la acción de creación, para que el `Location` de HTTP 201 sea un enlace válido de consulta de la entidad creada.

---

## Convenciones de Código

### Idioma
- Todo el código (nombres de clases, métodos, propiedades, variables, comentarios) está en **español**.
- Los namespaces usan el formato: `fyd.backend.{Capa}.{Módulo}.{Subcarpeta}`

### Nomenclatura
- Entidades: `CuentaContable`, `Grupo`, `Asiento` (PascalCase, sustantivos)
- Servicios: `PlanDeCuentaServicio` (sufijo `Servicio`)
- Interfaces: `IPlanDeCuentaServicio`, `IPlanDeCuentaRepositorio` (prefijo `I`)
- DTOs: `CrearCuentaDto`, `ActualizarCuentaDto`, `ConsultarCuentaDto` (acción + entidad + `Dto`)
- Errores: `CuentaContableError` (entidad + `Error`)
- Configs EF: `CuentaContableConfig` (entidad + `Config`)
- Validadores: `CrearCuentaDtoValidator` (DTO + `Validator`)

### Colecciones
- En interfaces y propiedades de navegación, usar siempre `ICollection<T>` en vez de `List<T>`.
- En implementaciones internas que requieran una lista concreta, declarar la variable local como `var`.

---

## Cómo Crear una Nueva Entidad

1. **Dominio** → Crear la clase en `Dominio/{Módulo}/Entidades/`:
   - Constructor privado vacío (para EF Core)
   - Propiedades con `{ get; private set; }` 
   - Método estático `Crear(...)` que devuelve `Resultado<T>`
   - Método de instancia `Actualizar(...)` que devuelve `Resultado`
   - Propiedades de navegación como `ICollection<T>`

   ```csharp
   public class MiEntidad
   {
       private MiEntidad() { }
       public string Nombre { get; private set; } = string.Empty;
       
       public static Resultado<MiEntidad> Crear(string nombre) { ... }
       public Resultado Actualizar(string nombre) { ... }
   }
   ```

2. **Dominio** → Crear errores en `Dominio/{Módulo}/Errores/MiEntidadError.cs`:
   ```csharp
   public static class MiEntidadError
   {
       public static readonly Error NombreVacio = new("MiEntidad.NombreVacio", "El nombre es obligatorio.", TipoDeError.Validacion);
       public static Error NoEncontrada(int id) => new("MiEntidad.NoEncontrada", $"...", TipoDeError.NoEncontrado);
   }
   ```

3. **Dominio** → Crear interfaz de repositorio en `Dominio/{Módulo}/Repositorios/`:
   ```csharp
   public interface IMiEntidadRepositorio
   {
       Task Agregar(MiEntidad entidad);
       void Eliminar(MiEntidad entidad);
       Task<MiEntidad?> ObtenerPorId(int id);
   }
   ```

4. **Infraestructura** → Crear config EF en `Infraestructura/{Módulo}/Configuraciones/MiEntidadConfig.cs`:
   ```csharp
   public class MiEntidadConfig : IEntityTypeConfiguration<MiEntidad>
   {
       public void Configure(EntityTypeBuilder<MiEntidad> builder)
       {
           builder.ToTable("tblMiEntidad");
           builder.HasKey(e => e.Id);
           builder.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
       }
   }
   ```

5. **Infraestructura** → Agregar `DbSet` en `ContextoAplicacion.cs`:
   ```csharp
   public DbSet<MiEntidad> MisEntidades { get; set; }
   ```

6. **Infraestructura** → Implementar repositorio en `Infraestructura/{Módulo}/Repositorios/`.

7. **Aplicación** → Crear DTOs en `Aplicacion/{Módulo}/DTOs.cs` como `record`.

8. **Aplicación** → Crear validadores en `Aplicacion/{Módulo}/Validadores/` con `AbstractValidator<T>`.

9. **Aplicación** → Crear servicio en `Aplicacion/{Módulo}/Servicios/` e interfaz en `Servicios/Interfaces/`.

10. **API** → Crear controller en `API/Controllers/{Módulo}/` heredando de `ApiBaseController`.

11. **Registrar dependencias** en los archivos `ExtensionParaRegistrarDependencias.cs` de cada capa.

---

## Sistema de Permisos

Cada endpoint protegido se decora con `[RequerirPermiso("Modulo.Recurso.Accion")]`.

### Formato
`Modulo.Recurso.Accion`

### Ejemplos
- `Contabilidad.PlanDeCuentas.Consultar`
- `Contabilidad.PlanDeCuentas.Crear`
- `Contabilidad.PlanDeCuentas.Actualizar`
- `Contabilidad.PlanDeCuentas.Eliminar`

### Reglas
- Cada endpoint debe tener su `[RequerirPermiso(...)]`, incluyendo consultas OData.
- Los permisos se sincronizan automáticamente desde el código a la base de datos al iniciar la aplicación (`ISincronizadorPermisos.SincronizarDesdeCodigo()`).

---

## Cómo Crear Tests

### Unit Test (Dominio)
- Ubicación: `tests/fyd.backend.UnitTests/Dominio/{Módulo}/`
- Construir los `Parametros` de la Entidad e instanciar usando `Crear()`.
- Validar las **Invariantes** y requerimientos de validación garantizando la completitud del Objeto de Dominio.
- Confirmar que retorna el `Error` correspondiente si un campo u operación es inválido, sin requerir acceso a base de datos.

### Unit Test (Servicio)
- Ubicación: `tests/fyd.backend.UnitTests/Aplicacion/{Módulo}/Servicios/`
- Mockear repositorios con `Moq`
- Validar que `Resultado.Exitoso` sea `true`/`false`
- Verificar que se llamaron (o no) los métodos del repositorio con `Verify`

### Unit Test (Controller)
- Ubicación: `tests/fyd.backend.UnitTests/API/Controllers/{Módulo}/`
- Mockear el servicio con `Moq`
- Asignar `ControllerContext` con `DefaultHttpContext`
- Castear el resultado a `OkObjectResult`, `CreatedAtActionResult`, `NoContentResult`, etc.
- Verificar `StatusCode` y contenido

### Integration Test
- Ubicación: `tests/fyd.backend.IntegrationTests/`
- Usar `UseInMemoryDatabase(Guid.NewGuid().ToString())` para aislar cada test
- Instanciar `ContextoAplicacion`, repositorios y `UnidadDeTrabajo` reales
- Agregar datos de prueba al contexto antes de actuar

### Naming de tests
- Formato: `MetodoQueSeTestea_Condicion_ResultadoEsperado`
- Ejemplo: `Crear_CuentaContable_Falla_Codigo_Existente`
- Estructura **AAA**: Arrange → Act → Assert

---

## Patrón de Respuestas (Result Pattern)

- **NUNCA** lanzar excepciones para errores de negocio.
- Siempre devolver `Resultado` o `Resultado<T>` desde los servicios.
- Los errores tienen: `Codigo` (string), `Descripción` (string), `Tipo` (enum: `Validacion`, `NoEncontrado`, `Conflicto`).
- El `ApiBaseController` traduce automáticamente:
  - `TipoDeError.Validacion` → `400 Bad Request`
  - `TipoDeError.NoEncontrado` → `404 Not Found`
  - `TipoDeError.Conflicto` → `409 Conflict`

---

## Reglas de Negocio

- Las validaciones de **formato** de DTOs van con **FluentValidation** (capa Aplicación).
- Las validaciones de **negocio** (ej: código duplicado, referencia circular) van en el **Servicio** consultando al **Repositorio**.
- Las entidades encapsulan su propia lógica de creación/actualización y devuelven `Resultado`.

---

## Consultas Complejas (OData + SP)

Para consultas que requieren lógica compleja o dependen de procedimientos almacenados (SP) existentes, se sigue un patrón basado en **Read Models** y un **Contexto de Lectura** separado.

Existen 3 partes principales:

1. **Dominio**:
   - `Dominio/{Modulo}/Consultas/{Nombre}ReadModel.cs`: POCO que representa el resultado del SP. Sin lógica, solo propiedades.
   - `Dominio/{Modulo}/Repositorios/I{Nombre}ProcedimientoAlmacenado.cs`: Interfaz con métodos que devuelven `IQueryable<{ReadModel}>`.

2. **Infraestructura**:
   - `Infraestructura/Contextos/ContextoLectura.cs`: `DbContext` especializado para lectura. Se registra el ReadModel como `.HasNoKey()`.
   - `Infraestructura/{Modulo}/Repositorios/{Nombre}ProcedimientoAlmacenado.cs`: Implementación que ejecuta el SP usando `FromSqlInterpolated` o `FromSqlRaw`.

3. **API**:
   - `API/ParametrosDeConsulta/{Modulo}/{Nombre}Params.cs`: Clase para recibir los parámetros del endpoint.
   - `API/OData/ModeloEdm.cs`: Registro del ReadModel y sus funciones OData.
   - `API/Controllers/{Modulo}/`: Controller que utiliza el repositorio de SP para exponer la data.
     - **Regla para el Endpoint OData**: Para consultas complejas debe definirse usando `[HttpPost("odata/{Nombre}")]` (en vez de un GET) para recibir un volumen grande de parámetros o configuraciones desde `[FromBody] {Nombre}Params key`, y decorarse con `[EnableQuery]`. Este método debe retornar `ActionResult<IQueryable<{ReadModel}>>`.

### Ejemplo de IMPLEMENTACIÓN de un SP:
```csharp
public IQueryable<MiReadModel> ObtenerDatos(int param1, string param2)
{
    var resultado = _contexto.MisReadModels
        .FromSqlInterpolated($"EXECUTE [dbo].[miProcedimiento] {param1}, {param2}")
        .AsNoTracking()
        .ToList();

    return resultado.AsQueryable();
}
```

---

## Base de Datos

- ORM: **Entity Framework Core** (Code-First con configuraciones fluentes).
- Tabla pivot / many-to-many: Usar shadow entities (`Dictionary<string, object>`) con `UsingEntity`.
- Las tablas siguen la nomenclatura del sistema legacy con prefijo de módulo: `ctb` (contabilidad), `cps` (compras), `vts` (ventas), `fds` (fondos), `gnr` (general).
- Siempre configurar `HasKey`, `ToTable`, `HasMaxLength`, `IsRequired` en las configuraciones.
- Dos bases de datos separadas: `DbGestion` (`ContextoAplicacion`) y `DbAdministrador` (`ContextoSeguridad`).

---

## Comandos Útiles

```bash
# Compilar
dotnet build

# Ejecutar API
cd src/fyd.backend.API && dotnet run

# Ejecutar todos los tests
dotnet test

# Ejecutar tests con detalle
dotnet test --verbosity normal
```

### Migraciones EF Core (Base de Identidad)

```bash
# Agregar migración
dotnet ef migrations add NombreMigracion --project src/fyd.backend.Identidad --startup-project src/fyd.backend.API --context ContextoSeguridad

# Aplicar migraciones
dotnet ef database update --project src/fyd.backend.Identidad --startup-project src/fyd.backend.API --context ContextoSeguridad

# Listar migraciones
dotnet ef migrations list --project src/fyd.backend.Identidad --startup-project src/fyd.backend.API --context ContextoSeguridad

# Eliminar última migración (si no fue aplicada)
dotnet ef migrations remove --project src/fyd.backend.Identidad --startup-project src/fyd.backend.API --context ContextoSeguridad
```
