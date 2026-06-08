using fyd.backend.Dominio.Ventas.Entidades;
using fyd.backend.Dominio.Ventas.Repositorios;
using fyd.backend.Infraestructura.ORM;
using fyd.backend.Infraestructura.Ventas.Mapeos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fyd.backend.Infraestructura.Ventas.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ContextoAplicacion _contexto;

        public ClienteRepositorio(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        public async Task<Cliente?> ObtenerPorId(int id)
        {
            var infra = await _contexto.Clientes
                .Include(c => c.Lineas)
                .Include(c => c.Percepciones)
                .Include(c => c.DatosAdicionales)
                .Include(c => c.Memos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (infra == null) return null;
            return ClienteMapeo.A_Dominio(infra);
        }

        public async Task<bool> ExisteCodigo(int codigo, int? excludeId = null)
        {
            var query = _contexto.Clientes.Where(c => c.Codigo == codigo);
            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }

        public async Task<int> ObtenerSiguienteCodigo()
        {
            var count = await _contexto.Clientes.CountAsync();
            if (count == 0) return 1;
            return await _contexto.Clientes.MaxAsync(c => c.Codigo) + 1;
        }

        public async Task<bool> TieneComprobantes(int clienteId)
        {
            return await _contexto.VentaComprobantes.AnyAsync(vc => vc.ClienteId == clienteId);
        }

        public async Task<bool> TieneContactos(int agendaId)
        {
            // TODO: Consultar si existen contactos asociados en la agenda.
            // Request depende de mapear tabla gnrContactos en contexto.
            return await Task.FromResult(false);
        }

        public async Task<bool> TieneMemos(int clienteId)
        {
            return await _contexto.ClientesMemos.AnyAsync(m => m.ClienteId == clienteId);
        }

        public async Task<bool> EsClienteCcDeOtros(int clienteId)
        {
            return await _contexto.Clientes.AnyAsync(c => c.ClienteCcId == clienteId && c.Id != clienteId);
        }

        public async Task<bool> EsClientePredeterminado(int clienteId)
        {
            // TODO: Consultar desde sistema de parámetros genérico
            return await Task.FromResult(false); 
        }

        public async Task<bool> EsEventualPredeterminado(int clienteId)
        {
            // TODO: Consultar desde sistema de parámetros genérico
            return await Task.FromResult(false);
        }

        public async Task<List<Cliente>> ObtenerClientesQueConsolidanEn(int clienteCcId)
        {
            var infraList = await _contexto.Clientes
                .Where(c => c.ClienteCcId == clienteCcId)
                .ToListAsync();

            var resultado = new List<Cliente>();
            foreach(var item in infraList)
            {
                var dom = ClienteMapeo.A_Dominio(item);
                if (dom != null) resultado.Add(dom);
            }
            return resultado;
        }

        public async Task Agregar(Cliente cliente)
        {
            var infra = ClienteMapeo.A_Infra(cliente);
            if (infra != null)
            {
                await _contexto.Clientes.AddAsync(infra);
                _contexto.SaveChanges(); // Persiste para recuperar el ID autonumerico 
                cliente.Id = infra.Id;   // Asigna ID al dominio
            }
        }

        public void Eliminar(Cliente cliente)
        {
            var infra = _contexto.Clientes.Local.First(c => c.Id == cliente.Id);
            //Si no hay cliente, tira error. Esto es intencional, porque el flujo de eliminación debería validar la existencia antes de llamar a este método.
            _contexto.Clientes.Remove(infra);
        }
    }
}
