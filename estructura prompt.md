## Consideraciones:

[Entidad] refiere a la entidad que se está creando. Ejemplo: "Cliente". Dependiendo de la capa en la que nos encontremos, esto puede significar distintas cosas. Por ejemplo, en la capa de Infraestructura, [Entidad] se refiere a la entidad de EF Core, mientras que en la capa de Dominio, [Entidad] se refiere a la entidad de dominio.

[Modulo] refiere al módulo al que pertenece la entidad. Ejemplo: "Clientes" pertenece al módulo de "Ventas". Si no está claro al leer la documentación, preguntar.

Para lo que a ContextoLectura se refiere:

Se basa en un SP de consulta. El nombre del SP de consulta tiene un nombre compuesto por *cst* + [Nombre del SP de consulta en la base de datos]. Debe ser facilitado con la documentación junto a los parámetros del mismo y el tipo de respuesta que da. En caso de no tenerlo disponible, pedirlo.

Los ReadModel tienen un nombre compuesto por *cst* + [Nombre del SP de consulta en la base de datos] + *ReadModel.cs*. Si el mismo no estuviera facilitado, pedirlo.

Los parámetros de consulta tienen un nombre compuesto por *cst* + [Nombre del SP de consulta en la base de datos] + *Params.cs*. Si el mismo no estuviera facilitado, pedirlo. Los parámetros son los utilizados

## Output de src
- fyd.backend.API/
    - Controllers/[Modulo]/
        - [Entidad]Controller.cs
            - Agregar los permisos en [RequerirPermiso]
    - OData/
        - ModeloEdm.cs -> Agregar [Entidad]ReadModel
    - ParametrosDeConsulta/[Modulo]
        -Cst[NombreSP]Params.cs             
- fyd.backend.Aplicacion/[Modulo]
    - Mapeos/[Entidad]DtoMapeo.cs -> Mapeo desde DTO a Parámtros para crear la entidad de Dominio y para crear el DTO a partir de la entidad de Dominio cuando se trata de una operación de lectura.
    - Validadores/[Modulo]DTOsValidador.cs -> Listado de validadores para los DTOS
    - Servicios/
        - Interfaces/I[Entidad]Servicio.cs
        - [Entidad]Servicio.cs
    - Registrar dependencias en ExtensionParaRegistrarDependencias.cs
- fyd.backend.Dominio/[Modulo]
    - Consultas/
        - Cst[NombreStoredProcedureConsulta]ReadModel.cs
    - Repositorios/
        - I[Entidad]Repositorio.cs
        - I[Entidad]ProcedimientoAlmacenado.cs
    - Entidades/
        - [Entidad].cs -> Crear las entidades para los casos con sus invariantes. Implementa un Crear() y un Actualizar() que utiliza los parámetros definidos en Parametros/[Entidad]Parametros.cs
    - Enums/ -> Crear los enums que se utilizan en la entidad.
    - Errores/
        - [Entidad]CodigoError.cs -> Crear los constantes de error  que se utilizan en la entidad.
        - [Entidad]Error.cs -> Crear los errores que se utilizan en la entidad utilizando la clase Error.
    - Parametros/
        [Entidad]Parametros.cs -> Se declaran los parámetros que utilizaran el método Crear() de cada entidad
- fyd.backend.Infraestructura/
    - [Modulo]/
        - Entidades/[Entidad].cs
        - Mapeos/[Entidad]Mapeo.cs
        - Repositorios/[Entidad]Repositorio.cs
        - Repositorios/[Entidad]ProcedimientoAlmacenado.cs
    - Contextos/
        - ContextoAplicacion.cs --> Agrega [Entidad].
        - ContextoLectura.cs --> Agregar [Entidad]ReadModel

## Output de tests
- fyd.backend.UnitTests/
    - Dominio/[Modulo]/
        - [Entidad]Test.cs -> Tests unitarios del Rich Domain Model (Validando Invariantes y factory method).
    - Aplicacion/[Modulo]/Servicios/
        - [Entidad]ServicioTest.cs -> Tests unitarios aislando la infraestructura con 'Moq'.
    - API/Controllers/[Modulo]/
        - [Entidad]ControllerTest.cs -> Tests unitarios de traducción de Resultados y códigos HTTP.
- fyd.backend.IntegrationTests/
    - Aplicacion/[Modulo]/
        - [Entidad]ServicioIntegrationTest.cs -> Pruebas testeando flujos completos con un In-Memory Database Context y Unit of Work real.