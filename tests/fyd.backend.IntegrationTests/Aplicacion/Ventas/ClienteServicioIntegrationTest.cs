using fyd.backend.Aplicacion.Ventas.DTOs;
using fyd.backend.Aplicacion.Ventas.Servicios;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Ventas.Enums;
using fyd.backend.Dominio.Ventas.Repositorios;
using fyd.backend.Infraestructura.Infraestructura;
using fyd.backend.Infraestructura.ORM;
using fyd.backend.Infraestructura.Ventas.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace fyd.backend.IntegrationTests.Aplicacion.Ventas
{
    [TestClass]
    public class ClienteServicioIntegrationTest
    {
        private ContextoAplicacion ObtenerContextoBdEnMemoria()
        {
            var options = new DbContextOptionsBuilder<ContextoAplicacion>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            var contexto = new ContextoAplicacion(options);
            contexto.Database.EnsureCreated();
            return contexto;
        }

        [TestMethod]
        public async Task Crear_ClienteValido_PersisteEnBaseDeDatos()
        {
            // Arrange
            var contexto = ObtenerContextoBdEnMemoria();
            IClienteRepositorio repositorio = new ClienteRepositorio(contexto);
            IUnidadDeTrabajo unidadDeTrabajo = new UnidadDeTrabajo(contexto);
            var servicio = new ClienteServicio(repositorio, unidadDeTrabajo);

            var dto = new CrearClienteDto(
                Codigo: 100,
                Nombre: "Integracion Test",
                Nombre2: null, Titulo: null, TipoDocId: 1, NumeroDoc: "20123456789", Ib: null, IibbProvinciaId: null, 
                Calle: null, Altura: null, PisoDpto: null, Barrio: null, Cp: null, LocalidadId: null, 
                Telefonos: null, Web: null, Emails: null, IvaCategoriaId: 1, PorcIvaLiberado: 0, 
                VendedorId: 1, CobradorId: 1, CondicionId: 1, ImpCredito: 0, MonedaId: 1, ListaId: null, 
                PorcDescuento1: 0, PorcDescuento2: 0, PorcDescuento3: 0, TransporteId: 1, DepositoId: null, 
                DomEntregaId: null, CalificacionId: 1, CategoriaId: 1, ZonaId: 1, FechaAlta: null, FechaBaja: null, 
                Situacion: SituacionCliente.ActivoNormal, EventualOk: false, SujetoVinculadoOk: false, ClienteCcId: null, 
                CuentaId: null, AgendaEmpresaId: null, Variable1: null, Variable2: null, Variable3: null, 
                Variable4: null, LineasIds: null, DatosAdicionales: null, Percepciones: null, Observaciones: null
            );

            // Act
            var resultado = await servicio.Crear(dto);

            // Assert
            Assert.IsTrue(resultado.Exitoso);
            var bdCliente = await contexto.Clientes.FirstOrDefaultAsync(c => c.Id == resultado.Valor);
            Assert.IsNotNull(bdCliente);
            Assert.AreEqual(100, bdCliente.Codigo);
        }
    }
}
