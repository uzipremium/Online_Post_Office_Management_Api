using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IUserRepository
    {
        Task<(Account, Role)> GetByUsernameAndPassword(string Username, string Password);
        Task<(Account account, Role role)> GetByUsername(string username);
    } 
}
