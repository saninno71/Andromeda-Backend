using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Enums;
using fyd.backend.Dominio.Contabilidad.Errores;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fyd.backend.UnitTests.Dominio.Contabilidad.Entidades
{
    [TestClass]
    public class AsientoTest
    {
        [TestMethod]
        public void Crear_AsientoValido_Exitoso()
        {
            // Arrange & Act
            var resultado = Asiento.Crear(
                comprobanteId: 1,
                cuentaId: 10,
                tipo: (int)AsientoTipo.Debe,
                importe: 1000m,
                impLocal: 1000m,
                impReferencia: 10m,
                detalle: "Test Asiento");

            // Assert
            Assert.IsTrue(resultado.Exitoso);
            Assert.IsNotNull(resultado.Valor);
            Assert.AreEqual(1, resultado.Valor.ComprobanteId);
            Assert.AreEqual(10, resultado.Valor.CuentaId);
            Assert.AreEqual((int)AsientoTipo.Debe, resultado.Valor.Tipo);
            Assert.AreEqual(1000m, resultado.Valor.Importe);
        }

        [TestMethod]
        public void Crear_ComprobanteInvalido_Falla()
        {
            var resultado = Asiento.Crear(
                comprobanteId: -1,
                cuentaId: 10,
                tipo: (int)AsientoTipo.Debe,
                importe: 1000m,
                impLocal: 1000m,
                impReferencia: 10m,
                detalle: "Test Asiento");

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(AsientoCodigoError.ComprobanteRequerido, resultado.Error!.Codigo);
        }

        [TestMethod]
        public void Crear_CuentaInvalida_Falla()
        {
            var resultado = Asiento.Crear(
                comprobanteId: 1,
                cuentaId: 0,
                tipo: (int)AsientoTipo.Debe,
                importe: 1000m,
                impLocal: 1000m,
                impReferencia: 10m,
                detalle: "Test Asiento");

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(AsientoCodigoError.CuentaRequerida, resultado.Error!.Codigo);
        }

        [TestMethod]
        public void Crear_TipoInvalido_Falla()
        {
            var resultado = Asiento.Crear(
                comprobanteId: 1,
                cuentaId: 10,
                tipo: 0,
                importe: 1000m,
                impLocal: 1000m,
                impReferencia: 10m,
                detalle: "Test Asiento");

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(AsientoCodigoError.TipoInvalido, resultado.Error!.Codigo);
        }

        [TestMethod]
        public void Crear_ImporteInvalido_Falla()
        {
            var resultado = Asiento.Crear(
                comprobanteId: 1,
                cuentaId: 10,
                tipo: (int)AsientoTipo.Debe,
                importe: -50m,
                impLocal: -50m,
                impReferencia: -0.5m,
                detalle: "Test Asiento");

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(AsientoCodigoError.ImporteInvalido, resultado.Error!.Codigo);
        }

        [TestMethod]
        public void Actualizar_AsientoValido_Exitoso()
        {
            var asiento = Asiento.Crear(1, 10, (int)AsientoTipo.Debe, 100m, 100m, 1m, "Test").Valor!;

            var resultado = asiento.Actualizar(20, (int)AsientoTipo.Haber, 200m, 200m, 2m, "Actualizado");

            Assert.IsTrue(resultado.Exitoso);
            Assert.AreEqual(20, asiento.CuentaId);
            Assert.AreEqual((int)AsientoTipo.Haber, asiento.Tipo);
            Assert.AreEqual(200m, asiento.Importe);
        }
    }
}
