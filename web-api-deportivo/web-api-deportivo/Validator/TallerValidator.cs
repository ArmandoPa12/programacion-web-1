using FluentValidation;
using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoGrupo;
using Microsoft.EntityFrameworkCore;

namespace web_api_deportivo.Validator
{
    public class TallerValidator : AbstractValidator<TallerDto>
    {
        private readonly AppDbContext _db;

        public TallerValidator(AppDbContext db)
        {
            _db = db;

            RuleFor(x => x.NombreTaller)
                .NotEmpty().WithMessage("El nombre del taller es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.")
                .MustAsync(async (dto, nombre, ct) =>
                    !await _db.Talleres
                        .AnyAsync(t => t.NombreTaller == nombre && t.Id != dto.Id, ct))
                .WithMessage("Ya existe un taller con ese nombre.");

            RuleFor(x => x.CategoriaEdad)
                .MaximumLength(50)
                .WithMessage("La categoría no puede superar los 50 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.CategoriaEdad));

            RuleFor(x => x.EdadMinima)
                .GreaterThanOrEqualTo(0)
                .WithMessage("La edad mínima debe ser mayor o igual a cero.");

            RuleFor(x => x.EdadMaxima)
                .GreaterThan(x => x.EdadMinima)
                .WithMessage("La edad máxima debe ser mayor que la edad mínima.");
        }
    }
}
