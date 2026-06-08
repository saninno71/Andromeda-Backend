using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Parametros;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fyd.backend.UnitTests.Dominio.Contabilidad
{
    [TestClass]
    public sealed class IndiceContableTest
    {
        [TestMethod]
        public void Crear_IndiceContable_Exitosamente()
        {
            var parametros = new ParametrosIndice(new DateTime(2024, 3, 15), 150.5432m);

            var resultado = IndiceContable.Crear(parametros);

            Assert.IsTrue(resultado.Exitoso);
            Assert.IsNotNull(resultado.Valor);
            Assert.AreEqual(150.5432m, resultado.Valor.Indice);
        }

        [TestMethod]
        public void Crear_IndiceContable_Normaliza_Periodo_Al_PrimerDia_Del_Mes()
        {
            var parametros = new ParametrosIndice(new DateTime(2024, 3, 15), 150m);

            var resultado = IndiceContable.Crear(parametros);

            Assert.IsTrue(resultado.Exitoso);
            Assert.AreEqual(new DateTime(2024, 3, 1), resultado.Valor!.Periodo);
        }

        [TestMethod]
        public void Crear_IndiceContable_Sin_Periodo_Falla()
        {
            var parametros = new ParametrosIndice(default, 150m);

            var resultado = IndiceContable.Crear(parametros);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(IndiceContableError.PeriodoVacio, resultado.Error!);
        }

        [TestMethod]
        public void Crear_IndiceContable_Indice_Cero_Falla()
        {
            var parametros = new ParametrosIndice(new DateTime(2024, 3, 1), 0m);

            var resultado = IndiceContable.Crear(parametros);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(IndiceContableError.IndiceInvalido, resultado.Error!);
        }

        [TestMethod]
        public void Crear_IndiceContable_Indice_Negativo_Falla()
        {
            var parametros = new ParametrosIndice(new DateTime(2024, 3, 1), -10m);

            var resultado = IndiceContable.Crear(parametros);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(IndiceContableError.IndiceInvalido, resultado.Error!);
        }

        [TestMethod]
        public void Actualizar_IndiceContable_Exitosamente()
        {
            var indice = IndiceContable.Crear(new ParametrosIndice(new DateTime(2024, 3, 1), 100m)).Valor!;
            var parametrosActualizar = new ParametrosIndice(new DateTime(2024, 4, 20), 200m);

            var resultado = indice.Actualizar(parametrosActualizar);

            Assert.IsTrue(resultado.Exitoso);
            Assert.AreEqual(new DateTime(2024, 4, 1), indice.Periodo);
            Assert.AreEqual(200m, indice.Indice);
        }

        [TestMethod]
        public void Actualizar_IndiceContable_Sin_Periodo_Falla()
        {
            var indice = IndiceContable.Crear(new ParametrosIndice(new DateTime(2024, 3, 1), 100m)).Valor!;

            var resultado = indice.Actualizar(new ParametrosIndice(default, 200m));

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(IndiceContableError.PeriodoVacio, resultado.Error!);
        }

        [TestMethod]
        public void Actualizar_IndiceContable_Indice_Invalido_Falla()
        {
            var indice = IndiceContable.Crear(new ParametrosIndice(new DateTime(2024, 3, 1), 100m)).Valor!;

            var resultado = indice.Actualizar(new ParametrosIndice(new DateTime(2024, 4, 1), 0m));

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(IndiceContableError.IndiceInvalido, resultado.Error!);
        }
    }
}
