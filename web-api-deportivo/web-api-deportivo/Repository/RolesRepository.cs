using Microsoft.EntityFrameworkCore;
using System.Data;
using web_api_deportivo.Conection;
using web_api_deportivo.Dto;
using web_api_deportivo.Entity;
using web_api_deportivo.IRepository;

namespace web_api_deportivo.Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly AppDbContext _db;
        public RolesRepository(AppDbContext db) {
            _db = db; 
        }

        public async Task<IEnumerable<ERoles>> GetAllAsync()
        {
            return await _db.Roles.ToListAsync();   
            //return await _db.Roles.Include(r => r.Permiso);
        }
    }
}
