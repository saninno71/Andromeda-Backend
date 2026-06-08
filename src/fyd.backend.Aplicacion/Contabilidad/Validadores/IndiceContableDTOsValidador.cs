using FluentValidation;
using fyd.backend.Aplicacion.Contabilidad.DTOs;

namespace fyd.backend.Aplicacion.Contabilidad.Validadores
{
    public class CrearIndiceDtoValidator : AbstractValidator<CrearIndiceDto>
    {
        public CrearIndiceDtoValidator()
        {
            RuleFor(x => x.Periodo)
                .NotEqual(default(DateTime)).WithMessage("El período del índice es obligatorio.");

            RuleFor(x => x.Indice)
                .GreaterThan(0).WithMessage("El índice debe ser mayor a cero.");
        }
    }

    public class ActualizarIndiceDtoValidator : AbstractValidator<ActualizarIndiceDto>
    {
        public ActualizarIndiceDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID del índice debe ser mayor a 0.");

            RuleFor(x => x.Periodo)
                .NotEqual(default(DateTime)).WithMessage("El período del índice es obligatorio.");

            RuleFor(x => x.Indice)
                .GreaterThan(0).WithMessage("El índice debe ser mayor a cero.");
        }
    }
}
