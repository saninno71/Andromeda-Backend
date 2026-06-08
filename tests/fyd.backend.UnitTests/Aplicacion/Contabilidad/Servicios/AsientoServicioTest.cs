using fyd.backend.Aplicacion.Contabilidad;
using fyd.backend.Aplicacion.Contabilidad.Servicios;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Dominio.Contabilidad.Entidades;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using fyd.backend.Dominio.General.Entidades;
using System.Reflection;
using fyd.backend.Aplicacion.Contabilidad.DTOs;

namespace fyd.backend.UnitTests.Aplicacion.Contabilidad.Servicios
{
    [TestClass]
    public class AsientoServicioTest
    {
        private Mock<IAsientoRepositorio> _repositorio;
        private Mock<IUnidadDeTrabajo> _unidadDeTrabajo;
        private AsientoServicio _servicio;

        [TestInitialize]
        public void Init()
        {
            _repositorio = new Mock<IAsientoRepositorio>();
            _unidadDeTrabajo = new Mock<IUnidadDeTrabajo>();

            _repositorio.Setup(r => r.ExisteCuenta(It.IsAny<int>())).ReturnsAsync(true);
            _repositorio.Setup(r => r.CuentaPermiteAsiento(It.IsAny<int>())).ReturnsAsync(true);
            _repositorio.Setup(r => r.CuentaTieneSubcuentaCaja(It.IsAny<int>())).ReturnsAsync(false);
            _repositorio.Setup(r => r.ObtenerEmpresaIdDeCuenta(It.IsAny<int>())).ReturnsAsync((int?)null);

            var cb = Comprobante.Crear(DateTime.Now, DateTime.Now, 1, 1, 1, 1, 1m, 1m, 100m, "Test", "Memo").Valor!;
            SetId(cb, 1);

            var asientoMock = Asiento.Crear(1, 10, 1, 100m, 100m, 1m, "Detalle").Valor!;
            SetId(asientoMock, 1);

            var prop = typeof(Asiento).GetProperty("Comprobante");
            if (prop != null)
                prop.SetValue(asientoMock, cb, null);

            _repositorio.Setup(r => r.ObtenerPorId(It.IsAny<int>())).ReturnsAsync(asientoMock);

            _servicio = new AsientoServicio(_repositorio.Object, _unidadDeTrabajo.Object);
        }

        private void SetId<T>(T entity, int id) where T : class
        {
            var propertyInfo = typeof(T).GetProperty("Id");
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(entity, id, null);
            }
        }

        [TestMethod]
        public async Task CrearAsiento_CotizacionInvalida_RetornaCotizacionInvalida()
        {
            var dto = new CrearAsientoDto(
                DateTime.Now, DateTime.Now, 1, 100, 1, 1, 0, 1m, "Test", "Obs",
                new List<CrearAsientoLineaDto>());

            var result = await _servicio.CrearAsiento(dto);

            Assert.IsFalse(result.Exitoso);
            Assert.AreEqual(AsientoCodigoError.CotizacionInvalida, result.Error!.Codigo);
        }

        [TestMethod]
        public async Task CrearAsiento_PeriodoCerrado_RetornaFalla()
        {
            _repositorio.Setup(r => r.PeriodoEstaCerrado(It.IsAny<DateTime>())).ReturnsAsync(true);

            var dto = new CrearAsientoDto(
                DateTime.Now, DateTime.Now, 1, 100, 1, 1, 1m, 1m, "Test", "Obs",
                new List<CrearAsientoLineaDto>());

            var result = await _servicio.CrearAsiento(dto);

            Assert.IsFalse(result.Exitoso);
            Assert.AreEqual(AsientoCodigoError.PeriodoCerrado, result.Error!.Codigo);
            _repositorio.Verify(r => r.PeriodoEstaCerrado(It.IsAny<DateTime>()), Times.Once);
        }

        [TestMethod]
        public async Task CrearAsiento_NoBalancea_RetornaFalla()
        {
            var dto = new CrearAsientoDto(
                DateTime.Now, DateTime.Now, 1, 100, 1, 1, 1m, 1m, "Test", "Obs",
                new List<CrearAsientoLineaDto>
                {
                    new CrearAsientoLineaDto(10, 1, 100m, "D", null, null, null),
                    new CrearAsientoLineaDto(20, -1, 50m, "H", null, null, null)
                });

            var result = await _servicio.CrearAsiento(dto);

            Assert.IsFalse(result.Exitoso);
            Assert.AreEqual(AsientoCodigoError.AsientoNoBalancea, result.Error!.Codigo);
        }

        [TestMethod]
        public async Task CrearAsiento_LineaSubcuentaCaja_RetornaFalla()
        {
            _repositorio.Setup(r => r.CuentaTieneSubcuentaCaja(10)).ReturnsAsync(true);

            var dto = new CrearAsientoDto(
                DateTime.Now, DateTime.Now, 1, 100, 1, 1, 1m, 1m, "Test", "Obs",
                new List<CrearAsientoLineaDto>
                {
                    new CrearAsientoLineaDto(10, 1, 100m, "D", null, null, null),
                    new CrearAsientoLineaDto(20, -1, 100m, "H", null, null, null)
                });

            var result = await _servicio.CrearAsiento(dto);

            Assert.IsFalse(result.Exitoso);
            Assert.AreEqual(AsientoCodigoError.SubcuentaCajaInvalida, result.Error!.Codigo);
        }

        [TestMethod]
        public async Task ActualizarAsiento_ComprobanteCerrado_RetornaConflicto()
        {
            _repositorio.Setup(r => r.TieneNumeroAsientoAsignado(It.IsAny<int>())).ReturnsAsync(true);

            // Se cambia el NumeroTipoId a un ID distinto que el configurado en el ctor
            var dto = new ActualizarAsientoDto(
                1, DateTime.Now, DateTime.Now, 999, 100, 1, 1, 1m, 1m, "Test", "Obs",
                new List<ActualizarAsientoLineaDto>());

            var result = await _servicio.ActualizarAsiento(dto);

            Assert.IsFalse(result.Exitoso);
            Assert.AreEqual(AsientoCodigoError.ComprobanteConNumeroAsiento, result.Error!.Codigo);
        }

        [TestMethod]
        public async Task EliminarAsiento_PeriodoRenumerado_RetornaConflicto()
        {
            _repositorio.Setup(r => r.PeriodoEstaRenumerado(It.IsAny<DateTime>())).ReturnsAsync(true);

            var result = await _servicio.EliminarAsiento(1);

            Assert.IsFalse(result.Exitoso);
            Assert.AreEqual(AsientoCodigoError.PeriodoRenumerado, result.Error!.Codigo);
        }
    }
}
