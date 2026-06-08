using fyd.backend.API.Controllers.Ventas;
using fyd.backend.Aplicacion.Ventas.DTOs;
using fyd.backend.Aplicacion.Ventas.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.General;
using fyd.backend.Dominio.Ventas.Errores;
using fyd.backend.Dominio.Ventas.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace fyd.backend.UnitTests.API.Controllers.Ventas
{
    [TestClass]
    public class ClienteControllerTest
    {
        private Mock<IClienteServicio> _servicioMock;
        private Mock<IClienteProcedimientoAlmacenado> _repositorioMock;
        private Mock<IServicioLogueo<ClienteController>> _logueoMock;
        private ClienteController _controller;

        [TestInitialize]
        public void Setup()
        {
            _servicioMock = new Mock<IClienteServicio>();
            _repositorioMock = new Mock<IClienteProcedimientoAlmacenado>();
            _logueoMock = new Mock<IServicioLogueo<ClienteController>>();

            _controller = new ClienteController(
                _servicioMock.Object, 
                _repositorioMock.Object, 
                _logueoMock.Object);
        }

        [TestMethod]
        public async Task ObtenerPorId_Retorna404_SiNoExiste()
        {
            // Arrange
            _servicioMock.Setup(s => s.ObtenerPorId(99))
                .ReturnsAsync(Resultado<ConsultarClienteDto>.Falla(ClienteError.NoEncontrado(99)));

            // Act
            var callResult = await _controller.ObtenerPorId(99);

            // Assert
            // var notFoundResult = callResult as NotFoundResult; //TODO: ver por qué no mapea bien.

            Assert.IsNotNull(callResult);
            Assert.AreEqual(404, (callResult as ObjectResult)!.StatusCode);
        }
    }
}
