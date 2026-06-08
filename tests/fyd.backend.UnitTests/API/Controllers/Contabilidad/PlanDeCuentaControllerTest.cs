using fyd.backend.API.Controllers.Contabilidad;
using fyd.backend.Aplicacion.Contabilidad;
using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Enums;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Dominio.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace fyd.backend.UnitTests.API.Controllers.Contabilidad
{
    [TestClass]
    public sealed class PlanDeCuentaControllerTest
    {
        private Mock<IPlanDeCuentaServicio> _servicioMock;
        private Mock<IPlanDeCuentaProcedimientoAlmacenado> _repositorioMock;
        private Mock<IServicioLogueo<PlanDeCuentaController>> _logMock;
        private PlanDeCuentaController _controller;

        [TestInitialize]
        public void Setup()
        {
            _servicioMock = new Mock<IPlanDeCuentaServicio>();
            _repositorioMock = new Mock<IPlanDeCuentaProcedimientoAlmacenado>();
            _logMock = new Mock<IServicioLogueo<PlanDeCuentaController>>();

            _controller = new PlanDeCuentaController(
                _servicioMock.Object, 
                _repositorioMock.Object, 
                _logMock.Object);

            // Importante: Para que CreatedAtAction funcione en tests, hay que mockear el ActionContext o usar un DefaultHttpContext
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [TestMethod]
        public async Task ObtenerPorId_CuandoExiste_RetornaOkConDeTodo()
        {
            // Arrange
            var id = 1;
            var consultarDto = new ConsultarCuentaDto(
                Id: id,
                Codigo: "1.1",
                Nombre: "Caja",
                CuentaMadreId:1,
                AsientoOk: true,
                SubcuentaTipo: (int)SubcuentaTipo.SinSubcuentas,
                AjustaOk: false,
                MonedasTipo: MonedaTipo.Local,
                EmpresaId: null,
                Grupos: new List<GrupoDto>
                {
                    new GrupoDto (Id: 1, Nombre:"Activo"),
                    new GrupoDto ( Id:2, Nombre:"Circulante")
                }
            );
            _servicioMock.Setup(s => s.ObtenerPorId(id)).ReturnsAsync(Resultado<ConsultarCuentaDto>.Exito(consultarDto));

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
            _servicioMock.Setup(s => s.ObtenerPorId(id)).ReturnsAsync(Resultado<ConsultarCuentaDto>.Falla(CuentaContableError.NoEncontrada(id)));

            // Act
            var result = await _controller.ObtenerPorId(id);

            // Assert
            var problemResult = result as ObjectResult;
            Assert.IsNotNull(problemResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, problemResult.StatusCode);
            
            var details = problemResult.Value as ProblemDetails;
            Assert.IsNotNull(details);
            Assert.AreEqual("CuentaContable.NoEncontrada", details.Title);
        }

        [TestMethod]
        public async Task Crear_CuandoEsValido_RetornaCreatedAtAction()
        {
            // Arrange
            var dto = new CrearCuentaDto("1.1", "Nueva", true, SubcuentaTipo.SinSubcuentas, MonedaTipo.Local, false, null, null, null);
            _servicioMock.Setup(s => s.CrearCuenta(dto)).ReturnsAsync(Resultado<int>.Exito(10));

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
            var dtoId2 = new ActualizarCuentaDto(2, "1.1", "Edit", true, SubcuentaTipo.SinSubcuentas, MonedaTipo.Local, false, null, null, null);

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
            var dto = new ActualizarCuentaDto(id, "1.1", "Edit", true, SubcuentaTipo.SinSubcuentas, MonedaTipo.Local, false, null, null, null);
            _servicioMock.Setup(s => s.ActualizarCuenta(dto)).ReturnsAsync(Resultado.Exito());

            // Act
            var result = await _controller.Actualizar(id, dto);

            // Assert
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }
    }
}
