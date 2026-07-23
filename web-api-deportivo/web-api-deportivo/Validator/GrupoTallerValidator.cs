using FluentValidation;
using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoGrupo;
using Microsoft.EntityFrameworkCore;

namespace web_api_deportivo.Validator
{
    public class GrupoTallerValidator : AbstractValidator<GrupoTallerDto>
    {
        private readonly AppDbContext _db;

        public GrupoTallerValidator(AppDbContext db)
        {
            _db = db;
            RuleFor(x => x.NombreGrupo)
                .NotEmpty()
                .WithMessage("El nombre del grupo es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El nombre del grupo no puede superar los 100 caracteres.")
                .MustAsync(async (dto, nombre, ct) =>
                    !await _db.GruposTalleres
                        .AnyAsync(i => i.NombreGrupo == nombre && i.Id != dto.Id, ct))
                .WithMessage("Ya existe un grupo con ese nombre.");


            RuleFor(x => x.CupoMaximo)
                .GreaterThan(0)
                .WithMessage("El cupo máximo debe ser mayor a cero.");

            RuleFor(x => x.TallerId)
                .GreaterThan(0)
                .WithMessage("Debe seleccionar un taller válido.")
                .MustAsync(ExisteTaller)
                .WithMessage("El taller seleccionado no existe.");


            RuleFor(x => x.InfraestructuraId)
                .GreaterThan(0)
                .WithMessage("Debe seleccionar una infraestructura válida.")
                .MustAsync(ExisteInfraestructura)
                .WithMessage("La infraestructura seleccionada no existe.");


            RuleFor(x => x.ProfesorId)
                .GreaterThan(0)
                .WithMessage("Debe seleccionar un profesor válido.")
                .MustAsync(ExisteProfesor)
                .WithMessage("El profesor seleccionado no existe.");

            RuleFor(x => x.Horarios)
                .NotEmpty()
                .WithMessage("Debe registrar al menos un horario.");


            RuleForEach(x => x.Horarios)
                .ChildRules(horario =>
                {

                    horario.RuleFor(x => x.DiaSemana)
                        .InclusiveBetween(1, 7)
                        .WithMessage("El día de semana debe estar entre 1 y 7.");
                    horario.RuleFor(x => x.HoraInicio)
                        .NotEmpty()
                        .WithMessage("La hora de inicio es obligatoria.");


                    horario.RuleFor(x => x.HoraFin)
                        .NotEmpty()
                        .WithMessage("La hora de fin es obligatoria.");


                    horario.RuleFor(x => x)
                        .Must(h => h.HoraInicio < h.HoraFin)
                        .WithMessage("La hora de inicio debe ser menor a la hora de fin.");

                });


            RuleFor(x => x.Horarios)
                .Must(HorariosSinDuplicados)
                .WithMessage("No pueden existir horarios duplicados para el mismo grupo.");
        }

        private async Task<bool> ExisteTaller(
            int tallerId,
            CancellationToken cancellationToken)
        {
            return await _db.Talleres
                .AnyAsync(t => t.Id == tallerId, cancellationToken);
        }

        private async Task<bool> ExisteInfraestructura(
            int infraestructuraId,
            CancellationToken cancellationToken)
        {
            return await _db.Infraestructuras
                .AnyAsync(i => i.Id == infraestructuraId, cancellationToken);
        }

        private async Task<bool> ExisteProfesor(
            int profesorId,
            CancellationToken cancellationToken)
        {
            return await _db.Usuarios
                .AnyAsync(u => u.Id == profesorId, cancellationToken);
        }

        private bool HorariosSinDuplicados(
            List<HorarioGrupoDto> horarios)
        {
            return horarios
                .GroupBy(h => new
                {
                    h.DiaSemana,
                    h.HoraInicio,
                    h.HoraFin
                })
                .All(g => g.Count() == 1);
        }
    }
}
