using System.Threading.Tasks;
using fyd.backend.API.Controllers.Compras;
using fyd.backend.Aplicacion.Compras.DTOs;
using fyd.backend.Aplicacion.Compras.Servicios.Interfaces;
using fyd.backend.Dominio.Compras.Errores;
using fyd.backend.Dominio.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace fyd.backend.UnitTests.API.Controllers.Compras
{
    [TestClass]
    public class ProveedorControllerTest
    {
        private Mock<IProveedorServicio> _servicioMock = null!;
        private ProveedorController _controlador = null!;

        [TestInitialize]
        public void Iniciar()
        {
            _servicioMock = new Mock<IProveedorServicio>();
            // El procedimientoAlmacenado no se usa para estos endpoints de ABM, mandamos null
            _controlador = new ProveedorController(_servicioMock.Object, null!);
        }

        [TestMethod]
        public async Task ObtenerPorId_ExisteRegistro_RetornaOkConDto()
        {
            // Arrange
            var dto = new ConsultarProveedorDto(1, 123, 1, 0, 1, 1, 1, 1, null, null, 0, 0, 1, null, null, 1, 100, true, true, null, new int[0], new InfoRetencionDto[0]);
            _servicioMock.Setup(s => s.ObtenerPorId(1)).ReturnsAsync(Resultado<ConsultarProveedorDto>.Exito(dto));

            // Act
            var accion = await _controlador.ObtenerPorId(1);

            // Assert
            var okResult = accion as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(dto, okResult.Value);
        }

        [TestMethod]
        public async Task Crear_ServicioRetornaExito_RetornaCreatedAtActionConId()
        {
            // Arrange
            var dto = new CrearProveedorDto(123, 1, 0, 1, 1, 1, 1, null, null, 0, 0, 1, 100, 1, true, true, null, new int[0], new InfoRetencionDto[0]);
            _servicioMock.Setup(s => s.Crear(dto)).ReturnsAsync(Resultado<int>.Exito(99));

            // Act
            var accion = await _controlador.Crear(dto);

            // Assert
            var createdResult = accion as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(nameof(ProveedorController.ObtenerPorId), createdResult.ActionName);
            Assert.AreEqual(99, createdResult.RouteValues!["id"]);
        }

        [TestMethod]
        public async Task Actualizar_MismoIdUriYDtos_RetornaNoContent()
        {
            // Arrange
            var dto = new ActualizarProveedorDto(10, 124, 1, 0, 1, 1, 1, 1, null, null, 0, 0, 1, null, 1, 100, true, true, null, new int[0], new InfoRetencionDto[0]);
            _servicioMock.Setup(s => s.Actualizar(dto)).ReturnsAsync(Resultado.Exito());

            // Act
            var accion = await _controlador.Actualizar(10, dto);

            // Assert
            var noContentResult = accion as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [TestMethod]
        public async Task Eliminar_IdInexistente_RetornaNotFound()
        {
            // Arrange
            _servicioMock.Setup(s => s.Eliminar(999)).ReturnsAsync(Resultado.Falla(ProveedorError.NoEncontrado(999)));

            // Act
            var accion = await _controlador.Eliminar(999);

            // Assert
            var notFoundResult = accion as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}
