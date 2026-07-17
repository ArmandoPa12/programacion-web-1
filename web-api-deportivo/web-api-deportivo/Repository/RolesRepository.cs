using Microsoft.EntityFrameworkCore;
using System.Data;
using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoRoles;
using web_api_deportivo.IRepository;

namespace web_api_deportivo.Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly AppDbContext _db;
        public RolesRepository(AppDbContext db) {
            _db = db; 
        }

        public async Task<IEnumerable<RolDto>> GetAllAsync()
        {
            return await _db.Roles
                .Include(r => r.Permisos)
                .Select(r => new RolDto
                {
                    Id = r.Id,
                    NombreRol = r.NombreRol,
                    Descripcion = r.Descripcion,
                    PermisosIds = null,
                    Permisos = r.Permisos.Select(p => new PermisoDetalleDto
                    {
                        Id = p.Id,
                        NombrePermiso = p.NombrePermiso,
                        Descripcion = p.Descripcion
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
