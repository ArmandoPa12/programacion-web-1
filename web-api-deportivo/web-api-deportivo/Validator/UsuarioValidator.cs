using FluentValidation;
using web_api_deportivo.Conection;
using Microsoft.EntityFrameworkCore;
using web_api_deportivo.Dto.DtoUsuario;

namespace web_api_deportivo.Validator
{
    public class UsuarioValidator : AbstractValidator<UsuarioDto>
    {
        private readonly AppDbContext _db;

        public UsuarioValidator(AppDbContext db)
        {
            _db = db;

            RuleFor(x => x.Nombre_completo)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.Carnet_usuario)
                .NotEmpty().WithMessage("El carnet de usuario es obligatorio.")
                .MaximumLength(20).WithMessage("El carnet no puede superar los 20 caracteres.")
                .MustAsync(async (dto, carnet, cancellationToken) =>
                    !await _db.Usuarios.AnyAsync(u => u.Carnet_usuario == carnet && u.Id != dto.Id, cancellationToken))
                .WithMessage("El carnet de usuario ya se encuentra registrado en otro usuario.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo electrónico no es válido.")
                .MaximumLength(100).WithMessage("El correo electrónico no puede superar los 100 caracteres.")
                .MustAsync(async (dto, email, cancellationToken) =>
                    !await _db.Usuarios.AnyAsync(u => u.Email == email && u.Id != dto.Id, cancellationToken))
                .WithMessage("El correo electrónico ya se encuentra registrado en otro usuario.");

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.RolId)
                .GreaterThan(0).WithMessage("Debe seleccionar un Rol válido.")
                .MustAsync(ExisteRol).WithMessage("El Rol seleccionado no existe en el sistema.");
        }

        private async Task<bool> ExisteRol(int rolId, CancellationToken cancellationToken)
        {
            return await _db.Roles.AnyAsync(r => r.Id == rolId, cancellationToken);
        }
    }
}
