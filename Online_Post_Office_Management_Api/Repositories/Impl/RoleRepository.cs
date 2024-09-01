using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IMongoCollection<Role> _roles;

        public RoleRepository(IMongoDatabase database)
        {
            _roles = database.GetCollection<Role>("Roles");
        }

        public async Task<Role> GetById(string id)
        {
            return await _roles.Find(role => role.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _roles.Find(FilterDefinition<Role>.Empty).ToListAsync();
        }
    }
}
