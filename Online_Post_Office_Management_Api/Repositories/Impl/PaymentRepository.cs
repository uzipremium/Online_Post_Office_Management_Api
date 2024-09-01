
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMongoCollection<Payment> _payments;

        public PaymentRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("qwer");
            _payments = database.GetCollection<Payment>("Payment");
        }

        public async Task<bool> UpdatePaymentStatusAsync(string paymentId, string status, DateTime transactionTime)
        {
            var update = Builders<Payment>.Update
                .Set(p => p.Status, status)
                .Set(p => p.TransactionTime, transactionTime);

            var result = await _payments.UpdateOneAsync(p => p.Id == paymentId, update);
            return result.ModifiedCount > 0;
        }
    }
}
