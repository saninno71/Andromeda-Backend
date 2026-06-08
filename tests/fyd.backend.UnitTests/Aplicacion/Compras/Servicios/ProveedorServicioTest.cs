using System.Threading.Tasks;
using fyd.backend.Aplicacion.Compras.DTOs;
using fyd.backend.Aplicacion.Compras.Servicios;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Compras.Entidades;
using fyd.backend.Dominio.Compras.Errores;
using fyd.backend.Dominio.Compras.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace fyd.backend.UnitTests.Aplicacion.Compras.Servicios
{
    [TestClass]
    public class ProveedorServicioTest
    {
        private Mock<IProveedorRepositorio> _proveedorRepositorioMock = null!;
        private Mock<IUnidadDeTrabajo> _unidadDeTrabajoMock = null!;
        private ProveedorServicio _servicio = null!;

        [TestInitialize]
        public void Iniciar()
        {
            _proveedorRepositorioMock = new Mock<IProveedorRepositorio>();
            _unidadDeTrabajoMock = new Mock<IUnidadDeTrabajo>();

            _servicio = new ProveedorServicio(
                _proveedorRepositorioMock.Object,
                _unidadDeTrabajoMock.Object);
        }

        [TestMethod]
        public async Task Crear_CodigoExistente_RetornaFalla()
        {
            // Arrange
            var dto = new CrearProveedorDto(123, 1, 0, 1, 1, 1, 1, null, null, 0, 0, 1, 100, 1, true, true, null, new int[0], new InfoRetencionDto[0]);
            _proveedorRepositorioMock.Setup(repo => repo.ExisteCodigo(123)).ReturnsAsync(true);

            // Act
            var resultado = await _servicio.Crear(dto);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ProveedorError.CodigoDuplicado.Codigo, resultado.Error!.Codigo);
            _unidadDeTrabajoMock.Verify(uow => uow.GuardarCambios(), Times.Never);
        }

        [TestMethod]
        public async Task Crear_ValoresCorrectos_InvocaUnidadDeTrabajo_RetornaExito()
        {
            // Arrange
            var dto = new CrearProveedorDto(123, 1, 0, 1, 1, 1, 1, null, null, 0, 0, 1, 100, 1, true, true, null, new int[0], new InfoRetencionDto[0]);
            _proveedorRepositorioMock.Setup(repo => repo.ExisteCodigo(123)).ReturnsAsync(false);
            
            // Act
            var resultado = await _servicio.Crear(dto);

            // Assert
            Assert.IsTrue(resultado.Exitoso);
            _proveedorRepositorioMock.Verify(repo => repo.Agregar(It.IsAny<Proveedor>()), Times.Once);
            _unidadDeTrabajoMock.Verify(uow => uow.GuardarCambios(), Times.Once);
        }

        [TestMethod]
        public async Task Actualizar_CircularidadCc_RetornaFalla()
        {
            // Arrange
            var dto = new ActualizarProveedorDto(10, 123, 1, 10, 1, 1, 1, 1, null, null, 0, 0, 1, null, 1, 100, true, true, null, new int[0], new InfoRetencionDto[0]);
            var param = new fyd.backend.Dominio.Compras.Parametros.ProveedorParametros { AgendaId = 1 };
            var proveedor = Proveedor.Crear(param).Valor!;
            proveedor.Id = 10; // Id = 10, ProveedorCcId = 10 en Dto!

            _proveedorRepositorioMock.Setup(repo => repo.ObtenerPorId(10)).ReturnsAsync(proveedor);

            // Act
            var resultado = await _servicio.Actualizar(dto);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ProveedorError.CircularidadCc.Codigo, resultado.Error!.Codigo);
            _unidadDeTrabajoMock.Verify(uow => uow.GuardarCambios(), Times.Never);
        }

        [TestMethod]
        public async Task Eliminar_ProveedorEnUso_RetornaFalla_NoLlamaEliminar()
        {
            // Arrange
            var param = new fyd.backend.Dominio.Compras.Parametros.ProveedorParametros { AgendaId = 1 };
            var proveedor = Proveedor.Crear(param).Valor!;
            proveedor.Id = 1;
            
            _proveedorRepositorioMock.Setup(repo => repo.ObtenerPorId(1)).ReturnsAsync(proveedor);
            _proveedorRepositorioMock.Setup(repo => repo.EnUso(1)).ReturnsAsync(true);

            // Act
            var resultado = await _servicio.Eliminar(1);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ProveedorError.EnUso.Codigo, resultado.Error!.Codigo);
            _proveedorRepositorioMock.Verify(repo => repo.Eliminar(It.IsAny<Proveedor>()), Times.Never);
            _unidadDeTrabajoMock.Verify(uow => uow.GuardarCambios(), Times.Never);
        }
    }
}
