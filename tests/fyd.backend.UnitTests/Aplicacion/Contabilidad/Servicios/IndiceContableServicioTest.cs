using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Parametros;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace fyd.backend.UnitTests.Aplicacion.Contabilidad.Servicios
{
    [TestClass]
    public sealed class IndiceContableServicioTest
    {
        private Mock<IIndiceContableRepositorio> _repositorio = null!;
        private Mock<IUnidadDeTrabajo> _unidadDeTrabajo = null!;
        private IndiceContableServicio _servicio = null!;

        [TestInitialize]
        public void Preparar_Tests()
        {
            _repositorio = new Mock<IIndiceContableRepositorio>();
            _unidadDeTrabajo = new Mock<IUnidadDeTrabajo>();
            _servicio = new IndiceContableServicio(_repositorio.Object, _unidadDeTrabajo.Object);
        }

        #region Crear

        [TestMethod]
        public async Task Crear_IndiceContable_Exitosamente()
        {
            var dto = new CrearIndiceDto(new DateTime(2024, 3, 15), 150m);

            _repositorio.Setup(r => r.ExistePeriodo(new DateTime(2024, 3, 1), null)).ReturnsAsync(false);
            _repositorio.Setup(r => r.Agregar(It.IsAny<IndiceContable>())).Callback<IndiceContable>(i => i.Id = 1);
            _unidadDeTrabajo.Setup(r => r.GuardarCambios()).ReturnsAsync(1);

            var resultado = await _servicio.Crear(dto);

            Assert.IsTrue(resultado.Exitoso);
            Assert.AreEqual(1, resultado.Valor);
        }

        [TestMethod]
        public async Task Crear_IndiceContable_Periodo_Duplicado_Falla()
        {
            var dto = new CrearIndiceDto(new DateTime(2024, 3, 1), 150m);

            _repositorio.Setup(r => r.ExistePeriodo(new DateTime(2024, 3, 1), null)).ReturnsAsync(true);

            var resultado = await _servicio.Crear(dto);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(IndiceContableCodigoError.PeriodoDuplicado, resultado.Error!.Codigo);
            _repositorio.Verify(r => r.Agregar(It.IsAny<IndiceContable>()), Times.Never);
            _unidadDeTrabajo.Verify(u => u.GuardarCambios(), Times.Never);
        }

        #endregion

        #region Actualizar

        [TestMethod]
        public async Task Actualizar_IndiceContable_Exitosamente()
        {
            var indiceExistente = IndiceContable.Crear(new ParametrosIndice(new DateTime(2024, 3, 1), 100m)).Valor!;
            indiceExistente.Id = 1;
            var dto = new ActualizarIndiceDto(1, new DateTime(2024, 4, 1), 200m);

            _repositorio.Setup(r => r.ObtenerPorId(1)).ReturnsAsync(indiceExistente);
            _unidadDeTrabajo.Setup(r => r.GuardarCambios()).ReturnsAsync(1);

            var resultado = await _servicio.Actualizar(dto);

            Assert.IsTrue(resultado.Exitoso);
            _unidadDeTrabajo.Verify(u => u.GuardarCambios(), Times.Once);
        }

        [TestMethod]
        public async Task Actualizar_IndiceContable_NoExiste_Falla()
        {
            var dto = new ActualizarIndiceDto(99, new DateTime(2024, 4, 1), 200m);

            _repositorio.Setup(r => r.ObtenerPorId(99)).ReturnsAsync((IndiceContable?)null);

            var resultado = await _servicio.Actualizar(dto);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(IndiceContableError.NoEncontrado(99), resultado.Error!);
            _unidadDeTrabajo.Verify(u => u.GuardarCambios(), Times.Never);
        }

        [TestMethod]
        public async Task Actualizar_IndiceContable_Periodo_Duplicado_Falla()
        {
            var indiceExistente = IndiceContable.Crear(new ParametrosIndice(new DateTime(2024, 3, 1), 100m)).Valor!;
            indiceExistente.Id = 1;
            var dto = new ActualizarIndiceDto(1, new DateTime(2024, 4, 1), 200m);

            _repositorio.Setup(r => r.ObtenerPorId(1)).ReturnsAsync(indiceExistente);
            _repositorio.Setup(r => r.ExistePeriodo(new DateTime(2024, 4, 1), 1)).ReturnsAsync(true);

            var resultado = await _servicio.Actualizar(dto);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(IndiceContableCodigoError.PeriodoDuplicado, resultado.Error!.Codigo);
            _unidadDeTrabajo.Verify(u => u.GuardarCambios(), Times.Never);
        }

        #endregion

        #region Eliminar

        [TestMethod]
        public async Task Eliminar_IndiceContable_Exitosamente()
        {
            var indice = IndiceContable.Crear(new ParametrosIndice(new DateTime(2024, 3, 1), 100m)).Valor!;
            indice.Id = 1;

            _repositorio.Setup(r => r.ObtenerPorId(1)).ReturnsAsync(indice);
            _repositorio.Setup(r => r.Eliminar(It.IsAny<IndiceContable>()));
            _unidadDeTrabajo.Setup(r => r.GuardarCambios()).ReturnsAsync(1);

            var resultado = await _servicio.Eliminar(1);

            Assert.IsTrue(resultado.Exitoso);
            _repositorio.Verify(r => r.Eliminar(It.IsAny<IndiceContable>()), Times.Once);
            _unidadDeTrabajo.Verify(u => u.GuardarCambios(), Times.Once);
        }

        [TestMethod]
        public async Task Eliminar_IndiceContable_NoExiste_Falla()
        {
            _repositorio.Setup(r => r.ObtenerPorId(99)).ReturnsAsync((IndiceContable?)null);

            var resultado = await _servicio.Eliminar(99);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(IndiceContableError.NoEncontrado(99), resultado.Error!);
            _repositorio.Verify(r => r.Eliminar(It.IsAny<IndiceContable>()), Times.Never);
            _unidadDeTrabajo.Verify(u => u.GuardarCambios(), Times.Never);
        }

        #endregion
    }
}
