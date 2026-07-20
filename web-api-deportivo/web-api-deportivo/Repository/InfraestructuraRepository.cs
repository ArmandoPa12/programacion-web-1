using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoGrupo;
using web_api_deportivo.Entity.EntityGrupo;
using web_api_deportivo.IRepository;
using Microsoft.EntityFrameworkCore;

namespace web_api_deportivo.Repository
{
    public class InfraestructuraRepository : IInfraestructuraRepository
    {
        private readonly AppDbContext _db;

        public InfraestructuraRepository(AppDbContext db)
        {
            _db = db;
        }

        #region Mapper

        private InfraestructuraDto ToDto(EInfraestructura entity)
        {
            return new InfraestructuraDto
            {
                Id = entity.Id,
                NombreEspacio = entity.NombreEspacio,
                UbicacionDetalle = entity.UbicacionDetalle ?? string.Empty,
                Activo = entity.Activo
            };
        }

        #endregion

        #region Get

        public async Task<List<InfraestructuraDto>> GetAllAsync()
        {
            var lista = await _db.Infraestructuras.ToListAsync();
            return lista.Select(ToDto).ToList();
        }

        public async Task<InfraestructuraDto?> GetByIdAsync(int id)
        {
            var entity = await _db.Infraestructuras
                .FirstOrDefaultAsync(i => i.Id == id);

            return entity == null ? null : ToDto(entity);
        }

        #endregion

        #region Create

        public async Task<InfraestructuraDto> CreateAsync(InfraestructuraDto dto)
        {
            var entity = new EInfraestructura
            {
                NombreEspacio = dto.NombreEspacio,
                UbicacionDetalle = dto.UbicacionDetalle,
                Activo = dto.Activo
            };

            _db.Infraestructuras.Add(entity);
            await _db.SaveChangesAsync();

            return ToDto(entity);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(int id, InfraestructuraDto dto)
        {
            var entity = await _db.Infraestructuras.FindAsync(id);

            if (entity == null)
                return false;

            entity.NombreEspacio = dto.NombreEspacio;
            entity.UbicacionDetalle = dto.UbicacionDetalle;
            entity.Activo = dto.Activo;

            await _db.SaveChangesAsync();

            return true;
        }

        #endregion

        #region Delete

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Infraestructuras.FindAsync(id);

            if (entity == null)
                return false;

            _db.Infraestructuras.Remove(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
