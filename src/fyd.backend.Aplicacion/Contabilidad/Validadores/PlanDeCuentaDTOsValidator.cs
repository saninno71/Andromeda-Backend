using FluentValidation;
using fyd.backend.Aplicacion.Contabilidad.DTOs;

namespace fyd.backend.Aplicacion.Contabilidad.Validadores
{
    public class CrearCuentaDtoValidator : AbstractValidator<CrearCuentaDto>
    {
        public CrearCuentaDtoValidator()
        {
            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("El código de la cuenta es requerido.")
                .MaximumLength(12).WithMessage("El código de la cuenta no puede superar los 12 caracteres.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la cuenta es requerido.")
                .MaximumLength(50).WithMessage("El nombre de la cuenta no puede superar los 50 caracteres.");

            RuleFor(x => x.SubcuentaTipo)
                .IsInEnum().WithMessage("Tipo de subcuenta inválido.");

            RuleFor(x => x.MonedaTipo)
                .IsInEnum().WithMessage("Tipo de moneda a asociar inválido.");
        }
    }

    public class ActualizarCuentaDtoValidator : AbstractValidator<ActualizarCuentaDto>
    {
        public ActualizarCuentaDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID de la cuenta debe ser mayor a 0.");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("El código de la cuenta es requerido.")
                .MaximumLength(12).WithMessage("El código de la cuenta no puede superar los 12 caracteres.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la cuenta es requerido.")
                .MaximumLength(50).WithMessage("El nombre de la cuenta no puede superar los 50 caracteres.");

            RuleFor(x => x.SubcuentaTipo)
                .IsInEnum().WithMessage("Tipo de subcuenta inválido.");

            RuleFor(x => x.MonedaTipo)
                .IsInEnum().WithMessage("Tipo de moneda a asociar inválido.");
        }
    }
}
