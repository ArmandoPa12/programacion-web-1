using web_api_deportivo.Dto.DtoRoles;

namespace web_api_deportivo.IRepository
{
    public interface IRolesRepository
    {
        Task<IEnumerable<RolDto>> GetAllAsync();

    }
}
