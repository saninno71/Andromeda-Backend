using System;
using System.Linq;
using System.Threading.Tasks;
using fyd.backend.Aplicacion.Compras.DTOs;
using fyd.backend.Aplicacion.Compras.Servicios;
using fyd.backend.Dominio.Compras.Entidades;
using fyd.backend.Dominio.Compras.Valores;
using fyd.backend.Infraestructura.Compras.Mapeos;
using fyd.backend.Infraestructura.Compras.Repositorios;
using fyd.backend.Infraestructura.Infraestructura;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fyd.backend.IntegrationTests.Compras
{
    [TestClass]
    public sealed class ProveedorIntegrationTest
    {
        [TestMethod]
        public async Task Agregar_GrafoCompleto_RescataYCompruebaMapeo()
        {
            // Arrange
            var opcion = new DbContextOptionsBuilder<ContextoAplicacion>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var contexto = new ContextoAplicacion(opcion);
            var repositorio = new ProveedorRepositorio(contexto);
            var unidadDeTrabajo = new UnidadDeTrabajo(contexto);
            var servicio = new ProveedorServicio(repositorio, unidadDeTrabajo);

            var agendaDatos = new InfoAgendaDto(
                1, "Test Proveedor S.A", "", "", "Guemes", 123, "", "", 1, "4000",
                1, "30-12345678-9", "IB-Test", "test@test.com", "", "", "", "",
                "Oficina", "3811234567", "", "", "", "", "", "", "", "",
                "", "", "", "", false, 0, "", false, "Sin Memo"
            );

            var retencion = new InfoRetencionDto(12, 1, null, null); // 12 -> IVARetencion

            var dto = new CrearProveedorDto(
                Codigo: 155, AgendaId: 0, ProveedorCcId: 0, IvaCategoriaId: 1, CondicionId: 1,
                CategoriaId: 1, CalificacionId: 1, CuentaId: null, MonedaId: null, PorcDescuento1: 5,
                PorcDescuento2: 0, Origen: 1, ImpCredito: 10000, Situacion: 1, EventualOk: false,
                SujetoVinculadoOk: false, AgendaDatos: agendaDatos, Lineas: new int[] { 3, 5 },
                Retenciones: new InfoRetencionDto[] { retencion }
            );

            // Act
            var resultado = await servicio.Crear(dto);

            // Assert
            Assert.IsTrue(resultado.Exitoso, "El servicio debería haber devuelto éxito al crear el grafo de proveedor.");
            
            // Re-fetch saltando el Tracking normal
            var proveedorExtraido = await contexto.Proveedores
                .Include(p => p.Agenda)
                .Include(p => p.Lineas)
                .Include(p => p.Retenciones)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == resultado.Valor);

            Assert.IsNotNull(proveedorExtraido);
            Assert.AreEqual(155, proveedorExtraido.Codigo);
            
            // Comprobación de colecciones guardadas en las tablas pivot
            Assert.HasCount(2, proveedorExtraido.Lineas);
            Assert.IsTrue(proveedorExtraido.Lineas.Any(l => l.LineaId == 3));
            Assert.IsTrue(proveedorExtraido.Lineas.Any(l => l.LineaId == 5));
            
            Assert.HasCount(1, proveedorExtraido.Retenciones);
            Assert.AreEqual(12, proveedorExtraido.Retenciones.First().RetencionId);

            // Comprobación de Agenda atómica
            Assert.IsNotNull(proveedorExtraido.Agenda);
            Assert.AreEqual("Test Proveedor S.A", proveedorExtraido.Agenda.Nombre);
            Assert.AreEqual("30-12345678-9", proveedorExtraido.Agenda.NumeroDoc);
            Assert.AreEqual("test@test.com", proveedorExtraido.Agenda.Email1);
        }

        [TestMethod]
        public async Task Eliminar_ConProveedorEnMovimiento_FallaPorDependencias()
        {
            // Arrange
            var opcion = new DbContextOptionsBuilder<ContextoAplicacion>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var contexto = new ContextoAplicacion(opcion);
            var repositorio = new ProveedorRepositorio(contexto);
            var unidadDeTrabajo = new UnidadDeTrabajo(contexto);
            var servicio = new ProveedorServicio(repositorio, unidadDeTrabajo);

            // Crear proveedor a través del servicio (garantiza que ObtenerPorId lo encuentra).
            var agendaDatos = new InfoAgendaDto(
                null, "Proveedor Test", "", "", "", null, "", "", 0, "",
                1, "20-12345678-0", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", false, 0, "", false, ""
            );
            var crearDto = new CrearProveedorDto(
                888, 0, 0, 1, 1, 1, 1, null, null, 0, 0, 1, 0, 1,
                false, false, agendaDatos, Array.Empty<int>(), Array.Empty<InfoRetencionDto>()
            );
            var crearResultado = await servicio.Crear(crearDto);
            Assert.IsTrue(crearResultado.Exitoso, "Setup: debería poder crear el proveedor base.");
            var proveedorId = crearResultado.Valor;

            // Act: intentar actualizar con ProveedorCcId apuntando al mismo proveedor (referencia circular).
            var dto = new ActualizarProveedorDto(
                proveedorId, 888, 0, proveedorId,
                1, 1, 1, 1, null, null, 0, 0, 1, null, 1, 0, false, false, null,
                Array.Empty<int>(), Array.Empty<InfoRetencionDto>()
            );
            
            var resultado = await servicio.Actualizar(dto);

            // Assert
            Assert.IsFalse(resultado.Exitoso, "Debería fallar la circularidad referencial a través de la Db y Repo real.");
            Assert.AreEqual("Proveedor.CircularidadCc", resultado.Error!.Codigo);
        }
    }
}
