using fyd.backend.API.Controllers.Contabilidad;
using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
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
    public sealed class IndiceContableControllerTest
    {
        private Mock<IIndiceContableServicio> _servicioMock = null!;
        private Mock<ICstctbIndicesProcedimientoAlmacenado> _repositorioMock = null!;
        private Mock<IServicioLogueo<IndiceContableController>> _logMock = null!;
        private IndiceContableController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _servicioMock = new Mock<IIndiceContableServicio>();
            _repositorioMock = new Mock<ICstctbIndicesProcedimientoAlmacenado>();
            _logMock = new Mock<IServicioLogueo<IndiceContableController>>();

            _controller = new IndiceContableController(
                _servicioMock.Object,
                _repositorioMock.Object,
                _logMock.Object);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [TestMethod]
        public async Task ObtenerPorId_CuandoExiste_RetornaOk()
        {
            var id = 1;
            var dto = new ConsultarIndiceDto(id, new DateTime(2024, 3, 1), 150m);
            _servicioMock.Setup(s => s.ObtenerPorId(id))
                         .ReturnsAsync(Resultado<ConsultarIndiceDto>.Exito(dto));

            var result = await _controller.ObtenerPorId(id);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(dto, okResult.Value);
        }

        [TestMethod]
        public async Task ObtenerPorId_CuandoNoExiste_RetornaNotFound()
        {
            var id = 99;
            _servicioMock.Setup(s => s.ObtenerPorId(id))
                         .ReturnsAsync(Resultado<ConsultarIndiceDto>.Falla(IndiceContableError.NoEncontrado(id)));

            var result = await _controller.ObtenerPorId(id);

            var problemResult = result as ObjectResult;
            Assert.IsNotNull(problemResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, problemResult.StatusCode);

            var details = problemResult.Value as ProblemDetails;
            Assert.IsNotNull(details);
            Assert.AreEqual(IndiceContableCodigoError.NoEncontrado, details.Title);
        }

        [TestMethod]
        public async Task Crear_CuandoEsValido_RetornaCreatedAtAction()
        {
            var dto = new CrearIndiceDto(new DateTime(2024, 3, 1), 150m);
            _servicioMock.Setup(s => s.Crear(dto))
                         .ReturnsAsync(Resultado<int>.Exito(5));

            var result = await _controller.Crear(dto);

            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.AreEqual(5, createdResult.Value);
            Assert.AreEqual("ObtenerPorId", createdResult.ActionName);
        }

        [TestMethod]
        public async Task Actualizar_IdDiferente_RetornaBadRequest()
        {
            var idUrl = 1;
            var dto = new ActualizarIndiceDto(2, new DateTime(2024, 4, 1), 200m);

            var result = await _controller.Actualizar(idUrl, dto);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [TestMethod]
        public async Task Actualizar_CuandoEsValido_RetornaNoContent()
        {
            var id = 1;
            var dto = new ActualizarIndiceDto(id, new DateTime(2024, 4, 1), 200m);
            _servicioMock.Setup(s => s.Actualizar(dto))
                         .ReturnsAsync(Resultado.Exito());

            var result = await _controller.Actualizar(id, dto);

            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [TestMethod]
        public async Task Eliminar_CuandoEsValido_RetornaNoContent()
        {
            var id = 1;
            _servicioMock.Setup(s => s.Eliminar(id))
                         .ReturnsAsync(Resultado.Exito());

            var result = await _controller.Eliminar(id);

            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [TestMethod]
        public async Task Eliminar_CuandoNoExiste_RetornaNotFound()
        {
            var id = 99;
            _servicioMock.Setup(s => s.Eliminar(id))
                         .ReturnsAsync(Resultado.Falla(IndiceContableError.NoEncontrado(id)));

            var result = await _controller.Eliminar(id);

            var problemResult = result as ObjectResult;
            Assert.IsNotNull(problemResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, problemResult.StatusCode);
        }
    }
}
