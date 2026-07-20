using web_api_deportivo.Dto.DtoGrupo;

namespace web_api_deportivo.IRepository
{
    public interface ITallerRepository
    {
        Task<List<TallerDto>> GetAllAsync();

        Task<TallerDto?> GetByIdAsync(int id);

        Task<TallerDto> CreateAsync(TallerDto dto);

        Task<bool> UpdateAsync(int id, TallerDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
