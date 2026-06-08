using fyd.backend.Dominio.Ventas.Entidades;
using fyd.backend.Dominio.Ventas.Enums;
using fyd.backend.Dominio.Ventas.Errores;
using fyd.backend.Dominio.Ventas.Parametros;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace fyd.backend.UnitTests.Dominio.Ventas
{
    [TestClass]
    public class ClienteTest
    {
        private ClienteParametros CrearParametrosValidos()
        {
            return new ClienteParametros
            {
                Codigo = 123,
                RazonSocial = "Cliente Valido",
                AgendaId = 1,
                Situacion = SituacionCliente.ActivoNormal,
                FechaAlta = new DateTime(2023, 1, 1),
                FechaBaja = null,
                PorcDescuento1 = 10,
                PorcIvaLiberado = 21,
                ImpCredito = 50000,
                DomEntregaId = 1
            };
        }

        [TestMethod]
        public void Crear_Exitoso()
        {
            // Arrange
            var param = CrearParametrosValidos();

            // Act
            var resultado = Cliente.Crear(param);

            // Assert
            Assert.IsTrue(resultado.Exitoso);
            Assert.IsNotNull(resultado.Valor);
            Assert.AreEqual(123, resultado.Valor.Codigo);
            Assert.AreEqual(1, resultado.Valor.AgendaId);
        }

        [TestMethod]
        public void Crear_Falla_CuandoCodigoEsCero()
        {
            // Arrange
            var param = CrearParametrosValidos();
            param.Codigo = 0;

            // Act
            var resultado = Cliente.Crear(param);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ClienteError.CodigoInvalido("").Codigo, resultado.Error.Codigo);
        }

        [TestMethod]
        public void Crear_Falla_CuandoRazonSocialEsVacia()
        {
            // Arrange
            var param = CrearParametrosValidos();
            param.RazonSocial = "   "; // Vacío/Blanco

            // Act
            var resultado = Cliente.Crear(param);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ClienteError.RazonSocialVacia.Codigo, resultado.Error.Codigo);
        }

        [TestMethod]
        public void Crear_Falla_CuandoFechaAltaEsPosteriorABaja()
        {
            // Arrange
            var param = CrearParametrosValidos();
            param.FechaAlta = new DateTime(2024, 1, 1);
            param.FechaBaja = new DateTime(2023, 1, 1);

            // Act
            var resultado = Cliente.Crear(param);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ClienteError.FechaAltaPosteriorABaja.Codigo, resultado.Error.Codigo);
        }

        [TestMethod]
        public void Crear_Falla_CuandoDescuentosSonNegativos()
        {
            // Arrange
            var param = CrearParametrosValidos();
            param.PorcDescuento1 = -5;

            // Act
            var resultado = Cliente.Crear(param);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(ClienteError.DescuentoNegativo.Codigo, resultado.Error.Codigo);
        }

        [TestMethod]
        public void Actualizar_Exitoso()
        {
            // Arrange
            var paramBase = CrearParametrosValidos();
            var cliente = Cliente.Crear(paramBase).Valor;

            var paramUpdate = CrearParametrosValidos();
            paramUpdate.Codigo = 456;
            paramUpdate.AgendaId = 2;
            paramUpdate.DomEntregaId = 0; // INV-04 Set DomEntregaId as AgendaId si es 0
            
            // Act
            var resultado = cliente.Actualizar(paramUpdate);

            // Assert
            Assert.IsTrue(resultado.Exitoso);
            Assert.AreEqual(456, cliente.Codigo);
            Assert.AreEqual(2, cliente.DomEntregaId); // INV-04 Set DomEntregaId as AgendaId si es 0
        }
    }
}
