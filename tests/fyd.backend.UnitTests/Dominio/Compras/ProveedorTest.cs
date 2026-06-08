using System;
using fyd.backend.Dominio.Compras.Entidades;
using fyd.backend.Dominio.Compras.Errores;
using fyd.backend.Dominio.Compras.Parametros;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fyd.backend.UnitTests.Dominio.Compras
{
    [TestClass]
    public class ProveedorTest
    {
        private ProveedorParametros CrearParametrosBase()
        {
            return new ProveedorParametros
            {
                Codigo = 123,
                AgendaId = 1,
                ProveedorCcId = 0,
                IvaCategoriaId = 1,
                CondicionId = 1,
                CategoriaId = 1,
                CalificacionId = 1,
                CuentaId = 1,
                MonedaId = 1,
                PorcDescuento1 = 10,
                PorcDescuento2 = 5,
                FechaAlta = DateTime.UtcNow,
                FechaBaja = null
            };
        }

        [TestMethod]
        public void Crear_ParametrosValidos_RetornaExito()
        {
            // Arrange
            var parametros = CrearParametrosBase();

            // Act
            var resultado = Proveedor.Crear(parametros);

            // Assert
            Assert.IsTrue(resultado.Exitoso);
            Assert.IsNotNull(resultado.Valor);
            Assert.AreEqual(123, resultado.Valor.Codigo);
            Assert.AreEqual(1, resultado.Valor.AgendaId);
            Assert.AreEqual(10, resultado.Valor.PorcDescuento1);
        }

        [TestMethod]
        public void Crear_DescuentosExcedidos_RetornaFalla()
        {
            // Arrange
            var parametros = CrearParametrosBase();
            parametros.PorcDescuento1 = 150; // Invalido > 100

            // Act
            var resultado = Proveedor.Crear(parametros);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ProveedorError.PorcentajeDescuentoInvalido.Codigo, resultado.Error!.Codigo);
        }

        [TestMethod]
        public void Crear_FechaBajaAnteriorAAlta_RetornaFalla()
        {
            // Arrange
            var parametros = CrearParametrosBase();
            parametros.FechaAlta = new DateTime(2023, 1, 10);
            parametros.FechaBaja = new DateTime(2023, 1, 1);

            // Act
            var resultado = Proveedor.Crear(parametros);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ProveedorError.FechasInvalidas.Codigo, resultado.Error!.Codigo);
        }

        [TestMethod]
        public void Actualizar_ModificandoValoresPermitidos_RetornaExito()
        {
            // Arrange
            var parametros = CrearParametrosBase();
            var proveedor = Proveedor.Crear(parametros).Valor!;

            var parametrosActualizacion = CrearParametrosBase();
            parametrosActualizacion.Codigo = 999;
            parametrosActualizacion.PorcDescuento1 = 50;

            // Act
            var resultado = proveedor.Actualizar(parametrosActualizacion);

            // Assert
            Assert.IsTrue(resultado.Exitoso);
            Assert.AreEqual(999, proveedor.Codigo);
            Assert.AreEqual(50, proveedor.PorcDescuento1);
        }
    }
}
