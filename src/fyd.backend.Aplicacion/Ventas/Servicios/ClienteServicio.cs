using fyd.backend.Aplicacion.Ventas.DTOs;
using fyd.backend.Aplicacion.Ventas.Mapeos;
using fyd.backend.Aplicacion.Ventas.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.General;
using fyd.backend.Dominio.Ventas.Entidades;
using fyd.backend.Dominio.Ventas.Errores;
using fyd.backend.Dominio.Ventas.Repositorios;
using System.Threading.Tasks;

namespace fyd.backend.Aplicacion.Ventas.Servicios
{
    public class ClienteServicio : IClienteServicio
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public ClienteServicio(
            IClienteRepositorio clienteRepositorio,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            _clienteRepositorio = clienteRepositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Resultado<ConsultarClienteDto>> ObtenerPorId(int id)
        {
            var cliente = await _clienteRepositorio.ObtenerPorId(id);
            if (cliente == null)
            {
                return Resultado<ConsultarClienteDto>.Falla(ClienteError.NoEncontrado(id));
            }

            var dto = ClienteDtoMapeo.A_ConsultarDto(cliente);
            return Resultado<ConsultarClienteDto>.Exito(dto);
        }

        public async Task<Resultado<int>> Crear(CrearClienteDto dto)
        {
            // SRV-09: Código autonumerable
            int codigo = dto.Codigo ?? await _clienteRepositorio.ObtenerSiguienteCodigo();

            // SRV-01: Código único
            var existeCodigo = await _clienteRepositorio.ExisteCodigo(codigo);
            if (existeCodigo)
                return Resultado<int>.Falla(ClienteError.CodigoDuplicado);

            // 1. Crear agenda o usar existente
            int agendaId = 1; // TODO: Integrar con AgendaServicio para crear/actualizar la gnrAgenda

            var parametros = ClienteDtoMapeo.AlParametro(dto, agendaId);
            parametros.Codigo = codigo;
            
            var resultadoCreacion = Cliente.Crear(parametros);
            if (!resultadoCreacion.Exitoso)
                return Resultado<int>.Falla(resultadoCreacion.Error!);

            var cliente = resultadoCreacion.Valor!;

            await _clienteRepositorio.Agregar(cliente);
            await _unidadDeTrabajo.GuardarCambios(); // Genera Cliente.Id auto incremental de la DB

            return Resultado<int>.Exito(cliente.Id);
        }

        public async Task<Resultado> Actualizar(ActualizarClienteDto dto)
        {
            var cliente = await _clienteRepositorio.ObtenerPorId(dto.Id);
            if (cliente == null)
                return Resultado.Falla(ClienteError.NoEncontrado(dto.Id));

            int codigo = dto.Codigo ?? cliente.Codigo;

            var existeCodigo = await _clienteRepositorio.ExisteCodigo(codigo, dto.Id);
            if (existeCodigo)
                return Resultado.Falla(ClienteError.CodigoDuplicado);

            var parametros = ClienteDtoMapeo.AlParametro(dto, cliente.AgendaId);
            parametros.Codigo = codigo;

            var resultadoActualizacion = cliente.Actualizar(parametros);
            if (!resultadoActualizacion.Exitoso)
                return resultadoActualizacion;

            await _unidadDeTrabajo.GuardarCambios();

            return Resultado.Exito();
        }

        public async Task<Resultado> Eliminar(int id)
        {
            var cliente = await _clienteRepositorio.ObtenerPorId(id);
            if (cliente == null)
                return Resultado.Falla(ClienteError.NoEncontrado(id));

            // DEL-01 a DEL-04
            if (await _clienteRepositorio.TieneComprobantes(id))
                return Resultado.Falla(ClienteError.TieneComprobantes);

            if (await _clienteRepositorio.TieneContactos(cliente.AgendaId))
                return Resultado.Falla(ClienteError.TieneContactos);
            
            if (await _clienteRepositorio.TieneMemos(id))
                return Resultado.Falla(ClienteError.TieneMemos);

            if (await _clienteRepositorio.EsClienteCcDeOtros(id))
                return Resultado.Falla(ClienteError.ActuaComoClienteCC);

            // DEL-05 a DEL-06
            if (await _clienteRepositorio.EsClientePredeterminado(id) || 
                await _clienteRepositorio.EsEventualPredeterminado(id))
            {
                return Resultado.Falla(ClienteError.ActuaComoClienteCC); // Placeholder de un error custom
            }

            _clienteRepositorio.Eliminar(cliente);
            await _unidadDeTrabajo.GuardarCambios();

            return Resultado.Exito();
        }
    }
}
