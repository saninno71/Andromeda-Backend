using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios;
using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Parametros;
using fyd.backend.Infraestructura.Contabilidad.Mapeos;
using fyd.backend.Infraestructura.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.Infraestructura;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fyd.backend.IntegrationTests.Aplicacion.Contabilidad
{
    [TestClass]
    public sealed class IndiceContableServicioIntegrationTest
    {
        [TestMethod]
        public async Task Crear_IndiceContable_Persiste_Correctamente()
        {
            // Arrange
            var opciones = new DbContextOptionsBuilder<ContextoAplicacion>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var contexto = new ContextoAplicacion(opciones);

            var repositorio = new IndiceContableRepositorio(contexto);
            var unidadDeTrabajo = new UnidadDeTrabajo(contexto);
            var servicio = new IndiceContableServicio(repositorio, unidadDeTrabajo);

            var dto = new CrearIndiceDto(new DateTime(2024, 3, 15), 150.5432m);

            // Act
            var resultado = await servicio.Crear(dto);

            // Assert
            Assert.IsTrue(resultado.Exitoso, "El servicio debería haber devuelto éxito.");

            var indicesEnDb = await contexto.IndicesContables.ToListAsync();
            Assert.HasCount(1, indicesEnDb);
            Assert.AreEqual(new DateTime(2024, 3, 1), indicesEnDb[0].Periodo);
            Assert.AreEqual(150.5432m, indicesEnDb[0].Indice);
        }

        [TestMethod]
        public async Task Crear_IndiceContable_Periodo_Duplicado_Falla()
        {
            // Arrange
            var opciones = new DbContextOptionsBuilder<ContextoAplicacion>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var contexto = new ContextoAplicacion(opciones);

            // Crear un índice existente directamente en el contexto
            var indiceInfra = IndiceContableMapeo.AInfraestructura(
                IndiceContable.Crear(new ParametrosIndice(new DateTime(2024, 3, 1), 100m)).Valor!);
            contexto.IndicesContables.Add(indiceInfra);
            await contexto.SaveChangesAsync();

            var repositorio = new IndiceContableRepositorio(contexto);
            var unidadDeTrabajo = new UnidadDeTrabajo(contexto);
            var servicio = new IndiceContableServicio(repositorio, unidadDeTrabajo);

            var dto = new CrearIndiceDto(new DateTime(2024, 3, 20), 200m);

            // Act
            var resultado = await servicio.Crear(dto);

            // Assert
            Assert.IsFalse(resultado.Exitoso, "Debería fallar por período duplicado.");
            Assert.AreEqual(IndiceContableCodigoError.PeriodoDuplicado, resultado.Error!.Codigo);

            var indicesEnDb = await contexto.IndicesContables.ToListAsync();
            Assert.HasCount(1, indicesEnDb);
        }

        [TestMethod]
        public async Task Eliminar_IndiceContable_Persiste_Correctamente()
        {
            // Arrange
            var opciones = new DbContextOptionsBuilder<ContextoAplicacion>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var contexto = new ContextoAplicacion(opciones);

            var indiceInfra = IndiceContableMapeo.AInfraestructura(
                IndiceContable.Crear(new ParametrosIndice(new DateTime(2024, 3, 1), 100m)).Valor!);
            contexto.IndicesContables.Add(indiceInfra);
            await contexto.SaveChangesAsync();

            var repositorio = new IndiceContableRepositorio(contexto);
            var unidadDeTrabajo = new UnidadDeTrabajo(contexto);
            var servicio = new IndiceContableServicio(repositorio, unidadDeTrabajo);

            // Act
            var resultado = await servicio.Eliminar(indiceInfra.Id);

            // Assert
            Assert.IsTrue(resultado.Exitoso);

            var indicesEnDb = await contexto.IndicesContables.ToListAsync();
            Assert.IsEmpty(indicesEnDb);
        }
    }
}
