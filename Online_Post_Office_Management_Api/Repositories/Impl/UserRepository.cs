using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<Account> _accounts;
        private readonly IMongoCollection<Role> _roles;

        public UserRepository(IMongoDatabase database)
        {
            _accounts = database.GetCollection<Account>("Accounts");
            _roles = database.GetCollection<Role>("Roles");
        }

        public async Task<(Account, Role)> GetByUsernameAndPassword(string Username, string Password)
        {
            var account = await _accounts.Find(a => a.Username == Username && a.Password == Password).FirstOrDefaultAsync();

            Role role = null;

            if (account != null && !string.IsNullOrEmpty(account.RoleId))
            {
                role = await _roles.Find(r => r.Id == account.RoleId).FirstOrDefaultAsync();
            }

            return (account, role);
        }

        public async Task<(Account account, Role role)> GetByUsername(string username)
        {
            var account = await _accounts.Find(a => a.Username == username).FirstOrDefaultAsync();

            if (account == null)
            {
                return (null, null);
            }

            var role = await _roles.Find(r => r.Id == account.RoleId).FirstOrDefaultAsync();

            return (account, role);
        }
    }
}
