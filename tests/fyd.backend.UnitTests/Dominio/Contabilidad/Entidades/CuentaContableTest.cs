using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Errores;

namespace fyd.backend.UnitTests.Dominio.Contabilidad.Entidades
{
    [TestClass]
    public sealed class CuentaContableTest
    {
        [TestMethod]
        public void Crear_CuentaContable_Exitosamente()
        {

            var resultado = CuentaContable.Crear(new CrearCuentaDto("1","Cuenta de prueba",true,backend.Dominio.Contabilidad.Enums.SubcuentaTipo.Clientes,backend.Dominio.Contabilidad.Enums.MonedaTipo.Local,false,1,1,null).AParametrosCuenta());

            Assert.IsTrue(resultado.Exitoso);
            Assert.IsNotNull(resultado.Valor);

        }

        [TestMethod]
        public void Crear_CuentaContable_Sin_Codigo_Falla()
        {
            var resultado = CuentaContable.Crear(new CrearCuentaDto("","Cuenta De Prueba",true,backend.Dominio.Contabilidad.Enums.SubcuentaTipo.Clientes,backend.Dominio.Contabilidad.Enums.MonedaTipo.Local,false,null,null,null).AParametrosCuenta());

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(CuentaContableError.CodigoVacio, resultado.Error!);
        }

        [TestMethod]
        public void Crear_CuentaContable_Sin_Nombre_Falla()
        {

            var resultado = CuentaContable.Crear(new CrearCuentaDto("1","",true,backend.Dominio.Contabilidad.Enums.SubcuentaTipo.Clientes,backend.Dominio.Contabilidad.Enums.MonedaTipo.Local,false,null,null,null).AParametrosCuenta());
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(CuentaContableError.NombreVacio, resultado.Error!);
        }

        [TestMethod]
        //TODO: No sé si este test aplica realmente. Quizás queremos devolver un error en lugar de forzar convertir tipos.
        public void Crear_CuentaContable_No_AsientoOK_Queda_Sin_Subcuentas_Ni_EmpresaId()
        {

            var resultado = CuentaContable.Crear(new CrearCuentaDto("1","Cuenta De Prueba",false,backend.Dominio.Contabilidad.Enums.SubcuentaTipo.Clientes,backend.Dominio.Contabilidad.Enums.MonedaTipo.Local,false,1,null,null).AParametrosCuenta());

            Assert.AreEqual(backend.Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, resultado.Valor!.SubcuentaTipo);
            Assert.IsNull(resultado.Valor!.EmpresaId);
        }
    }
}
