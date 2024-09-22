using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerRepository(IMongoDatabase database)
        {
            _customers = database.GetCollection<Customer>("Customers");
        }

        public async Task Create(Customer customer)
        {
            await _customers.InsertOneAsync(customer);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _customers.Find(_ => true).ToListAsync();
        }

        public async Task<Customer> FindByPhone(string phone)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Phone, phone);
            return await _customers.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<Customer> FindById(string id)
        {
            return await _customers.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

     
    }
}
