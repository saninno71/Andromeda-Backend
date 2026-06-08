using fyd.backend.Aplicacion.Compras.DTOs;
using fyd.backend.Aplicacion.Compras.Mapeos;
using fyd.backend.Aplicacion.Compras.Servicios.Interfaces;
using fyd.backend.Dominio.Compras.Entidades;
using fyd.backend.Dominio.Compras.Errores;
using fyd.backend.Dominio.Compras.Repositorios;
using fyd.backend.Dominio.General;
using fyd.backend.Dominio.Abstracciones;
using System.Threading.Tasks;

namespace fyd.backend.Aplicacion.Compras.Servicios
{
    public class ProveedorServicio : IProveedorServicio
    {
        private readonly IProveedorRepositorio _proveedorRepositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public ProveedorServicio(IProveedorRepositorio proveedorRepositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _proveedorRepositorio = proveedorRepositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Resultado<int>> Crear(CrearProveedorDto dto)
        {
            int codigo = dto.Codigo ?? (await _proveedorRepositorio.ObtenerMaximoCodigo() + 1);

            if (await _proveedorRepositorio.ExisteCodigo(codigo))
            {
                return Resultado<int>.Falla(ProveedorError.CodigoDuplicado);
            }

            var parametros = dto.AParametros();
            parametros.Codigo = codigo;
            
            var resultado = Proveedor.Crear(parametros);

            if (!resultado.Exitoso)
                return Resultado<int>.Falla(resultado.Error!);

            var proveedor = resultado.Valor!;

            await _proveedorRepositorio.Agregar(proveedor);
            await _unidadDeTrabajo.GuardarCambios();

            return Resultado<int>.Exito(proveedor.Id);
        }

        public async Task<Resultado<ConsultarProveedorDto>> ObtenerPorId(int id)
        {
            var proveedor = await _proveedorRepositorio.ObtenerPorId(id);
            if (proveedor is null)
                return Resultado<ConsultarProveedorDto>.Falla(ProveedorError.NoEncontrado(id));

            return Resultado<ConsultarProveedorDto>.Exito(proveedor.AConsultarDto());
        }

        public async Task<Resultado> Actualizar(ActualizarProveedorDto dto)
        {
            var proveedor = await _proveedorRepositorio.ObtenerPorId(dto.Id);
            if (proveedor is null)
                return Resultado.Falla(ProveedorError.NoEncontrado(dto.Id));

            if (dto.ProveedorCcId != 0 && dto.ProveedorCcId == dto.Id)
            {
                return Resultado.Falla(ProveedorError.CircularidadCc);
            }

            int codigo = dto.Codigo ?? proveedor.Codigo;
            if (proveedor.Codigo != codigo && await _proveedorRepositorio.ExisteCodigo(codigo))
            {
                return Resultado.Falla(ProveedorError.CodigoDuplicado);
            }

            var parametros = dto.AParametros();
            parametros.Codigo = codigo;

            var resultado = proveedor.Actualizar(parametros);
            if (!resultado.Exitoso)
                return Resultado.Falla(resultado.Error!);

            _proveedorRepositorio.Actualizar(proveedor);
            await _unidadDeTrabajo.GuardarCambios();
            return Resultado.Exito();
        }

        public async Task<Resultado> Eliminar(int id)
        {
            var proveedor = await _proveedorRepositorio.ObtenerPorId(id);
            if (proveedor is null)
                return Resultado.Falla(ProveedorError.NoEncontrado(id));

            if (await _proveedorRepositorio.EnUso(id))
            {
                return Resultado.Falla(ProveedorError.EnUso);
            }

            _proveedorRepositorio.Eliminar(proveedor);
            await _unidadDeTrabajo.GuardarCambios();

            return Resultado.Exito();
        }

        public async Task<Resultado<bool>> TieneMovimientosEnCc(int id)
        {
            var existe = await _proveedorRepositorio.ObtenerPorId(id);
            if (existe is null)
                return Resultado<bool>.Falla(ProveedorError.NoEncontrado(id));

            var tiene = await _proveedorRepositorio.TieneMovimientosEnCc(id);
            return Resultado<bool>.Exito(tiene);
        }
    }
}
