using fyd.backend.Aplicacion.Contabilidad;
using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios;
using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Infraestructura.Contabilidad.Mapeos;
using fyd.backend.Infraestructura.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.Infraestructura;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fyd.backend.IntegrationTests
{
    [TestClass]
    public sealed class PlanDeCuentaServicioTest
    {
        [TestMethod]
        public async Task ActualizarCuenta_Con_Grupos_Validos_ActualizaCorrectamente()
        {
            // Arrange
            var opcion = new DbContextOptionsBuilder<ContextoAplicacion>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var contexto = new ContextoAplicacion(opcion);

            // Crear grupos en la DB
            var grupo1 = Grupo.Crear("G1", "Grupo 1").Valor!;
            var grupo2 = Grupo.Crear("G2", "Grupo 2").Valor!;
            var grupo3 = Grupo.Crear("G3", "Grupo 3").Valor!;
            var infraGrupo1 = GrupoMapeo.AInfraestructura(grupo1);
            var infraGrupo2 = GrupoMapeo.AInfraestructura(grupo2);
            var infraGrupo3 = GrupoMapeo.AInfraestructura(grupo3);
            contexto.Grupos.AddRange(infraGrupo1, infraGrupo2, infraGrupo3);

            // Crear cuenta en la DB
            var cuenta = CuentaContable.Crear(new CrearCuentaDto("1.1", "Cuenta Test", true, fyd.backend.Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, fyd.backend.Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, null,null).AParametrosCuenta()).Valor;
            var infraCuenta = CuentaContableMapeo.AInfraestructura(cuenta!);
            contexto.CuentasContables.Add(infraCuenta);
            await contexto.SaveChangesAsync();
            grupo1.Id = infraGrupo1.Id;
            grupo2.Id = infraGrupo2.Id;
            grupo3.Id = infraGrupo3.Id;
            cuenta!.Id = infraCuenta.Id;

            var repositorio = new PlanDeCuentaRepositorio(contexto, null);
            var unidadDeTrabajo = new UnidadDeTrabajo(contexto);
            var servicio = new PlanDeCuentaServicio(repositorio, unidadDeTrabajo);

            // Act
            var actualizarDto = new ActualizarCuentaDto(
                Id: cuenta!.Id,
                Codigo: "1.1",
                Nombre: "Cuenta Test Modificada",
                AsientoOk: true,
                SubcuentaTipo: fyd.backend.Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas,
                MonedaTipo: fyd.backend.Dominio.Contabilidad.Enums.MonedaTipo.Local,
                AjustaOk: false,
                EmpresaId: null,
                CuentaIdMadre: null,
                GruposIds: new List<int> { grupo1.Id, grupo3.Id }
            );

            var resultado = await servicio.ActualizarCuenta(actualizarDto);

            // Assert
            Assert.IsTrue(resultado.Exitoso, "El servicio debería haber devuelto éxito al asignar grupos válidos.");
            
            // Refrescar la entidad directamente del repositorio (se forzará con tracking el Include)
            var cuentaActualizada = await repositorio.ObtenerPorId(cuenta.Id);
            Assert.IsNotNull(cuentaActualizada);
            Assert.AreEqual("Cuenta Test Modificada", cuentaActualizada.Nombre);
            Assert.HasCount(2, cuentaActualizada!.Grupos);
            Assert.IsTrue(cuentaActualizada.Grupos.Any(g => g.Id == grupo1.Id));
            Assert.IsTrue(cuentaActualizada.Grupos.Any(g => g.Id == grupo3.Id));
        }

        [TestMethod]
        public async Task ActualizarCuenta_Con_Grupos_Inexistentes_DevuelveError()
        {
            // Arrange
            var opcion = new DbContextOptionsBuilder<ContextoAplicacion>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var contexto = new ContextoAplicacion(opcion);

            var cuenta = CuentaContable.Crear(new CrearCuentaDto("1.2", "Cuenta 2", true, fyd.backend.Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, fyd.backend.Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, null,null).AParametrosCuenta()).Valor;

            Assert.IsNotNull(cuenta);

            var infraCuenta2 = CuentaContableMapeo.AInfraestructura(cuenta);
            contexto.CuentasContables.Add(infraCuenta2);
            await contexto.SaveChangesAsync();
            cuenta.Id = infraCuenta2.Id;

            var repositorio = new PlanDeCuentaRepositorio(contexto, null);
            var unidadDeTrabajo = new UnidadDeTrabajo(contexto);
            var servicio = new PlanDeCuentaServicio(repositorio, unidadDeTrabajo);

            // Act: Intentar asignar un grupo que no existe (ej. ID 99)
            var actualizarDto = new ActualizarCuentaDto(
                Id: cuenta.Id,
                Codigo: "1.2",
                Nombre: "Cuenta 2 Modificada",
                AsientoOk: true,
                SubcuentaTipo: fyd.backend.Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas,
                MonedaTipo: fyd.backend.Dominio.Contabilidad.Enums.MonedaTipo.Local,
                AjustaOk: false,
                EmpresaId: null,
                CuentaIdMadre: null,
                GruposIds: new List<int> { 99 }
            );

            var resultado = await servicio.ActualizarCuenta(actualizarDto);

            // Assert
            Assert.IsFalse(resultado.Exitoso, "El servicio debería fallar al solicitar grupos no existentes en base de datos.");
            Assert.AreEqual("CuentaContable.GrupoNoEncontrado", resultado.Error?.Codigo);
        }
    }
}
