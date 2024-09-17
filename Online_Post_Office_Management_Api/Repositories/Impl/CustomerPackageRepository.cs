using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class CustomerPackageRepository : ICustomerPackageRepository
    {
        private readonly IMongoCollection<Package> _packages;
        private readonly IMongoCollection<Customer> _customers;

        public CustomerPackageRepository(IMongoDatabase database)
        {
            _packages = database.GetCollection<Package>("Packages");
            _customers = database.GetCollection<Customer>("Customers");
        }

        public async Task<Package> GetPackageByIdAsync(string packageId)
        {
            return await _packages.Find(package => package.Id == packageId).FirstOrDefaultAsync();
        }

        public async Task<Customer> GetById(string customerId)
        {
            return await _customers.Find(customer => customer.Id == customerId).FirstOrDefaultAsync();
        }
    }
}
