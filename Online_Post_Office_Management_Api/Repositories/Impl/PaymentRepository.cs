﻿using MongoDB.Driver;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Repositories.Impl
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMongoCollection<Payment> _paymentCollection;

        public PaymentRepository(IMongoDatabase database)
        {
            _paymentCollection = database.GetCollection<Payment>("Payments");
        }

        public async Task<Payment> GetById(string id)
        {
            return await _paymentCollection.Find(payment => payment.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Payment>> GetAll(int pageNumber, int pageSize, string paymentStatus, DateTime? startDate)
        {
            // Xây dựng filter
            var filterBuilder = Builders<Payment>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(paymentStatus))
            {
                filter &= filterBuilder.Eq(p => p.Status, paymentStatus);
            }

            if (startDate.HasValue)
            {
                filter &= filterBuilder.Gte(p => p.TransactionTime, startDate.Value);
            }

            // Tính toán phân trang
            return await _paymentCollection
                .Find(filter)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<bool> Update(string id, Payment payment)
        {
            var result = await _paymentCollection.ReplaceOneAsync(p => p.Id == id, payment);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> Create(Payment payment)
        {
            await _paymentCollection.InsertOneAsync(payment);
            return true;
        }
    }
}
