using FluentValidation;
using fyd.backend.Aplicacion.Compras.DTOs;
using fyd.backend.Dominio.General.Enums;

namespace fyd.backend.Aplicacion.Compras.Validadores
{
    public class InfoAgendaDtoValidator : AbstractValidator<InfoAgendaDto>
    {
        public InfoAgendaDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres.");
                
            RuleFor(x => x.NumeroDoc)
                .NotEmpty().WithMessage("El número de documento/CUIT es obligatorio cuando se completan datos de agenda.")
                .MaximumLength(20).WithMessage("El documento no puede exceder los 20 caracteres.");
        }
    }

    public class InfoRetencionDtoValidator : AbstractValidator<InfoRetencionDto>
    {
        public InfoRetencionDtoValidator()
        {
            RuleFor(x => x.RetencionId)
                .Must(EsIdRetencionValido)
                .WithMessage("El ID de retención debe coindicir con un valor válido (Retención/Percepción) dentro de TipoImporte.");
        }

        private bool EsIdRetencionValido(int id)
        {
            if (!System.Enum.IsDefined(typeof(TipoImporte), id)) return false;
            var tipo = (TipoImporte)id;
            return tipo == TipoImporte.IVAPercepcion || tipo == TipoImporte.IVARetencion ||
                   tipo == TipoImporte.IIBBPercepcion || tipo == TipoImporte.IIBBRetencion ||
                   tipo == TipoImporte.GananciasPercepcion || tipo == TipoImporte.GananciasRetencion ||
                   tipo == TipoImporte.SUSSRetencion;
        }
    }

    public class CrearProveedorDtoValidator : AbstractValidator<CrearProveedorDto>
    {
        public CrearProveedorDtoValidator()
        {
            RuleFor(x => x.Codigo).GreaterThan(0).When(x => x.Codigo.HasValue).WithMessage("El código debe ser mayor a 0 si se especifica.");
            RuleFor(x => x.AgendaId).GreaterThan(0).When(x => x.AgendaDatos == null).WithMessage("Debe estar vinculado a un contacto en agenda o proveer AgendaDatos.");
            
            RuleFor(x => x.AgendaDatos)
                .SetValidator(new InfoAgendaDtoValidator()!)
                .When(x => x.AgendaDatos != null);

            RuleForEach(x => x.Retenciones)
                .SetValidator(new InfoRetencionDtoValidator()!)
                .When(x => x.Retenciones != null);
        }
    }

    public class ActualizarProveedorDtoValidator : AbstractValidator<ActualizarProveedorDto>
    {
        public ActualizarProveedorDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");
            RuleFor(x => x.Codigo).GreaterThan(0).When(x => x.Codigo.HasValue).WithMessage("El código debe ser mayor a 0 si se especifica.");
            RuleFor(x => x.AgendaId).GreaterThan(0).When(x => x.AgendaDatos == null).WithMessage("Debe estar vinculado a un contacto en agenda o proveer AgendaDatos.");

            RuleFor(x => x.AgendaDatos)
                .SetValidator(new InfoAgendaDtoValidator()!)
                .When(x => x.AgendaDatos != null);

            RuleForEach(x => x.Retenciones)
                .SetValidator(new InfoRetencionDtoValidator()!)
                .When(x => x.Retenciones != null);
        }
    }
}
