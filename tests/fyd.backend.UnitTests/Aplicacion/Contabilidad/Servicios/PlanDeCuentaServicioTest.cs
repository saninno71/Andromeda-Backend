using fyd.backend.Aplicacion.Contabilidad;
using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using Moq;

namespace fyd.backend.UnitTests.Aplicacion.Contabilidad.Servicios
{
    [TestClass]
    public sealed class PlanDeCuentaServicioTest
    {
        private Mock<IPlanDeCuentaRepositorio> _repositorio;
        private Mock<IUnidadDeTrabajo> _unidadDeTrabajo;
        private PlanDeCuentaServicio _servicio;

        [TestInitialize]
        public void Preparar_Tests()
        {
            _repositorio = new Mock<IPlanDeCuentaRepositorio>();
            _unidadDeTrabajo = new Mock<IUnidadDeTrabajo>();

            _servicio = new PlanDeCuentaServicio(_repositorio.Object, _unidadDeTrabajo.Object);
        }

        #region Crear cuenta contable

        [TestMethod]
        public async Task Crear_CuentaContable_Exitosamente()
        {
            var cuentaDto = new CrearCuentaDto(
                Codigo: "1",
                Nombre: "Cuenta De Prueba",
                AsientoOk: false,
                SubcuentaTipo: backend.Dominio.Contabilidad.Enums.SubcuentaTipo.Clientes,
                MonedaTipo: backend.Dominio.Contabilidad.Enums.MonedaTipo.Local,
                AjustaOk: false,
                EmpresaId: null,
                CuentaIdMadre: null,
                GruposIds: null
            );

            _repositorio.Setup(r => r.ExisteCodigo(cuentaDto.Codigo)).ReturnsAsync(false);

            _repositorio.Setup(r => r.Agregar(It.IsAny<CuentaContable>())).Callback<CuentaContable>(c => c.Id = 1);
            _unidadDeTrabajo.Setup(r => r.GuardarCambios()).ReturnsAsync(1);

            var resultado = await _servicio.CrearCuenta(cuentaDto);

            Assert.AreNotEqual(0, resultado.Valor);
            Assert.AreEqual(1, resultado.Valor);
        }

        [TestMethod]
        public async Task Crear_CuentaContable_Falla_Codigo_Existente()
        {
            var cuentaDto = new CrearCuentaDto(
                Codigo: "1",
                Nombre: "Cuenta De Prueba",
                AsientoOk: false,
                SubcuentaTipo: backend.Dominio.Contabilidad.Enums.SubcuentaTipo.Clientes,
                MonedaTipo: backend.Dominio.Contabilidad.Enums.MonedaTipo.Local,
                AjustaOk: false,
                EmpresaId: null,
                CuentaIdMadre: null,
                GruposIds: null
            );

            _repositorio.Setup(r => r.ExisteCodigo(cuentaDto.Codigo)).ReturnsAsync(true);

            var resultado = await _servicio.CrearCuenta(cuentaDto);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(CuentaContableError.CodigoDuplicado("Cuenta De Prueba").Codigo, resultado.Error!.Codigo);

            _repositorio.Verify(r => r.Agregar(It.IsAny<CuentaContable>()), Times.Never);
            _unidadDeTrabajo.Verify(u => u.GuardarCambios(), Times.Never);
        }

        [TestMethod]
        public async Task Crear_CuentaContable_Falla_Supera_Nivel_Maximo_Permitido()
        {
            var cuentaDto = new CrearCuentaDto(
                Codigo: "1",
                Nombre: "Cuenta De Prueba",
                AsientoOk: false,
                SubcuentaTipo: backend.Dominio.Contabilidad.Enums.SubcuentaTipo.Clientes,
                MonedaTipo: backend.Dominio.Contabilidad.Enums.MonedaTipo.Local,
                AjustaOk: false,
                EmpresaId: null,
                CuentaIdMadre: 6,
                GruposIds: null
            );
            _repositorio.Setup(r => r.SuperaMaximoNivelPermitido(6)).ReturnsAsync(true);

            var resultado = await _servicio.CrearCuenta(cuentaDto);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(CuentaContableError.SuperaNivelMaximoPermitido, resultado.Error!);

            _repositorio.Verify(r => r.Agregar(It.IsAny<CuentaContable>()), Times.Never);
            _unidadDeTrabajo.Verify(u => u.GuardarCambios(), Times.Never);
        }

        #endregion

        #region Borrar cuenta contable
        [TestMethod]
        public async Task Borrar_CuentaContable_Exitosamente()
        {

            var resultado = CuentaContable.Crear(new CrearCuentaDto("1","Cuenta de prueba",true,backend.Dominio.Contabilidad.Enums.SubcuentaTipo.Clientes,backend.Dominio.Contabilidad.Enums.MonedaTipo.Local,false,1,1,null).AParametrosCuenta());

            resultado.Valor!.Id = 1;

            _repositorio.Setup(r => r.ObtenerPorId(resultado.Valor.Id)).ReturnsAsync(resultado.Valor);
            _repositorio.Setup(r => r.TieneMovimientos(resultado.Valor.Id)).ReturnsAsync(false);
            _repositorio.Setup(r => r.Eliminar(It.IsAny<CuentaContable>()));
            _unidadDeTrabajo.Setup(r => r.GuardarCambios()).ReturnsAsync(1);

            await _servicio.EliminarCuenta(resultado.Valor.Id);

            _repositorio.Verify(r => r.Eliminar(It.IsAny<CuentaContable>()), Times.Once);
            _unidadDeTrabajo.Verify(u => u.GuardarCambios(), Times.Once);
        }

        [TestMethod]
        public async Task Borrar_CuentaContable_Tiene_Movimientos_Falla()
        {

            var resultado = CuentaContable.Crear(new CrearCuentaDto("1", "Cuenta de prueba", true, backend.Dominio.Contabilidad.Enums.SubcuentaTipo.Clientes, backend.Dominio.Contabilidad.Enums.MonedaTipo.Local, false, 1, 1, null).AParametrosCuenta());

            resultado.Valor!.Id = 1;

            _repositorio.Setup(r => r.ObtenerPorId(resultado.Valor.Id)).ReturnsAsync(resultado.Valor);
            _repositorio.Setup(r => r.TieneMovimientos(resultado.Valor.Id)).ReturnsAsync(true);
            _repositorio.Setup(r => r.Eliminar(It.IsAny<CuentaContable>()));
            _unidadDeTrabajo.Setup(r => r.GuardarCambios()).ReturnsAsync(1);

            var resultadoEliminar = await _servicio.EliminarCuenta(resultado.Valor.Id);

            Assert.IsFalse(resultadoEliminar.Exitoso);
            Assert.AreEqual(CuentaContableError.TieneMovimientos, resultadoEliminar.Error!);
        }

        [TestMethod]
        public async Task Borrar_CuentaContable_Tiene_Cuentas_Hijas_Falla()
        {

            var resultado = CuentaContable.Crear(new CrearCuentaDto("1","Cuenta de prueba Madre",false,backend.Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas,backend.Dominio.Contabilidad.Enums.MonedaTipo.Local,false,null,null,null).AParametrosCuenta());
            resultado.Valor!.Id = 1;

            _repositorio.Setup(r => r.ObtenerPorId(resultado.Valor.Id)).ReturnsAsync(resultado.Valor);
            _repositorio.Setup(r => r.TieneMovimientos(resultado.Valor.Id)).ReturnsAsync(false);
            _repositorio.Setup(r => r.TieneCuentasHijas(resultado.Valor.Id)).ReturnsAsync(true);
            _repositorio.Setup(r => r.Eliminar(It.IsAny<CuentaContable>()));
            _unidadDeTrabajo.Setup(r => r.GuardarCambios()).ReturnsAsync(1);

            var resultadoEliminar = await _servicio.EliminarCuenta(resultado.Valor.Id);

            Assert.IsFalse(resultadoEliminar.Exitoso);
            Assert.AreEqual(CuentaContableError.TieneCuentasHijas, resultadoEliminar.Error!);
        }

        [TestMethod]
        public async Task Borrar_CuentaContable_No_Existe_Mas_Falla()
        {
            var cuentaId = 1;

            _repositorio.Setup(r => r.ObtenerPorId(cuentaId)).ReturnsAsync((CuentaContable?)null);

            var resultado = await _servicio.EliminarCuenta(cuentaId);

            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual(CuentaContableError.NoEncontrada(cuentaId), resultado.Error!);
        }

        #endregion
    }
}
