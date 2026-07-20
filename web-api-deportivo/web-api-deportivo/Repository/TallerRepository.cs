using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoGrupo;
using web_api_deportivo.Entity.EntityGrupo;
using web_api_deportivo.IRepository;
using Microsoft.EntityFrameworkCore;

namespace web_api_deportivo.Repository
{
    public class TallerRepository : ITallerRepository
    {
        private readonly AppDbContext _db;

        public TallerRepository(AppDbContext db)
        {
            _db = db;
        }

        #region Mapper

        private TallerDto ToDto(ETaller entity)
        {
            return new TallerDto
            {
                Id = entity.Id,
                NombreTaller = entity.NombreTaller,
                CategoriaEdad = entity.CategoriaEdad ?? string.Empty,
                EdadMinima = entity.EdadMinima,
                EdadMaxima = entity.EdadMaxima,
                Activo = entity.Activo
            };
        }

        #endregion

        #region Get

        public async Task<List<TallerDto>> GetAllAsync()
        {
            var lista = await _db.Talleres.ToListAsync();
            
            return lista.Select(ToDto).ToList();
        }

        public async Task<TallerDto?> GetByIdAsync(int id)
        {
            var entity = await _db.Talleres
                .FirstOrDefaultAsync(t => t.Id == id);

            return entity == null ? null : ToDto(entity);
        }

        #endregion

        #region Create

        public async Task<TallerDto> CreateAsync(TallerDto dto)
        {
            var entity = new ETaller
            {
                NombreTaller = dto.NombreTaller,
                CategoriaEdad = dto.CategoriaEdad,
                EdadMinima = dto.EdadMinima,
                EdadMaxima = dto.EdadMaxima,
                Activo = dto.Activo
            };

            _db.Talleres.Add(entity);
            await _db.SaveChangesAsync();

            return ToDto(entity);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(int id, TallerDto dto)
        {
            var entity = await _db.Talleres.FindAsync(id);

            if (entity == null)
                return false;

            entity.NombreTaller = dto.NombreTaller;
            entity.CategoriaEdad = dto.CategoriaEdad;
            entity.EdadMinima = dto.EdadMinima;
            entity.EdadMaxima = dto.EdadMaxima;
            entity.Activo = dto.Activo;

            await _db.SaveChangesAsync();

            return true;
        }

        #endregion

        #region Delete

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Talleres.FindAsync(id);

            if (entity == null)
                return false;

            _db.Talleres.Remove(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        #endregion
    }

}
