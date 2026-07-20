using web_api_deportivo.Dto.DtoUsuario;

namespace web_api_deportivo.IRepository
{
    public interface IAlumnoRepository
    {
        Task<List<AlumnoDto>> GetAllAsync();

        Task<AlumnoDto?> GetByIdAsync(int id);

        Task<AlumnoDto> CreateAsync(AlumnoDto dto);

        Task<bool> UpdateAsync(int id, AlumnoDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
