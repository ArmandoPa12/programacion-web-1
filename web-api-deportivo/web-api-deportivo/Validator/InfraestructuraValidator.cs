using FluentValidation;
using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoGrupo;
using Microsoft.EntityFrameworkCore;

namespace web_api_deportivo.Validator
{
    public class InfraestructuraValidator : AbstractValidator<InfraestructuraDto>
    {
        private readonly AppDbContext _db;

        public InfraestructuraValidator(AppDbContext db)
        {
            _db = db;

            RuleFor(x => x.NombreEspacio)
                .NotEmpty().WithMessage("El nombre del espacio es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.")
                .MustAsync(async (dto, nombre, ct) =>
                    !await _db.Infraestructuras
                        .AnyAsync(i => i.NombreEspacio == nombre && i.Id != dto.Id, ct))
                .WithMessage("Ya existe una infraestructura con ese nombre.");

            RuleFor(x => x.UbicacionDetalle)
                .MaximumLength(255)
                .WithMessage("La ubicación no puede superar los 255 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.UbicacionDetalle));
        }
    }
}
