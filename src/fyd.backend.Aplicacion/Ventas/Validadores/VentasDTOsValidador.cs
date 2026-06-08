using FluentValidation;
using fyd.backend.Aplicacion.Ventas.DTOs;

namespace fyd.backend.Aplicacion.Ventas.Validadores
{
    public class CrearClienteDtoValidator : AbstractValidator<CrearClienteDto>
    {
        public CrearClienteDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("La razón social es obligatoria.")
                .MaximumLength(100).WithMessage("La razón social no puede exceder los 100 caracteres.");

            RuleFor(x => x.NumeroDoc)
                .Matches(@"^\d{11}$").When(x => x.TipoDocId == 1) // 1 = CUIT
                .WithMessage("El CUIT debe tener exactamente 11 dígitos sin guiones ni puntos.");

            RuleFor(x => x.PorcDescuento1).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PorcDescuento2).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PorcDescuento3).GreaterThanOrEqualTo(0);

            RuleFor(x => x.PorcIvaLiberado).InclusiveBetween(0, 100);
            RuleFor(x => x.ImpCredito).GreaterThanOrEqualTo(0);

            RuleFor(x => x.FechaAlta)
                .LessThanOrEqualTo(x => x.FechaBaja)
                .When(x => x.FechaAlta.HasValue && x.FechaBaja.HasValue)
                .WithMessage("La fecha de alta no puede ser posterior a la fecha de baja.");
        }
    }

    public class ActualizarClienteDtoValidator : AbstractValidator<ActualizarClienteDto>
    {
        public ActualizarClienteDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("La razón social es obligatoria.")
                .MaximumLength(100).WithMessage("La razón social no puede exceder los 100 caracteres.");

            RuleFor(x => x.NumeroDoc)
                .Matches(@"^\d{11}$").When(x => x.TipoDocId == 1)
                .WithMessage("El CUIT debe tener exactamente 11 dígitos sin guiones ni puntos.");

            RuleFor(x => x.PorcDescuento1).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PorcDescuento2).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PorcDescuento3).GreaterThanOrEqualTo(0);

            RuleFor(x => x.PorcIvaLiberado).InclusiveBetween(0, 100);
            RuleFor(x => x.ImpCredito).GreaterThanOrEqualTo(0);

            RuleFor(x => x.FechaAlta)
                .LessThanOrEqualTo(x => x.FechaBaja)
                .When(x => x.FechaAlta.HasValue && x.FechaBaja.HasValue)
                .WithMessage("La fecha de alta no puede ser posterior a la fecha de baja.");
        }
    }
}
