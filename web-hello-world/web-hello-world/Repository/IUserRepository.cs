using web_hello_world.Dto;

namespace web_hello_world.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAllAssync();
        Task<Users> CreateAsync(Users user);
        Task<int> GetCountAsync();
    }
}
