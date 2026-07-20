using FluentValidation;
using Microsoft.EntityFrameworkCore;
using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoUsuario;

namespace web_api_deportivo.Validator
{
    public class AlumnoValidator : AbstractValidator<AlumnoDto>
    {
        private readonly AppDbContext _db;

        public AlumnoValidator(AppDbContext db)
        {
            _db = db;

            RuleFor(x => x.CarnetAlumno)
                .NotEmpty().WithMessage("El carnet del alumno es obligatorio.")
                .MaximumLength(20).WithMessage("El carnet no puede superar los 20 caracteres.")
                .MustAsync(async (dto, carnet, cancellationToken) =>
                    !await _db.Alumnos.AnyAsync(a => a.carnet_alumno == carnet && a.Id != dto.Id, cancellationToken))
                .WithMessage("El carnet ya se encuentra registrado.");

            RuleFor(x => x.NombreAlumno)
                .NotEmpty().WithMessage("El nombre del alumno es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.ApellidosAlumno)
                .NotEmpty().WithMessage("Los apellidos son obligatorios.")
                .MaximumLength(100).WithMessage("Los apellidos no pueden superar los 100 caracteres.");

            RuleFor(x => x.FechaNacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
                .LessThan(DateTime.Today)
                .WithMessage("La fecha de nacimiento debe ser anterior a la fecha actual.");

            RuleFor(x => x.Alergias)
                .MaximumLength(500)
                .WithMessage("Las alergias no pueden superar los 500 caracteres.");

            RuleFor(x => x.CondicionesMedicas)
                .MaximumLength(500)
                .WithMessage("Las condiciones médicas no pueden superar los 500 caracteres.");

            RuleForEach(x => x.ContactosEmergencia)
                .SetValidator(new ContactoEmergenciaValidator());
        }
    }
}
