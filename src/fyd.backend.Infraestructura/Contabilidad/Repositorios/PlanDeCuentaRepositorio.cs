using Entidad = fyd.backend.Infraestructura.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Infraestructura.ORM;
using Microsoft.EntityFrameworkCore;
using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Infraestructura.Contabilidad.Mapeos;
namespace fyd.backend.Infraestructura.Contabilidad.Repositorios
{
    public class PlanDeCuentaRepositorio : IPlanDeCuentaRepositorio
    {
        private readonly ContextoAplicacion _contextoAplicacion;
        private readonly ContextoLectura? _contextoLectura;

        public PlanDeCuentaRepositorio(ContextoAplicacion contextoAplicacion, ContextoLectura? contextoLectura)
        {
            _contextoAplicacion = contextoAplicacion;
            _contextoLectura = contextoLectura;
        }

        public async Task<CuentaContable?> ObtenerPorId(int id)
        {
            var entidad = await _contextoAplicacion.CuentasContables
                .Include(c => c.CuentasHijas)
                .Include(c => c.Grupos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entidad == null) return null;

            return CuentaContableMapeo.ADominio(entidad);
        }

        public async Task Agregar(CuentaContable cuenta)
        {
            var entidad = CuentaContableMapeo.AInfraestructura(cuenta);
            await _contextoAplicacion.CuentasContables.AddAsync(entidad);
        }

        public async Task Actualizar(CuentaContable cuenta)
        {
            var entidad = await _contextoAplicacion.CuentasContables
                .Include(c => c.Grupos)
                .FirstOrDefaultAsync(c => c.Id == cuenta.Id);

            if (entidad == null) return;

            entidad.Codigo = cuenta.Codigo;
            entidad.Nombre = cuenta.Nombre;
            entidad.AsientoOk = cuenta.AsientoOk;
            entidad.SubcuentaTipo = cuenta.SubcuentaTipo;
            entidad.MonedaTipo = cuenta.MonedaTipo;
            entidad.AjustaOk = cuenta.AjustaOk;
            entidad.EmpresaId = cuenta.EmpresaId;
            entidad.CuentaIdMadre = cuenta.CuentaIdMadre;

            // Sincronizar colección Grupos (many-to-many)
            entidad.Grupos.Clear();
            if (cuenta.Grupos != null && cuenta.Grupos.Any())
            {
                var gruposIds = cuenta.Grupos.Select(g => g.Id).ToList();
                var gruposTracked = await _contextoAplicacion.Grupos
                    .Where(g => gruposIds.Contains(g.Id))
                    .ToListAsync();
                foreach (var grupo in gruposTracked)
                    entidad.Grupos.Add(grupo);
            }
        }

        public void Eliminar(CuentaContable cuenta)
        {
            var entidad = CuentaContableMapeo.AInfraestructura(cuenta);
            _contextoAplicacion.CuentasContables.Remove(entidad);
        }

        public async Task<bool> TieneMovimientos(int cuentaId)
        {
            // TODO: Falta implementar validación contra las siguientes entidades 
            // que aún no existen en el ContextoAplicacion:
            // - ctbModeloCuentas (Modelos de Cuentas)
            // - gnrImputaciones (Imputaciones genéricas)
            // - cpsCategorias (Categorías de compra)
            // - vtsCategorias (Categorías de venta)
            // - fdsCajas (Cajas)
            // - fdsTarjetas (Tarjetas)
            // - fdsTickets (Tickets)
            // - gnrArticulos (Artículos)
            // - gnrImpuestos (Impuestos)
            // - gnrIvaAlicuotas (Alícuotas de IVA)
            
            bool tieneAsientos = await _contextoAplicacion.Asientos.AnyAsync(a => a.CuentaId == cuentaId);
            if (tieneAsientos) return true;
            
            bool tieneGrupoCuentas = await _contextoAplicacion.Grupos.AnyAsync(g => g.CuentasContables.Any(c => c.Id == cuentaId));
            if (tieneGrupoCuentas) return true;
            
            bool usadoClientes = await _contextoAplicacion.Clientes.AnyAsync(c => c.CuentaId == cuentaId);
            if (usadoClientes) return true;

            bool usadoProveedores = await _contextoAplicacion.Proveedores.AnyAsync(p => p.CuentaId == cuentaId);
            if (usadoProveedores) return true;

            return false;
        }

        public async Task<bool> EsReferenciaCircular(int posibleCuentaHijaId, int posibleCuentaMadreId)
        {
            int? cuentaId = posibleCuentaMadreId;

            while (cuentaId.HasValue)
            {
                if (cuentaId == posibleCuentaHijaId) return true;

                var madreId = await _contextoAplicacion.CuentasContables
                    .Where(c => c.Id == cuentaId.Value)
                    .Select(c => c.CuentaIdMadre)
                    .FirstOrDefaultAsync();

                cuentaId = madreId;
            }

            return false;
        }

        public async Task<bool> TieneCuentasHijas(int cuentaId)
        {
            return await _contextoAplicacion.CuentasContables.AnyAsync(x => x.CuentaIdMadre == cuentaId);
        }

        public async Task<bool> ExisteCodigo(string codigo, int? ignorarId = null)
        {
            return await _contextoAplicacion.CuentasContables
                .AnyAsync(c => c.Codigo == codigo && (!ignorarId.HasValue || c.Id != ignorarId));
        }

        public async Task<bool> SuperaMaximoNivelPermitido(int cuentaMadreId)
        {
            var nivel = 1;
            int? cuentaMadreIdActual = cuentaMadreId;

            while (cuentaMadreIdActual != null)
            {
                nivel++;
                var cuentaMadre = await ObtenerPorId(cuentaMadreIdActual.Value);
                if (cuentaMadre != null)
                {
                    cuentaMadreIdActual = cuentaMadre!.CuentaIdMadre;
                }
                else
                {
                    cuentaMadreIdActual = null;
                }
            }

            return nivel > 5;
        }

        public async Task<ICollection<Grupo>> ObtenerGruposPorIds(IEnumerable<int> gruposIds)
        {
            if (gruposIds == null || !gruposIds.Any()) return new List<Grupo>();
            var lista = await _contextoAplicacion.Grupos
                .Where(g => gruposIds.Contains(g.Id))
                .ToListAsync();
            
            var resultado = new List<Grupo>();
            
            foreach(var grupo in lista)
            {
                resultado.Add(GrupoMapeo.ADominio(grupo));
            }
            return resultado;
        }
    }
}
