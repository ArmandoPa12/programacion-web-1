using FluentValidation;
using web_hello_world.Dto;
using web_hello_world.Repository;

namespace web_hello_world.Validators
{
    public class UsersValidator : AbstractValidator<Users>
    {
        private readonly IUserRepository _userRepository;
        
        public UsersValidator(IUserRepository userRepository) {
            _userRepository = userRepository;
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .Length(3, 50).WithMessage("El nombre de usuario debe tener entre 3 y 50 caracteres.");


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico no es válido.");

            RuleFor(x => x)
                .MustAsync(async(usuario, cancellation) =>
                {
                    int totalUsuarios = await _userRepository.GetCountAsync();
                    return totalUsuarios < 6;
                })
                .WithMessage("No se pueden crear más de 6 usuarios.");
        }
    }
}
