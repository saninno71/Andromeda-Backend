using fyd.backend.API.Controllers.Contabilidad;
using fyd.backend.Aplicacion.Contabilidad;
using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Dominio.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace fyd.backend.UnitTests.API.Controllers.Contabilidad
{
    [TestClass]
    public sealed class AsientoControllerTest
    {
        private Mock<IAsientoServicio> _servicioMock;
        private Mock<IAsientoProcedimientoAlmacenado> _repositorioSpMock;
        private Mock<IServicioLogueo<AsientoController>> _logMock;
        private AsientoController _controller;

        [TestInitialize]
        public void Setup()
        {
            _servicioMock = new Mock<IAsientoServicio>();
            _repositorioSpMock = new Mock<IAsientoProcedimientoAlmacenado>();
            _logMock = new Mock<IServicioLogueo<AsientoController>>();

            _controller = new AsientoController(
                _servicioMock.Object, 
                _repositorioSpMock.Object, 
                _logMock.Object);

            // Importante: Para que CreatedAtAction funcione en tests
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [TestMethod]
        public async Task ObtenerPorId_CuandoExiste_RetornaOk()
        {
            // Arrange
            var id = 1;
            var consultarDto = new ConsultarAsientoDto(
                Id: id,
                Fecha: DateTime.Now,
                FechaEmision: DateTime.Now,
                NumeraTipoId: 1,
                NumeraTipoSimbolo: "FA",
                Letra: "A",
                PuntoVenta: 1,
                Numero: 100,
                EmpresaId: 1,
                EmpresaNombre: "Empresa",
                MonedaId: 1,
                MonedaNombre: "ARS",
                CotizacionLocal: 1,
                CotizacionReferencia: 1,
                ImpTotal: 100,
                Detalle: "Test",
                Observaciones: "Obs",
                Lineas: new List<ConsultarAsientoLineaDto>()
            );

            _servicioMock.Setup(s => s.ObtenerPorId(id)).ReturnsAsync(Resultado<ConsultarAsientoDto>.Exito(consultarDto));

            // Act
            var result = await _controller.ObtenerPorId(id);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(consultarDto, okResult.Value);
        }

        [TestMethod]
        public async Task ObtenerPorId_CuandoNoExiste_RetornaNotFound()
        {
            // Arrange
            var id = 99;
            _servicioMock.Setup(s => s.ObtenerPorId(id)).ReturnsAsync(Resultado<ConsultarAsientoDto>.Falla(AsientoError.NoEncontrado(id)));

            // Act
            var result = await _controller.ObtenerPorId(id);

            // Assert
            var problemResult = result as ObjectResult;
            Assert.IsNotNull(problemResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, problemResult.StatusCode);
            
            var details = problemResult.Value as ProblemDetails;
            Assert.IsNotNull(details);
            Assert.AreEqual("Asiento.NoEncontrado", details.Title);
        }

        [TestMethod]
        public async Task Crear_CuandoEsValido_RetornaCreatedAtAction()
        {
            // Arrange
            var dto = new CrearAsientoDto(
                DateTime.Now, DateTime.Now, 1, 100, 1, 1, 1, 1, "Test", "Obs",
                new List<CrearAsientoLineaDto>()
            );
            _servicioMock.Setup(s => s.CrearAsiento(dto)).ReturnsAsync(Resultado<int>.Exito(10));

            // Act
            var result = await _controller.Crear(dto);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.AreEqual(10, createdResult.Value);
            Assert.AreEqual("ObtenerPorId", createdResult.ActionName);
        }

        [TestMethod]
        public async Task Actualizar_IdDiferente_RetornaBadRequest()
        {
            // Arrange
            var idUrl = 1;
            var dtoId2 = new ActualizarAsientoDto(
                2, DateTime.Now, DateTime.Now, 1, 100, 1, 1, 1, 1, "Test", "Obs",
                new List<ActualizarAsientoLineaDto>()
            );

            // Act
            var result = await _controller.Actualizar(idUrl, dtoId2);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [TestMethod]
        public async Task Actualizar_CuandoEsValido_RetornaNoContent()
        {
            // Arrange
            var id = 1;
            var dto = new ActualizarAsientoDto(
                id, DateTime.Now, DateTime.Now, 1, 100, 1, 1, 1, 1, "Test", "Obs",
                new List<ActualizarAsientoLineaDto>()
            );
            _servicioMock.Setup(s => s.ActualizarAsiento(dto)).ReturnsAsync(Resultado.Exito());

            // Act
            var result = await _controller.Actualizar(id, dto);

            // Assert
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [TestMethod]
        public async Task Eliminar_CuandoExiste_RetornaNoContent()
        {
            // Arrange
            var id = 1;
            _servicioMock.Setup(s => s.EliminarAsiento(id)).ReturnsAsync(Resultado.Exito());

            // Act
            var result = await _controller.Eliminar(id);

            // Assert
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }
    }
}
