using FluentValidation;
using fyd.backend.Aplicacion.Contabilidad.DTOs;

namespace fyd.backend.Aplicacion.Contabilidad.Validadores
{
    public class VerificarEstadoDtoValidator : AbstractValidator<VerificarEstadoDto>
    {
        public VerificarEstadoDtoValidator()
        {
            RuleFor(x => x.EmpresaId)
                .GreaterThan(0).WithMessage("El ID de empresa es requerido.");

            RuleFor(x => x.Anio)
                .InclusiveBetween(1900, 2100).WithMessage("El año debe estar entre 1900 y 2100.");

            RuleFor(x => x.Mes)
                .InclusiveBetween(1, 12).WithMessage("El mes debe estar entre 1 y 12.");
        }
    }

    public class GenerarConsolidacionDtoValidator : AbstractValidator<GenerarConsolidacionDto>
    {
        public GenerarConsolidacionDtoValidator()
        {
            RuleFor(x => x.EmpresaId)
                .GreaterThan(0).WithMessage("El ID de empresa es requerido.");

            RuleFor(x => x.Anio)
                .InclusiveBetween(1900, 2100).WithMessage("El año debe estar entre 1900 y 2100.");

            RuleFor(x => x.Mes)
                .InclusiveBetween(1, 12).WithMessage("El mes debe estar entre 1 y 12.");

            RuleFor(x => x.NumeraTipoId)
                .GreaterThan(0).WithMessage("Debe seleccionar una numeración.");

            RuleFor(x => x.Modulos)
                .NotEmpty().WithMessage("Debe seleccionar al menos un módulo.");
        }
    }

    public class EliminarConsolidacionDtoValidator : AbstractValidator<EliminarConsolidacionDto>
    {
        public EliminarConsolidacionDtoValidator()
        {
            RuleFor(x => x.EmpresaId)
                .GreaterThan(0).WithMessage("El ID de empresa es requerido.");

            RuleFor(x => x.Anio)
                .InclusiveBetween(1900, 2100).WithMessage("El año debe estar entre 1900 y 2100.");

            RuleFor(x => x.Mes)
                .InclusiveBetween(1, 12).WithMessage("El mes debe estar entre 1 y 12.");

            RuleFor(x => x.Modulos)
                .NotEmpty().WithMessage("Debe seleccionar al menos un módulo.");
        }
    }
}
