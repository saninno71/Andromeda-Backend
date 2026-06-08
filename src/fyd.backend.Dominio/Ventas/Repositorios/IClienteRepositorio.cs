using fyd.backend.Dominio.Ventas.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fyd.backend.Dominio.Ventas.Repositorios
{
    public interface IClienteRepositorio
    {
        Task<Cliente?> ObtenerPorId(int id);
        Task<bool> ExisteCodigo(int codigo, int? excludeId = null);
        Task<int> ObtenerSiguienteCodigo();
        Task<bool> TieneComprobantes(int clienteId);
        Task<bool> TieneContactos(int agendaId);
        Task<bool> TieneMemos(int clienteId);
        Task<bool> EsClienteCcDeOtros(int clienteId);
        Task<bool> EsClientePredeterminado(int clienteId);
        Task<bool> EsEventualPredeterminado(int clienteId);
        Task<List<Cliente>> ObtenerClientesQueConsolidanEn(int clienteCcId);
        Task Agregar(Cliente cliente);
        void Eliminar(Cliente cliente);
    }
}
