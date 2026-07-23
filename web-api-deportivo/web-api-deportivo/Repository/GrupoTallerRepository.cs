using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoGrupo;
using web_api_deportivo.Entity.EntityGrupo;
using web_api_deportivo.IRepository;
using Microsoft.EntityFrameworkCore;

namespace web_api_deportivo.Repository
{
    public class GrupoTallerRepository : IGrupoTallerRepository
    {
        private readonly AppDbContext _db;

        public GrupoTallerRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<GrupoTallerDto>> GetAllAsync()
        {
            var lista = await _db.GruposTalleres
                .Where(g => g.Activo == true)
                .Include(g => g.Taller)
                .Include(g => g.Infraestructura)
                .Include(g => g.Profesor)
                .Include(g => g.Horarios)
                .ToListAsync();

            return lista.Select(ToDto).ToList();
        }

        public async Task<GrupoTallerDto?> GetByIdAsync(int id)
        {
            var grupo = await _db.GruposTalleres
                .Include(g => g.Taller)
                .Include(g => g.Infraestructura)
                .Include(g => g.Profesor)
                .Include(g => g.Horarios)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (grupo == null)
                return null;

            return ToDto(grupo);
        }

        public async Task CreateAsync(GrupoTallerDto dto)
        {
            var grupo = new EGrupoTaller
            {
                TallerId = dto.TallerId,
                InfraestructuraId = dto.InfraestructuraId,
                ProfesorId = dto.ProfesorId,
                NombreGrupo = dto.NombreGrupo,
                CupoMaximo = dto.CupoMaximo,
                Activo = dto.Activo,

                Horarios = dto.Horarios.Select(h => new EHorarioGrupo
                {
                    DiaSemana = h.DiaSemana,
                    HoraInicio = h.HoraInicio,
                    HoraFin = h.HoraFin
                }).ToList()
            };

            _db.GruposTalleres.Add(grupo);

            await _db.SaveChangesAsync();

            dto.Id = grupo.Id;
        }

        public async Task UpdateAsync(int id, GrupoTallerDto dto)
        {
            var grupo = await _db.GruposTalleres
                .Include(g => g.Horarios)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (grupo == null)
                return;

            grupo.TallerId = dto.TallerId;
            grupo.InfraestructuraId = dto.InfraestructuraId;
            grupo.ProfesorId = dto.ProfesorId;
            grupo.NombreGrupo = dto.NombreGrupo;
            grupo.CupoMaximo = dto.CupoMaximo;
            grupo.Activo = dto.Activo;

            grupo.Horarios.Clear();

            if (dto.Horarios != null && dto.Horarios.Any())
            {
                foreach (var h in dto.Horarios)
                {
                    grupo.Horarios.Add(new EHorarioGrupo
                    {
                        DiaSemana = h.DiaSemana,
                        HoraInicio = h.HoraInicio,
                        HoraFin = h.HoraFin
                    });
                }
            }

            await _db.SaveChangesAsync();
        }

        public async Task<Boolean> DeleteAsync(int id)
        {
            var grupo = await _db.GruposTalleres.FindAsync(id);
            if (grupo == null)
            {
                return false;
            }
            grupo.Activo = false;
            await _db.SaveChangesAsync();


            return true;
        }

        private static GrupoTallerDto ToDto(EGrupoTaller grupo)
        {
            return new GrupoTallerDto
            {
                Id = grupo.Id,

                TallerId = grupo.TallerId,
                InfraestructuraId = grupo.InfraestructuraId,
                ProfesorId = grupo.ProfesorId,

                NombreGrupo = grupo.NombreGrupo,
                CupoMaximo = grupo.CupoMaximo,
                Activo = grupo.Activo,

                NombreTaller = grupo.Taller.NombreTaller,
                NombreInfraestructura = grupo.Infraestructura.NombreEspacio,
                NombreProfesor = grupo.Profesor.Nombre_completo,

                Horarios = grupo.Horarios
                    .OrderBy(h => h.DiaSemana)
                    .Select(h => new HorarioGrupoDto
                    {
                        Id = h.Id,
                        DiaSemana = h.DiaSemana,
                        HoraInicio = h.HoraInicio,
                        HoraFin = h.HoraFin
                    })
                    .ToList()
            };
        }
    }
}
