using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoUsuario;
using web_api_deportivo.Entity.EntityUsuario;
using web_api_deportivo.IRepository;
using Microsoft.EntityFrameworkCore;

namespace web_api_deportivo.Repository
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private readonly AppDbContext _db;

        public AlumnoRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<AlumnoDto>> GetAllAsync()
        {
            var alumnos = await _db.Alumnos
                .Include(a => a.ContactosEmergencia)
                .ToListAsync();

            return alumnos.Select(ToDto).ToList();
        }

        public async Task<AlumnoDto?> GetByIdAsync(int id)
        {
            var alumno = await _db.Alumnos
                .Include(a => a.ContactosEmergencia)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (alumno == null)
                return null;

            return ToDto(alumno);
        }

        public async Task<AlumnoDto> CreateAsync(AlumnoDto dto)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var alumno = new EAlumno
                {
                    carnet_alumno = dto.CarnetAlumno,
                    Nombre_Alumnos = dto.NombreAlumno,
                    Apellidos_alumno = dto.ApellidosAlumno,
                    Fecha_nacimiento = dto.FechaNacimiento.ToUniversalTime(),
                    Alergias = dto.Alergias,
                    Condiciones_Medicas = dto.CondicionesMedicas,
                    Activo = dto.Activo,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _db.Alumnos.Add(alumno);
                await _db.SaveChangesAsync();

                if (dto.ContactosEmergencia != null && dto.ContactosEmergencia.Any())
                {
                    var contactos = dto.ContactosEmergencia.Select(c => new EContactoEmergencias
                    {
                        Alumno_id = alumno.Id,
                        Nombre_Completo = c.NombreCompleto,
                        Telefono = c.Telefono,
                        Parentesco = c.Parentesco,
                        Prioridad = c.Prioridad,
                        Activo = c.Activo
                    }).ToList();

                    _db.ContactosEmergencias.AddRange(contactos);
                    await _db.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return await GetByIdAsync(alumno.Id)
                    ?? throw new Exception("No se pudo recuperar el alumno creado.");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, AlumnoDto dto)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var alumno = await _db.Alumnos
                    .Include(a => a.ContactosEmergencia)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (alumno == null)
                    return false;

                alumno.carnet_alumno = dto.CarnetAlumno;
                alumno.Nombre_Alumnos = dto.NombreAlumno;
                alumno.Apellidos_alumno = dto.ApellidosAlumno;
                alumno.Fecha_nacimiento = dto.FechaNacimiento.ToUniversalTime();    
                alumno.Alergias = dto.Alergias;
                alumno.Condiciones_Medicas = dto.CondicionesMedicas;
                alumno.Activo = dto.Activo;
                alumno.UpdatedAt = DateTime.UtcNow;

                if (alumno.ContactosEmergencia.Any())
                {
                    _db.ContactosEmergencias.RemoveRange(alumno.ContactosEmergencia);
                }

                if (dto.ContactosEmergencia != null && dto.ContactosEmergencia.Any())
                {
                    var contactos = dto.ContactosEmergencia.Select(c => new EContactoEmergencias
                    {
                        Alumno_id = alumno.Id,
                        Nombre_Completo = c.NombreCompleto,
                        Telefono = c.Telefono,
                        Parentesco = c.Parentesco,
                        Prioridad = c.Prioridad,
                        Activo = c.Activo
                    });

                    await _db.ContactosEmergencias.AddRangeAsync(contactos);
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alumno = await _db.Alumnos.FindAsync(id);

            if (alumno == null)
                return false;

            _db.Alumnos.Remove(alumno);

            await _db.SaveChangesAsync();

            return true;
        }

        private AlumnoDto ToDto(EAlumno alumno)
        {
            return new AlumnoDto
            {
                Id = alumno.Id,
                CarnetAlumno = alumno.carnet_alumno,
                NombreAlumno = alumno.Nombre_Alumnos,
                ApellidosAlumno = alumno.Apellidos_alumno,
                FechaNacimiento = alumno.Fecha_nacimiento,
                Alergias = alumno.Alergias ?? string.Empty,
                CondicionesMedicas = alumno.Condiciones_Medicas ?? string.Empty,
                Activo = alumno.Activo,

                ContactosEmergencia = alumno.ContactosEmergencia
                    .OrderBy(c => c.Prioridad)
                    .Select(c => new ContactoEmergenciaDto
                    {
                        Id = c.Id,
                        AlumnoId = c.Alumno_id,
                        NombreCompleto = c.Nombre_Completo,
                        Telefono = c.Telefono,
                        Parentesco = c.Parentesco,
                        Prioridad = c.Prioridad,
                        Activo = c.Activo
                    }).ToList()
            };
        }
    }
}
