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
    public sealed class AsientoServicioTest
    {
        [TestMethod]
        public async Task CrearAsiento_BalanceadoConCuentasValidas_GuardaEnDb()
        {
            // Arrange
            var opcion = new DbContextOptionsBuilder<ContextoAplicacion>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var contexto = new ContextoAplicacion(opcion);

            // Seed Account (Activo/Pasivo sin subcuenta caja para que pase la validación)
            var cuentaDebeDto = new CrearCuentaDto("1.1.1", "Banco", true,
                fyd.backend.Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas,
                fyd.backend.Dominio.Contabilidad.Enums.MonedaTipo.Local, false, 1, null,null);

            var cuentaDebe = CuentaContable.Crear(cuentaDebeDto.AParametrosCuenta()).Valor!;

            var cuentaHaberDto = new CrearCuentaDto("2.1.1", "Proveedores", true,
                fyd.backend.Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas,
                fyd.backend.Dominio.Contabilidad.Enums.MonedaTipo.Local, false, 1, null, null);

            var cuentaHaber = CuentaContable.Crear(cuentaHaberDto.AParametrosCuenta()).Valor!;

            var infraCuentaDebe = CuentaContableMapeo.AInfraestructura(cuentaDebe);
            var infraCuentaHaber = CuentaContableMapeo.AInfraestructura(cuentaHaber);
            contexto.CuentasContables.AddRange(infraCuentaDebe, infraCuentaHaber);
            await contexto.SaveChangesAsync();
            cuentaDebe.Id = infraCuentaDebe.Id;
            cuentaHaber.Id = infraCuentaHaber.Id;

            var repositorio = new AsientoRepositorio(contexto);
            var unidadDeTrabajo = new UnidadDeTrabajo(contexto);
            var servicio = new AsientoServicio(repositorio, unidadDeTrabajo);

            // Act
            var asientoDto = new CrearAsientoDto(
                Fecha: DateTime.Now,
                FechaEmision: DateTime.Now,
                NumeraTipoId: 1,
                Numero: null,
                EmpresaId: 1,
                MonedaId: 1,
                CotizacionLocal: 1m,
                CotizacionReferencia: 100m,
                Detalle: "Pago a Proveedor",
                Observaciones: "Test Integration",
                Lineas: new List<CrearAsientoLineaDto>
                {
                    new CrearAsientoLineaDto(cuentaDebe.Id, 1, 500m, "D", null, null, null),
                    new CrearAsientoLineaDto(cuentaHaber.Id, -1, 500m, "H", null, null, null)
                }
            );

            var resultado = await servicio.CrearAsiento(asientoDto);

            // Assert
            Assert.IsTrue(resultado.Exitoso, $"Debería crear el asiento correctamente. Error: {resultado.Error?.Codigo}");
            
            var asientosBd = await contexto.Asientos.ToListAsync();
            Assert.HasCount(2, asientosBd, "Deberían haberse guardado 2 líneas de asiento en la BD");

            //TODO: Validar si esto tiene sentido. Se hizo con copilot.            
            // var comprobantesBd = await contexto.Set<fyd.backend.Infraestructura.General.Entidades.Comprobante>().ToListAsync();
            // Assert.HasCount(1, comprobantesBd, "Debería haberse guardado 1 comprobante de cabecera");
            
            // Assert.AreEqual(500m, comprobantesBd.First().ImpTotal, "El importe total del comprobante debe ser la suma del debe");
        }

        [TestMethod]
        public async Task CrearAsiento_ConCuentaSinPermitirAsientos_DevuelveFalla()
        {
            // Arrange
            var opcion = new DbContextOptionsBuilder<ContextoAplicacion>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var contexto = new ContextoAplicacion(opcion);

            var cuentaDto = new CrearCuentaDto("1.1", "Madre", false,
                fyd.backend.Dominio.Contabilidad.Enums.SubcuentaTipo.SinSubcuentas,
                fyd.backend.Dominio.Contabilidad.Enums.MonedaTipo.Local, false, 1, null,null);

            // Seed Account con AsientoOk = false
            var cuenta = CuentaContable.Crear(cuentaDto.AParametrosCuenta()).Valor!;

            var infraCuenta = CuentaContableMapeo.AInfraestructura(cuenta);
            contexto.CuentasContables.Add(infraCuenta);
            await contexto.SaveChangesAsync();
            cuenta.Id = infraCuenta.Id;

            var repositorio = new AsientoRepositorio(contexto);
            var unidadDeTrabajo = new UnidadDeTrabajo(contexto);
            var servicio = new AsientoServicio(repositorio, unidadDeTrabajo);

            // Act
            var asientoDto = new CrearAsientoDto(
                Fecha: DateTime.Now,
                FechaEmision: DateTime.Now,
                NumeraTipoId: 1,
                Numero: null,
                EmpresaId: 1,
                MonedaId: 1,
                CotizacionLocal: 1m,
                CotizacionReferencia: 10m,
                Detalle: "Error asientoo",
                Observaciones: null,
                Lineas: new List<CrearAsientoLineaDto>
                {
                    new CrearAsientoLineaDto(cuenta.Id, 1, 100m, "D", null, null, null),
                    new CrearAsientoLineaDto(cuenta.Id, -1, 100m, "H", null, null, null)
                }
            );

            var resultado = await servicio.CrearAsiento(asientoDto);

            // Assert
            Assert.IsFalse(resultado.Exitoso);
            Assert.AreEqual("Asiento.CuentaNoPermiteAsiento", resultado.Error!.Codigo);
        }
    }
}
