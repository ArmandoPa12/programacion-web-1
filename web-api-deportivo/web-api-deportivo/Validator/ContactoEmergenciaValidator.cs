using FluentValidation;
using web_api_deportivo.Dto.DtoUsuario;

namespace web_api_deportivo.Validator
{
    public class ContactoEmergenciaValidator : AbstractValidator<ContactoEmergenciaDto>
    {
        public ContactoEmergenciaValidator()
        {
            RuleFor(x => x.NombreCompleto)
                .NotEmpty().WithMessage("El nombre del contacto es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .MaximumLength(20).WithMessage("El teléfono no puede superar los 20 caracteres.");

            RuleFor(x => x.Parentesco)
                .NotEmpty().WithMessage("El parentesco es obligatorio.")
                .MaximumLength(50).WithMessage("El parentesco no puede superar los 50 caracteres.");

            RuleFor(x => x.Prioridad)
                .InclusiveBetween(1, 5)
                .WithMessage("La prioridad debe estar entre 1 y 5.");
        }
    }
}
