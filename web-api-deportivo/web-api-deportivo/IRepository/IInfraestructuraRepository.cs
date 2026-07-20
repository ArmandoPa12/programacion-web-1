using web_api_deportivo.Dto.DtoGrupo;

namespace web_api_deportivo.IRepository
{
    public interface IInfraestructuraRepository
    {
        Task<List<InfraestructuraDto>> GetAllAsync();

        Task<InfraestructuraDto?> GetByIdAsync(int id);

        Task<InfraestructuraDto> CreateAsync(InfraestructuraDto dto);

        Task<bool> UpdateAsync(int id, InfraestructuraDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
