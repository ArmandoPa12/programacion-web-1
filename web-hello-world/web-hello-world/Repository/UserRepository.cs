using web_hello_world.Dto;
using web_hello_world.Conection;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace web_hello_world.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Users> CreateAsync(Users user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<Users>> GetAllAssync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            string query = "SELECT COUNT(*) FROM users";
            var connection = _db.Database.GetDbConnection();
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result); 
            }
        }
    }
}
