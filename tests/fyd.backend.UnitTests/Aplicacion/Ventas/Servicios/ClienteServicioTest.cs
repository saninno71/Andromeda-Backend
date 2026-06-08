using fyd.backend.Aplicacion.Ventas.DTOs;
using fyd.backend.Aplicacion.Ventas.Servicios;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Ventas.Entidades;
using fyd.backend.Dominio.Ventas.Enums;
using fyd.backend.Dominio.Ventas.Errores;
using fyd.backend.Dominio.Ventas.Parametros;
using fyd.backend.Dominio.Ventas.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace fyd.backend.UnitTests.Aplicacion.Ventas.Servicios
{
    [TestClass]
    public class ClienteServicioTest
    {
        private Mock<IClienteRepositorio> _clienteRepositorioMock;
        private Mock<IUnidadDeTrabajo> _unidadDeTrabajoMock;
        private ClienteServicio _clienteServicio;

        [TestInitialize]
        public void Setup()
        {
            _clienteRepositorioMock = new Mock<IClienteRepositorio>();
            _unidadDeTrabajoMock = new Mock<IUnidadDeTrabajo>();

            _clienteServicio = new ClienteServicio(
                _clienteRepositorioMock.Object,
                _unidadDeTrabajoMock.Object
            );
        }

        [TestMethod]
        public async Task Crear_Cliente_Falla_CodigoDuplicado()
        {
            // Arrange
            var dto = new CrearClienteDto(
                Codigo: 123,
                Nombre: "Test",
                Nombre2: null, Titulo: null, TipoDocId: 1, NumeroDoc: "20123456789", 
                Ib: null, IibbProvinciaId: null, Calle: null, Altura: null, 
                PisoDpto: null, Barrio: null, Cp: null, LocalidadId: null, 
                Telefonos: null, Web: null, Emails: null, IvaCategoriaId: 1, 
                PorcIvaLiberado: 0, VendedorId: 1, CobradorId: 1, CondicionId: 1, 
                ImpCredito: 0, MonedaId: 1, ListaId: null, PorcDescuento1: 0, 
                PorcDescuento2: 0, PorcDescuento3: 0, TransporteId: 1, 
                DepositoId: null, DomEntregaId: null, CalificacionId: 1, 
                CategoriaId: 1, ZonaId: 1, FechaAlta: null, FechaBaja: null, 
                Situacion: SituacionCliente.ActivoNormal, EventualOk: false, 
                SujetoVinculadoOk: false, ClienteCcId: null, CuentaId: null, 
                AgendaEmpresaId: null, Variable1: null, Variable2: null, 
                Variable3: null, Variable4: null, LineasIds: null, 
                DatosAdicionales: null, Percepciones: null, Observaciones: null
            );

            _clienteRepositorioMock.Setup(repo => repo.ExisteCodigo(123, null)).ReturnsAsync(true);

            // Act
            var resultado = await _clienteServicio.Crear(dto);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ClienteError.CodigoDuplicado.Codigo, resultado.Error.Codigo);
        }

        [TestMethod]
        public async Task Eliminar_Cliente_Falla_ConComprobantes()
        {
            // Arrange
            var paramBase = new ClienteParametros { Codigo = 1, RazonSocial = "Test", AgendaId = 1, Situacion = SituacionCliente.ActivoNormal };
            var cliente = Cliente.Crear(paramBase).Valor;
            
            _clienteRepositorioMock.Setup(r => r.ObtenerPorId(1)).ReturnsAsync(cliente);
            _clienteRepositorioMock.Setup(r => r.TieneComprobantes(1)).ReturnsAsync(true);

            // Act
            var resultado = await _clienteServicio.Eliminar(1);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ClienteCodigoError.TieneComprobantes, resultado.Error.Codigo);
            _clienteRepositorioMock.Verify(r => r.Eliminar(It.IsAny<Cliente>()), Times.Never);
        }
    }
}
