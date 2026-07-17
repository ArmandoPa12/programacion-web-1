using FluentValidation;
using Microsoft.EntityFrameworkCore;
using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoRoles;

namespace web_api_deportivo.Validator
{
    public class RolValidator : AbstractValidator<RolDto>
    {
        private readonly AppDbContext _db;
        public RolValidator(AppDbContext db)
        {
            _db = db;
            RuleFor(x => x.NombreRol)
                .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre del rol no puede superar los 50 caracteres.");
            RuleFor(x => x.Descripcion)
                .MaximumLength(200).WithMessage("La descripción no puede superar los 200 caracteres.");
            RuleFor(x => x.PermisosIds)
                .Must(NoTenerDuplicados).WithMessage("La lista de permisos contiene IDs duplicados.")
                .MustAsync(ExistenPermisos).WithMessage("Uno o más de los permisos seleccionados no existen en el sistema.");
        }
        private bool NoTenerDuplicados(List<int>? ids)
        {
            if (ids == null) return true;
            return ids.Count == ids.Distinct().Count();
        }
        private async Task<bool> ExistenPermisos(List<int>? ids, CancellationToken cancellationToken)
        {
            if (ids == null || !ids.Any()) return true;           
            var cantidadExistentes = await _db.Permisos
                .Where(p => ids.Contains(p.Id))
                .CountAsync(cancellationToken);
            return cantidadExistentes == ids.Count;
        }
    }

}
