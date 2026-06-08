# fyd-backend

Backend de FyD Web — Sistema de Gestión ERP migrado de VB6 a .NET 10.

## Stack Tecnológico

| Tecnología | Versión | Uso |
|---|---|---|
| .NET | 10.0 | Framework principal |
| Entity Framework Core | 10.x | ORM (SQL Server) |
| ASP.NET Core Identity | 10.x | Autenticación y Autorización |
| JWT Bearer | — | Tokens de autenticación |
| FluentValidation | — | Validación de DTOs |
| Serilog | — | Logging estructurado |
| OData | 9.x | Consultas parametrizadas |
| Swashbuckle | 10.x | Documentación Swagger |
| MSTest | 4.x | Framework de Tests |
| Moq | 4.x | Mocking para Unit Tests |

## Arquitectura

El proyecto sigue una **Clean Architecture** (Arquitectura Limpia) con separación en 5 capas:

```
fyd-backend/
├── src/
│   ├── fyd.backend.API              # Capa de Presentación (Controllers, Middlewares, Swagger)
│   ├── fyd.backend.Aplicacion       # Capa de Aplicación (Servicios, DTOs, Validadores)
│   ├── fyd.backend.Dominio          # Capa de Dominio (Entidades, Enums, Errores, Interfaces de Repositorio)
│   ├── fyd.backend.Identidad        # Capa de Identidad (ASP.NET Identity, JWT, Permisos)
│   └── fyd.backend.Infraestructura  # Capa de Infraestructura (EF Core, Repositorios, Unit of Work)
├── tests/
│   ├── fyd.backend.UnitTests         # Tests unitarios (Moq + MSTest)
│   └── fyd.backend.IntegrationTests  # Tests de integración (InMemoryDatabase + MSTest)
└── fyd.backend.slnx                  # Solución .NET
```

### Flujo de dependencias

```
API → Aplicación → Dominio ← Infraestructura
                     ↑
                  Identidad
```

- **Dominio** no depende de nada externo (es el núcleo).
- **Aplicación** depende solo de Dominio (servicios, DTOs).
- **Infraestructura** implementa las interfaces definidas en Dominio (repositorios, EF Core).
- **Identidad** maneja la autenticación/autorización con su propio `DbContext`.
- **API** orquesta todo y expone los endpoints.

## Patrones Implementados

### Result Pattern (`Resultado<T>`)
En lugar de lanzar excepciones para validaciones de negocio, los métodos devuelven un `Resultado<T>` o `Resultado` que encapsula el éxito o el error. Esto permite un manejo predecible de errores sin abusar de las excepciones.

### Rich Domain Model (Entidades con lógica)
Las entidades del dominio contienen lógica de validación propia. Por ejemplo, `CuentaContable` valida que su código y nombre no estén vacíos, y aplica reglas de negocio internas (ej: si `AsientoOk = false`, fuerza `SubcuentaTipo = SinSubcuentas`).

### Unit of Work (`IUnidadDeTrabajo`)
Encapsula el `SaveChangesAsync()` de EF Core para coordinar la persistencia de múltiples operaciones como una sola transacción.

### Repository Pattern
Las interfaces de repositorio se definen en **Dominio** y se implementan en **Infraestructura**, desacoplando el acceso a datos del negocio.

### DTO + FluentValidation
Los DTOs (`CrearCuentaDto`, `ActualizarCuentaDto`, `ConsultarCuentaDto`) se validan con FluentValidation antes de llegar al servicio, garantizando que los datos de entrada cumplan las restricciones de la base de datos.

### Permisos basados en atributos (`[RequerirPermiso]`)
Cada endpoint puede decorarse con `[RequerirPermiso("Modulo.Recurso.Accion")]` para definir qué permiso se necesita. Los permisos se sincronizan automáticamente desde el código a la base de datos de identidad al iniciar la aplicación.

### Consultas Complejas (OData + Stored Procedures)
Para reportes o consultas que exceden la lógica simple de EF Core, se utilizan Read Models en el dominio y un `ContextoLectura` en infraestructura que ejecuta SPs legacy, exponiéndolos como `IQueryable` para mantener la compatibilidad con OData (filtros, ordenamiento, paginación).

## Bases de datos

El sistema utiliza **dos bases de datos** separadas, cada una con su propio `DbContext`:

| Base de datos | DbContext | Propósito |
|---|---|---|
| `DbGestion` | `ContextoAplicacion` | Datos del negocio (cuentas, asientos, clientes, etc.) |
| `DbIdentidad` | `ContextoSeguridad` | Autenticación, roles y permisos (ASP.NET Identity) |

## Comandos EF Core

### Crear la base de datos de Identidad

Si la base de datos de Identidad (`DbIdentidad`) aún no existe en tu instancia de SQL Server, podés crearla automáticamente ejecutando el siguiente comando desde la raíz del proyecto:

```bash
dotnet ef database update --project src/fyd.backend.Identidad --startup-project src/fyd.backend.API --context ContextoSeguridad
```

Este comando aplicará todas las migraciones pendientes y creará la base de datos en la instancia configurada en el `appsettings.json` o en el `DesignTimeDataContextFactory`.

> **Tip:** Si necesitás apuntar a otra instancia o base de datos, modificá la cadena de conexión en el archivo correspondiente antes de ejecutar el comando.

### Base de Identidad (`ContextoSeguridad`)

Todos los comandos de migraciones de Identidad deben ejecutarse desde la raíz del proyecto, indicando el proyecto de Identidad como origen y el proyecto API como startup:

```bash
# Agregar una nueva migración
dotnet ef migrations add NombreDeLaMigracion --project src/fyd.backend.Identidad --startup-project src/fyd.backend.API --context ContextoSeguridad

# Aplicar migraciones pendientes
dotnet ef database update --project src/fyd.backend.Identidad --startup-project src/fyd.backend.API --context ContextoSeguridad

# Revertir a una migración anterior
dotnet ef database update NombreDeLaMigracion --project src/fyd.backend.Identidad --startup-project src/fyd.backend.API --context ContextoSeguridad

# Eliminar la última migración (si aún no fue aplicada)
dotnet ef migrations remove --project src/fyd.backend.Identidad --startup-project src/fyd.backend.API --context ContextoSeguridad

# Listar migraciones
dotnet ef migrations list --project src/fyd.backend.Identidad --startup-project src/fyd.backend.API --context ContextoSeguridad
```

## Tests

### Tipos de tests implementados

| Tipo | Proyecto | Herramientas | Qué se testea |
|---|---|---|---|
| **Unit Tests** | `fyd.backend.UnitTests` | MSTest + Moq | Servicios, Controladores, Entidades del dominio |
| **Integration Tests** | `fyd.backend.IntegrationTests` | MSTest + InMemoryDatabase | Repositorios, Servicio + Repositorio + DB |

### Ejecutar tests

```bash
# Todos los tests
dotnet test

# Solo unit tests
cd tests/fyd.backend.UnitTests && dotnet test

# Solo integration tests
cd tests/fyd.backend.IntegrationTests && dotnet test
```

### Filosofía de testing
- **Dominio**: Se testea la lógica interna de las entidades (ej: validaciones de `CuentaContable.Crear()`).
- **Aplicación**: Se testea la orquestación de los servicios mockeando repositorios (ej: que `CrearCuenta` falle si el código ya existe).
- **Controladores**: Se testea que las respuestas HTTP sean correctas según los resultados del servicio (ej: `404` si no se encuentra, `201` al crear).
- **Integración**: Se testea el flujo completo con base de datos en memoria (ej: nivel máximo de jerarquía, referencias circulares).

## Ejecución local

```bash
# Restaurar paquetes
dotnet restore

# Compilar
dotnet build

# Ejecutar (modo desarrollo)
dotnet run --project src/fyd.backend.API

# Acceder a Swagger
# https://localhost:{puerto}/swagger
```
