
using web_api_deportivo.Entity.EntityRoles;

namespace web_api_deportivo.IRepository
{
    public interface IPermisosRepository
    {
        Task<IEnumerable<EPermisos>> GetAllAsync();
    }
}
