using fyd.backend.API.Controllers.Contabilidad;
using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace fyd.backend.UnitTests.API.Controllers.Contabilidad
{
    [TestClass]
    public class ConsolidacionControllerTest
    {
        private Mock<IConsolidacionServicio> _servicioMock;
        private Mock<IServicioLogueo<ConsolidacionController>> _logMock;
        private ConsolidacionController _controller;

        [TestInitialize]
        public void Setup()
        {
            _servicioMock = new Mock<IConsolidacionServicio>();
            _logMock = new Mock<IServicioLogueo<ConsolidacionController>>();

            _controller = new ConsolidacionController(
                _servicioMock.Object,
                _logMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
        }

        // =====================================================
        // VerificarEstado
        // =====================================================

        [TestMethod]
        public async Task VerificarEstado_Exitoso_RetornaOk()
        {
            var estadoDtos = new List<ModuloEstadoDto>
            {
                new("Ventas", false, true),
                new("Compras", false, true)
            }.AsReadOnly();

            _servicioMock.Setup(s => s.VerificarEstado(It.IsAny<VerificarEstadoDto>()))
                .ReturnsAsync(Resultado<IReadOnlyList<ModuloEstadoDto>>.Exito(estadoDtos));

            var dto = new VerificarEstadoDto(1, 2024, 3);

            var resultado = await _controller.VerificarEstado(dto);

            var ok = resultado as OkObjectResult;
            Assert.IsNotNull(ok);
            Assert.AreEqual(200, ok.StatusCode);
        }

        // =====================================================
        // Generar
        // =====================================================

        [TestMethod]
        public async Task Generar_Exitoso_RetornaNoContent()
        {
            _servicioMock.Setup(s => s.Generar(It.IsAny<GenerarConsolidacionDto>()))
                .ReturnsAsync(Resultado.Exito());

            var dto = new GenerarConsolidacionDto(1, 2024, 3, true, 1, "Det", null,
                new List<string> { "Ventas" });

            var resultado = await _controller.Generar(dto);

            var noContent = resultado as NoContentResult;
            Assert.IsNotNull(noContent);
            Assert.AreEqual(204, noContent.StatusCode);
        }

        [TestMethod]
        public async Task Generar_PeriodoYaGenerado_RetornaConflict()
        {
            _servicioMock.Setup(s => s.Generar(It.IsAny<GenerarConsolidacionDto>()))
                .ReturnsAsync(Resultado.Falla(ConsolidacionError.PeriodoYaGenerado("Ventas")));

            var dto = new GenerarConsolidacionDto(1, 2024, 3, true, 1, "Det", null,
                new List<string> { "Ventas" });

            var resultado = await _controller.Generar(dto);

            var objectResult = resultado as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(409, objectResult.StatusCode);
        }

        // =====================================================
        // Eliminar
        // =====================================================

        [TestMethod]
        public async Task Eliminar_Exitoso_RetornaNoContent()
        {
            _servicioMock.Setup(s => s.Eliminar(It.IsAny<EliminarConsolidacionDto>()))
                .ReturnsAsync(Resultado.Exito());

            var dto = new EliminarConsolidacionDto(1, 2024, 3, new List<string> { "Ventas" });

            var resultado = await _controller.Eliminar(dto);

            var noContent = resultado as NoContentResult;
            Assert.IsNotNull(noContent);
            Assert.AreEqual(204, noContent.StatusCode);
        }

        [TestMethod]
        public async Task Eliminar_PeriodoNoGenerado_RetornaValidacion()
        {
            _servicioMock.Setup(s => s.Eliminar(It.IsAny<EliminarConsolidacionDto>()))
                .ReturnsAsync(Resultado.Falla(ConsolidacionError.PeriodoNoPuedeEliminarse));

            var dto = new EliminarConsolidacionDto(1, 2024, 3, new List<string> { "Ventas" });

            var resultado = await _controller.Eliminar(dto);

            var objectResult = resultado as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(400, objectResult.StatusCode);
        }
    }
}
