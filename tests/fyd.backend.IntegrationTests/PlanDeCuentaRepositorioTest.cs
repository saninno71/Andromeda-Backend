using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Infraestructura.Contabilidad.Mapeos;
using fyd.backend.Infraestructura.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.IntegrationTests
{
    [TestClass]
    public sealed class PlanDeCuentaRepositorioTest
    {
        [TestMethod]
        public async Task Crear_CuentaContable_Falla_Supera_Nivel_Maximo_Permitido()
        {
            var opcion = new DbContextOptionsBuilder<ContextoAplicacion>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            await using var contexto = new ContextoAplicacion(opcion);

            var cta1 = CuentaContable.Crear(new CrearCuentaDto("1", "cta1", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, null,null).AParametrosCuenta()).Valor!;
            cta1.Id = 1;
            var cta2 = CuentaContable.Crear(new CrearCuentaDto("2", "cta2", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, 1,null).AParametrosCuenta()).Valor!;
            cta2.Id = 2;
            var cta3 = CuentaContable.Crear(new CrearCuentaDto("3", "cta3", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, 2,null).AParametrosCuenta()).Valor!;
            cta3.Id = 3;
            var cta4 = CuentaContable.Crear(new CrearCuentaDto("4", "cta4", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, 3, null).AParametrosCuenta()).Valor!;
            cta4.Id = 4;
            var cta5 = CuentaContable.Crear(new CrearCuentaDto("5", "cta5", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, 4, null).AParametrosCuenta()).Valor!;
            cta5.Id = 5;
            var cta6 = CuentaContable.Crear(new CrearCuentaDto("6", "cta6", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, 5, null).AParametrosCuenta()).Valor!;
            cta6.Id = 6;

            contexto.CuentasContables.AddRange(
            CuentaContableMapeo.AInfraestructura(cta1),
            CuentaContableMapeo.AInfraestructura(cta2),
            CuentaContableMapeo.AInfraestructura(cta3),
            CuentaContableMapeo.AInfraestructura(cta4),
            CuentaContableMapeo.AInfraestructura(cta5),
            CuentaContableMapeo.AInfraestructura(cta6));

            await contexto.SaveChangesAsync();

            var repository = new PlanDeCuentaRepositorio(contexto, null);

            // Act
            var resultado = await repository.SuperaMaximoNivelPermitido(6);

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public async Task Crear_CuentaContable_Falla_Referencia_Circular()
        {
            var opcion = new DbContextOptionsBuilder<ContextoAplicacion>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            await using var contexto = new ContextoAplicacion(opcion);

            var cta1 = CuentaContable.Crear(new CrearCuentaDto("1", "cta1", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, 6, null).AParametrosCuenta()).Valor!;
            cta1.Id = 1;
            var cta2 = CuentaContable.Crear(new CrearCuentaDto("2", "cta2", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, 1, null).AParametrosCuenta()).Valor!;
            cta2.Id = 2;
            var cta3 = CuentaContable.Crear(new CrearCuentaDto("3", "cta3", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, 2, null).AParametrosCuenta()).Valor!;
            cta3.Id = 3;
            var cta4 = CuentaContable.Crear(new CrearCuentaDto("4", "cta4", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, 3, null).AParametrosCuenta()).Valor!;
            cta4.Id = 4;
            var cta5 = CuentaContable.Crear(new CrearCuentaDto("5", "cta5", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, 4, null).AParametrosCuenta()).Valor!;
            cta5.Id = 5;
            var cta6 = CuentaContable.Crear(new CrearCuentaDto("6", "cta6", false, Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas, Dominio.Contabilidad.Enums.MonedaTipo.Local, false, null, 5, null).AParametrosCuenta()).Valor!;
            cta6.Id = 6;

            contexto.CuentasContables.AddRange(
            CuentaContableMapeo.AInfraestructura(cta1),
            CuentaContableMapeo.AInfraestructura(cta2),
            CuentaContableMapeo.AInfraestructura(cta3),
            CuentaContableMapeo.AInfraestructura(cta4),
            CuentaContableMapeo.AInfraestructura(cta5),
            CuentaContableMapeo.AInfraestructura(cta6));

            await contexto.SaveChangesAsync();

            var repository = new PlanDeCuentaRepositorio(contexto, null);

            // Act
            var resultado = await repository.EsReferenciaCircular(6,5);

            // Assert
            Assert.IsTrue(resultado);
        }
    }
}
