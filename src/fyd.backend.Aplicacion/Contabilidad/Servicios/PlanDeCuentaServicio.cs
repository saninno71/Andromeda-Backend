using fyd.backend.Aplicacion.Contabilidad.DTOs;
using fyd.backend.Aplicacion.Contabilidad.Servicios.Interfaces;
using fyd.backend.Dominio.Abstracciones;
using fyd.backend.Dominio.Contabilidad.Entidades;
using fyd.backend.Dominio.Contabilidad.Errores;
using fyd.backend.Dominio.Contabilidad.Repositorios;
using fyd.backend.Dominio.General;

namespace fyd.backend.Aplicacion.Contabilidad.Servicios
{
    public class PlanDeCuentaServicio : IPlanDeCuentaServicio
    {
        private readonly IPlanDeCuentaRepositorio _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public PlanDeCuentaServicio(IPlanDeCuentaRepositorio repo, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorio = repo;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Resultado<int>> CrearCuenta(CrearCuentaDto dto)
        {
            if (await _repositorio.ExisteCodigo(dto.Codigo))
            {
                return Resultado<int>.Falla(CuentaContableError.CodigoDuplicado(dto.Codigo));
            }

            if (dto.CuentaIdMadre.HasValue && await _repositorio.SuperaMaximoNivelPermitido(dto.CuentaIdMadre.Value))
            {
                return Resultado<int>.Falla(CuentaContableError.SuperaNivelMaximoPermitido);
            }

            var resultado = CuentaContable.Crear(dto.AParametrosCuenta());

            if (!resultado.Exitoso)
            {
                return Resultado<int>.Falla(resultado.Error!);
            }

            if (dto.GruposIds != null)
            {
                var gruposUnicos = dto.GruposIds.Distinct().ToList();
                var nuevosGrupos = await _repositorio.ObtenerGruposPorIds(gruposUnicos);

                if (nuevosGrupos.Count != gruposUnicos.Count)
                {
                    return Resultado<int>.Falla(CuentaContableError.GrupoNoEncontrado);
                }

                resultado.Valor!.AsignarGrupos(nuevosGrupos);
            }

            await _repositorio.Agregar(resultado.Valor!);

            await _unidadDeTrabajo.GuardarCambios();

            return Resultado<int>.Exito(resultado.Valor!.Id);
        }

        public async Task<Resultado> ActualizarCuenta(ActualizarCuentaDto dto)
        {
            var cuenta = await _repositorio.ObtenerPorId(dto.Id);

            if (cuenta == null)
            {
                return Resultado.Falla(CuentaContableError.NoEncontrada(dto.Id));
            }

            if (cuenta.Codigo != dto.Codigo)
            {
                if (await _repositorio.ExisteCodigo(dto.Codigo, ignorarId: dto.Id))
                {
                    return Resultado.Falla(CuentaContableError.CodigoDuplicado(dto.Codigo));
                }
            }

            if (dto.CuentaIdMadre.HasValue && dto.CuentaIdMadre.Value == cuenta.Id)
            {
                return Resultado.Falla(CuentaContableError.CuentaMadreNoValida);
            }

            if (dto.CuentaIdMadre.HasValue && await _repositorio.SuperaMaximoNivelPermitido(dto.CuentaIdMadre.Value))
            {
                return Resultado<int>.Falla(CuentaContableError.SuperaNivelMaximoPermitido);
            }

            if (dto.CuentaIdMadre.HasValue && dto.CuentaIdMadre != cuenta.CuentaIdMadre)
            {
                if (await _repositorio.EsReferenciaCircular(posibleCuentaHijaId: cuenta.Id, posibleCuentaMadreId: dto.CuentaIdMadre.Value))
                    return Resultado.Falla(CuentaContableError.ReferenciaCircular);
            }

            bool tieneMovimientos = await _repositorio.TieneMovimientos(cuenta.Id);

            if (tieneMovimientos && !dto.AsientoOk)
            {
                return Resultado.Falla(CuentaContableError.TieneMovimientos);
            }

            var resultado = cuenta.Actualizar(dto.AParametrosCuenta());

            if (!resultado.Exitoso)
                return resultado;

            // Actualizar Grupos (Borra anteriores y pone nuevos)
            if (dto.GruposIds != null)
            {
                var gruposUnicos = dto.GruposIds.Distinct().ToList();
                var nuevosGrupos = await _repositorio.ObtenerGruposPorIds(gruposUnicos);
                
                if (nuevosGrupos.Count != gruposUnicos.Count)
                {
                    return Resultado.Falla(CuentaContableError.GrupoNoEncontrado);
                }

                cuenta.AsignarGrupos(nuevosGrupos);
            }

            await _repositorio.Actualizar(cuenta);
            await _unidadDeTrabajo.GuardarCambios();

            return Resultado.Exito();
        }

        public async Task<Resultado> EliminarCuenta(int id)
        {
            var cuenta = await _repositorio.ObtenerPorId(id);

            if (cuenta is null)
            {
                return Resultado.Falla(CuentaContableError.NoEncontrada(id));
            }

            if (await _repositorio.TieneMovimientos(id))
            {
                return Resultado.Falla(CuentaContableError.TieneMovimientos);
            }

            if (await _repositorio.TieneCuentasHijas(id))
            {
                return Resultado.Falla(CuentaContableError.TieneCuentasHijas);
            }

            _repositorio.Eliminar(cuenta);

            await _unidadDeTrabajo.GuardarCambios();

            return Resultado.Exito();
        }

        public async Task<Resultado<ConsultarCuentaDto>> ObtenerPorId(int id)
        {
            var cuenta = await _repositorio.ObtenerPorId(id);

            if (cuenta == null)
            {
                return Resultado<ConsultarCuentaDto>.Falla(CuentaContableError.NoEncontrada(id));
            }

            var cuentaDto = ConsultarCuentaDto.DesdeEntidad(cuenta!);
           
            return Resultado<ConsultarCuentaDto>.Exito(cuentaDto);
        }
    }
}
