using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Dominio.General;

namespace fyd.backend.Aplicacion.Contabilidad.Servicios
{
    public class IndiceContableServicio : IIndiceContableServicio
    {
        private readonly IIndiceContableRepositorio _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public IndiceContableServicio(IIndiceContableRepositorio repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Resultado<int>> Crear(CrearIndiceDto dto)
        {
            var periodoNormalizado = new DateTime(dto.Periodo.Year, dto.Periodo.Month, 1);

            if (await _repositorio.ExistePeriodo(periodoNormalizado))
            {
                return Resultado<int>.Falla(IndiceContableError.PeriodoDuplicado(periodoNormalizado));
            }

            var resultado = IndiceContable.Crear(dto.AParametrosIndice());

            if (!resultado.Exitoso)
            {
                return Resultado<int>.Falla(resultado.Error!);
            }

            await _repositorio.Agregar(resultado.Valor!);
            await _unidadDeTrabajo.GuardarCambios();

            return Resultado<int>.Exito(resultado.Valor!.Id);
        }

        public async Task<Resultado> Actualizar(ActualizarIndiceDto dto)
        {
            var indice = await _repositorio.ObtenerPorId(dto.Id);

            if (indice == null)
            {
                return Resultado.Falla(IndiceContableError.NoEncontrado(dto.Id));
            }

            var periodoNormalizado = new DateTime(dto.Periodo.Year, dto.Periodo.Month, 1);

            if (indice.Periodo != periodoNormalizado)
            {
                if (await _repositorio.ExistePeriodo(periodoNormalizado, ignorarId: dto.Id))
                {
                    return Resultado.Falla(IndiceContableError.PeriodoDuplicado(periodoNormalizado));
                }
            }

            var resultadoActualizar = indice.Actualizar(dto.AParametrosIndice());

            if (!resultadoActualizar.Exitoso)
            {
                return resultadoActualizar;
            }

            await _unidadDeTrabajo.GuardarCambios();

            return Resultado.Exito();
        }

        public async Task<Resultado> Eliminar(int id)
        {
            var indice = await _repositorio.ObtenerPorId(id);

            if (indice == null)
            {
                return Resultado.Falla(IndiceContableError.NoEncontrado(id));
            }

            _repositorio.Eliminar(indice);
            await _unidadDeTrabajo.GuardarCambios();

            return Resultado.Exito();
        }

        public async Task<Resultado<ConsultarIndiceDto>> ObtenerPorId(int id)
        {
            var indice = await _repositorio.ObtenerPorId(id);

            if (indice == null)
            {
                return Resultado<ConsultarIndiceDto>.Falla(IndiceContableError.NoEncontrado(id));
            }

            return Resultado<ConsultarIndiceDto>.Exito(ConsultarIndiceDto.DesdeEntidad(indice));
        }
    }
}
