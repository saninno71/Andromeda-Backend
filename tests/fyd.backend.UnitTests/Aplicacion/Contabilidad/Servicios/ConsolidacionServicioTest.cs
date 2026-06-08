using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Consultas;
using fyd.backend.Dominio.Contabilidad.Enums;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Dominio.General;
using fyd.backend.Dominio.General.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace fyd.backend.UnitTests.Aplicacion.Contabilidad.Servicios
{
    [TestClass]
    public class ConsolidacionServicioTest
    {
        private Mock<IConsolidacionRepositorio> _repositorio;
        private Mock<IBloqueoRepositorio> _bloqueoRepositorio;
        private Mock<IUnidadDeTrabajo> _unidadDeTrabajo;
        private ConsolidacionServicio _servicio;

        private const int EmpresaId = 1;
        private const int Anio = 2024;
        private const int Mes = 3;

        [TestInitialize]
        public void Init()
        {
            _repositorio = new Mock<IConsolidacionRepositorio>();
            _bloqueoRepositorio = new Mock<IBloqueoRepositorio>();
            _unidadDeTrabajo = new Mock<IUnidadDeTrabajo>();

            _repositorio.Setup(r => r.ModuloYaGenerado(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(false);
            _repositorio.Setup(r => r.TieneComprobantes(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(true);
            _repositorio.Setup(r => r.ValidarAsientosCompletos(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(true);
            _repositorio.Setup(r => r.GuardarAsientoDeConsolidacion(It.IsAny<AsientoParaGuardar>()))
                .ReturnsAsync(42);
            _repositorio.Setup(r => r.ObtenerImputaciones(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(CrearImputacionesMock());

            _bloqueoRepositorio.Setup(b => b.BloqueoExiste(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            _unidadDeTrabajo.Setup(u => u.GuardarCambios()).ReturnsAsync(1);

            _servicio = new ConsolidacionServicio(
                _repositorio.Object,
                _bloqueoRepositorio.Object,
                _unidadDeTrabajo.Object);
        }

        // =====================================================
        // Tests de VerificarEstado
        // =====================================================

        [TestMethod]
        public async Task VerificarEstado_PeriodoValido_RetornaEstadoPorModulo()
        {
            var dto = new VerificarEstadoDto(EmpresaId, Anio, Mes);

            var resultado = await _servicio.VerificarEstado(dto);

            Assert.IsTrue(resultado.Exitoso);
            Assert.IsNotNull(resultado.Valor);
            Assert.AreEqual(5, resultado.Valor!.Count); // 5 módulos
        }

        // =====================================================
        // Tests de Generar
        // =====================================================

        [TestMethod]
        public async Task Generar_ModuloNoValido_RetornaError()
        {
            var dto = new GenerarConsolidacionDto(EmpresaId, Anio, Mes, true, 1, "Det", null,
                new List<string> { "ModuloInexistente" });

            var resultado = await _servicio.Generar(dto);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ConsolidacionError.ModuloNoValido.Codigo, resultado.Error!.Codigo);
        }

        [TestMethod]
        public async Task Generar_PeriodoYaGenerado_RetornaError()
        {
            _repositorio.Setup(r => r.ModuloYaGenerado(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(true);

            var dto = new GenerarConsolidacionDto(EmpresaId, Anio, Mes, true, 1, "Det", null,
                new List<string> { "Ventas" });

            var resultado = await _servicio.Generar(dto);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual("Consolidacion.PeriodoYaGenerado", resultado.Error!.Codigo);
        }

        [TestMethod]
        public async Task Generar_SinComprobantes_RetornaError()
        {
            _repositorio.Setup(r => r.TieneComprobantes(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(false);

            var dto = new GenerarConsolidacionDto(EmpresaId, Anio, Mes, true, 1, "Det", null,
                new List<string> { "Compras" });

            var resultado = await _servicio.Generar(dto);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual("Consolidacion.SinComprobantes", resultado.Error!.Codigo);
        }

        [TestMethod]
        public async Task Generar_BloqueoOcupado_RetornaError()
        {
            _bloqueoRepositorio.Setup(b => b.BloqueoExiste(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            var dto = new GenerarConsolidacionDto(EmpresaId, Anio, Mes, true, 1, "Det", null,
                new List<string> { "Ventas" });

            var resultado = await _servicio.Generar(dto);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ConsolidacionError.BloqueoOcupado.Codigo, resultado.Error!.Codigo);
        }

        [TestMethod]
        public async Task Generar_PorComprobante_Exitoso_GuardaYLiberaBloqueo()
        {
            var dto = new GenerarConsolidacionDto(EmpresaId, Anio, Mes, true, 1, "Det", null,
                new List<string> { "Ventas" });

            var resultado = await _servicio.Generar(dto);

            Assert.IsTrue(resultado.Exitoso);
            _repositorio.Verify(r => r.GuardarAsientoDeConsolidacion(It.IsAny<AsientoParaGuardar>()), Times.Once);
            _bloqueoRepositorio.Verify(b => b.EliminarBloqueo(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task Generar_ValidacionFallida_DeslincaYRetornaError()
        {
            _repositorio.Setup(r => r.ValidarAsientosCompletos(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(false);

            var dto = new GenerarConsolidacionDto(EmpresaId, Anio, Mes, true, 1, "Det", null,
                new List<string> { "Ventas" });

            var resultado = await _servicio.Generar(dto);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual("Consolidacion.ValidacionAsientosFallida", resultado.Error!.Codigo);
            _repositorio.Verify(r => r.DesvincularComprobantes(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        // =====================================================
        // Tests de Eliminar
        // =====================================================

        [TestMethod]
        public async Task Eliminar_PeriodoNoGenerado_RetornaError()
        {
            // ModuloYaGenerado = false → no se puede eliminar lo que no existe
            var dto = new EliminarConsolidacionDto(EmpresaId, Anio, Mes, new List<string> { "Ventas" });

            var resultado = await _servicio.Eliminar(dto);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ConsolidacionError.PeriodoNoPuedeEliminarse.Codigo, resultado.Error!.Codigo);
        }

        [TestMethod]
        public async Task Eliminar_PeriodoGenerado_Exitoso_DesvinculaYElimina()
        {
            _repositorio.Setup(r => r.ModuloYaGenerado(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(true);
            _repositorio.Setup(r => r.ObtenerAsientosParaEliminar(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new List<int> { 10, 11 }.AsReadOnly());

            var dto = new EliminarConsolidacionDto(EmpresaId, Anio, Mes, new List<string> { "Ventas" });

            var resultado = await _servicio.Eliminar(dto);

            Assert.IsTrue(resultado.Exitoso);
            _repositorio.Verify(r => r.DesvincularComprobantes(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>()), Times.Once);
            _repositorio.Verify(r => r.EliminarAsientoPorComprobanteId(It.IsAny<int>()), Times.Exactly(2));
            _bloqueoRepositorio.Verify(b => b.EliminarBloqueo(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        // =====================================================
        // Helpers privados
        // =====================================================

        private static IReadOnlyList<ImputacionConsolidacionData> CrearImputacionesMock() =>
            new List<ImputacionConsolidacionData>
            {
                new(
                    ComprobanteId: 100,
                    Fecha: new DateTime(Anio, Mes, 15),
                    MonedaId: 1,
                    CotizacionLocal: 1m,
                    CotizacionReferencia: 1m,
                    ImpTotal: 1000m,
                    CuentaId: 1,
                    Importe: 1000m,
                    ImporteLocal: 1000m,
                    ImporteReferencia: 1000m,
                    DetalleComprobante: "Factura A-0001",
                    DetalleImputacion: null,
                    NumeraTipoSimbolo: "FA",
                    NumeraTipoTipo: 1,
                    PuntoVenta: 1,
                    Numero: 1,
                    AgendaNombre: "Cliente SA",
                    SubcuentaTipoCuenta: 0
                )
            }.AsReadOnly();
    }
}
