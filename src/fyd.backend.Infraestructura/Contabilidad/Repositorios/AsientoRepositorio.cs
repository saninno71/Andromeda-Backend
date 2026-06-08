using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.Contabilidad.Mapeos;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;

namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class AsientoRepositorio : IAsientoRepositorio
    {
        private readonly ContextoAplicacion _contexto;

        public AsientoRepositorio(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        // =====================================================
        // CRUD Básico
        // =====================================================

        public async Task Agregar(Asiento asiento)
        {
            await _contexto.Asientos.AddAsync(AsientoMapeo.AInfraestructura(asiento));
        }

        public void Eliminar(Asiento asiento)
        {
            _contexto.Asientos.Remove(AsientoMapeo.AInfraestructura(asiento));
        }

        public async Task<Asiento?> ObtenerPorId(int id)
        {
            var infra = await _contexto.Asientos
                .Include(a => a.Comprobante)
                .FirstOrDefaultAsync(a => a.Id == id);

            return infra is null ? null : AsientoMapeo.ADominio(infra);
        }

        // =====================================================
        // Validaciones de Cuenta Contable (B1, B2, B3)
        // =====================================================

        public async Task<bool> ExisteCuenta(int cuentaId)
        {
            return await _contexto.CuentasContables.AnyAsync(c => c.Id == cuentaId);
        }

        public async Task<int?> ObtenerEmpresaIdDeCuenta(int cuentaId)
        {
            return await _contexto.CuentasContables
                .Where(c => c.Id == cuentaId)
                .Select(c => c.EmpresaId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CuentaPermiteAsiento(int cuentaId)
        {
            return await _contexto.CuentasContables
                .Where(c => c.Id == cuentaId)
                .Select(c => c.AsientoOk)
                .FirstOrDefaultAsync();
        }

        // =====================================================
        // Validaciones de Cliente (B4, B5)
        // =====================================================

        public async Task<bool> ExisteCliente(int clienteId)
        {
            // Cliente tiene AgendaId → Agenda tiene los datos de contacto.
            // La validación del VB6 usa cstAgenda WHERE ClienteID = ...
            // En nuestro modelo, el ClienteId del Asiento apunta a Cliente.Id
            return await _contexto.Clientes.AnyAsync(c => c.Id == clienteId);
        }

        public async Task<int?> ObtenerEmpresaIdDeCliente(int clienteId)
        {
            // Cliente → Agenda → EmpresaId (AgendaEmpresaID en el VB6)
            return await _contexto.Clientes
                .Where(c => c.Id == clienteId)
                .Join(_contexto.Agendas,
                    cliente => cliente.AgendaId,
                    agenda => agenda.Id,
                    (cliente, agenda) => agenda.EmpresaId)
                .FirstOrDefaultAsync();
        }

        // =====================================================
        // Validaciones de Proveedor (B6, B7)
        // =====================================================

        public async Task<bool> ExisteProveedor(int proveedorId)
        {
            return await _contexto.Proveedores.AnyAsync(p => p.Id == proveedorId);
        }

        public async Task<int?> ObtenerEmpresaIdDeProveedor(int proveedorId)
        {
            // Proveedor → Agenda → EmpresaId
            return await _contexto.Proveedores
                .Where(p => p.Id == proveedorId)
                .Join(_contexto.Agendas,
                    proveedor => proveedor.AgendaId,
                    agenda => agenda.Id,
                    (proveedor, agenda) => agenda.EmpresaId)
                .FirstOrDefaultAsync();
        }

        // =====================================================
        // Validaciones de Caja (B8, B9)
        // TODO: La entidad Caja (fdsCajas) aún no existe en el dominio.
        //       Cuando se cree, descomentar y ajustar.
        // =====================================================

        public Task<bool> ExisteCaja(int cajaId)
        {
            // TODO: Implementar cuando exista la entidad Caja (fdsCajas)
            // return await _contexto.Cajas.AnyAsync(c => c.Id == cajaId);
            return Task.FromResult(true); // Escenario positivo por defecto
        }

        public Task<int?> ObtenerEmpresaIdDeCaja(int cajaId)
        {
            // TODO: Implementar cuando exista la entidad Caja (fdsCajas)
            // return await _contexto.Cajas
            //     .Where(c => c.Id == cajaId)
            //     .Select(c => c.EmpresaId)
            //     .FirstOrDefaultAsync();
            return Task.FromResult<int?>(null); // Escenario positivo (null = todas las empresas)
        }

        // =====================================================
        // Validaciones de Período y Comprobante (B11, B12, B13, B14)
        // =====================================================

        public Task<bool> PeriodoEstaCerrado(DateTime fecha)
        {
            //TODO: La lógica del métododo PeriodoCerradoOK() en abmctbasiento.cls no me es clara, y tampoco tengo acceso a las bases cliente y servidor de Andrómeda.
            // por ahora retornamos "false" para simular que el período está abierto y poder seguír con un camino felíz. 
            return Task.FromResult(false);
        }

        public Task<bool> TieneComprobantePadre(int asientoId)
        {
            // TODO: Implementar cuando exista la tabla ctbAsientosComprobantes.
            //       El VB6 hace: SELECT ... FROM ctbAsientosComprobantes WHERE AsientoID = id
            //       Si encuentra registros, el asiento fue generado por otro comprobante.
            // return await _contexto.AsientosComprobantes.AnyAsync(ac => ac.AsientoId == asientoId);
            return Task.FromResult(false); // Escenario positivo: sin comprobante padre
        }

        public Task<bool> TieneVinculaciones(int asientoId)
        {
            // TODO: Implementar cuando exista la tabla gnrVinculaciones.
            //       El VB6 hace: SELECT ... FROM gnrVinculaciones WHERE AsientoID = id
            // return await _contexto.Vinculaciones.AnyAsync(v => v.AsientoId == asientoId);
            return Task.FromResult(false); // Escenario positivo: sin vinculaciones
        }

        public Task<bool> TieneAplicacionesEnCtaCte(int comprobanteId)
        {
            // TODO: Implementar cuando exista la tabla gnrAplicaciones.
            //       El VB6 consulta gnrAplicaciones cruzando con vtsCtasCtes y cpsCtasCtes
            //       para verificar si el comprobante está aplicado.
            // return await _contexto.Aplicaciones
            //     .AnyAsync(a => a.VentaCtaCteAfectado.VentaComprobante.ComprobanteId == comprobanteId
            //                 || a.CompraCtaCteAfectado.CompraComprobante.ComprobanteId == comprobanteId);
            return Task.FromResult(false); // Escenario positivo: sin aplicaciones
        }

        public Task<bool> CuentaTieneSubcuentaCaja(int cuentaId)
        {
            // TODO: Implementar lógica para verificar si la cuenta tiene subcuenta = caja
            // return await _contexto.Cuentas.AnyAsync(c => c.Id == cuentaId && c.Subcuenta == "Caja");
            return Task.FromResult(false);
        }

        public Task<bool> CajaBancariaEstaConciliada(int cajaId)
        {
            // TODO: Implementar lógica de conciliación bancaria
            return Task.FromResult(false);
        }

        public Task<bool> PeriodoEstaRenumerado(DateTime fecha)
        {
            // TODO: Implementar lógica para verificar si el período de la fecha fue renumerado
            // No tengo información en abmctbasiento.cls respecto de esto.
            return Task.FromResult(false);
        }

        public Task<bool> TieneNumeroAsientoAsignado(int comprobanteId)
        {
            // TODO: Implementar lógica para verificar si el comprobante ya tiene NumeroAsiento
            return Task.FromResult(false);
        }
    }
}
