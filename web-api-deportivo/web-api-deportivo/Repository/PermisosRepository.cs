using web_api_deportivo.Conection;
using web_api_deportivo.Entity.EntityRoles;
using web_api_deportivo.IRepository;
using Microsoft.EntityFrameworkCore;

namespace web_api_deportivo.Repository
{
    public class PermisosRepository : IPermisosRepository
    {
        private readonly AppDbContext _db ;
        public PermisosRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<EPermisos>> GetAllAsync()
        {
            return await _db.Permisos.ToListAsync();
        }
    }
}
