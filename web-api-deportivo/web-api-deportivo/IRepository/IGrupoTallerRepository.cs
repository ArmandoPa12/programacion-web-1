using web_api_deportivo.Dto.DtoGrupo;

namespace web_api_deportivo.IRepository
{
    public interface IGrupoTallerRepository
    {
        Task<List<GrupoTallerDto>> GetAllAsync();

        Task<GrupoTallerDto?> GetByIdAsync(int id);

        Task CreateAsync(GrupoTallerDto dto);

        Task UpdateAsync(int id, GrupoTallerDto dto);

        Task<Boolean> DeleteAsync(int id);
    }
}
