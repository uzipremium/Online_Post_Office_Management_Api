using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMongoCollection<Payment> _payments;

        public PaymentRepository(IMongoDatabase database)
        {
            _payments = database.GetCollection<Payment>("Payments");
        }

        public async Task<Payment> GetById(string id)
        {
            return await _payments.Find(payment => payment.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            return await _payments.Find(FilterDefinition<Payment>.Empty).ToListAsync();
        }

        public async Task<bool> Update(string id, Payment payment)
        {
            var result = await _payments.ReplaceOneAsync(p => p.Id == id, payment);
            return result.ModifiedCount > 0;
        }
    }
}
