

using web_api_deportivo.Entity;

namespace web_api_deportivo.IRepository
{
    public interface IRolesRepository
    {
        Task<IEnumerable<ERoles>> GetAllAsync();

    }
}
