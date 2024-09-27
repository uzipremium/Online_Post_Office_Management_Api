using MongoDB.Driver;
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

        public async Task<IEnumerable<Payment>> GetAll(int? pageNumber = null, int? pageSize = null, string paymentStatus = null, DateTime? startDate = null)
        {
            // Xây dựng filter mặc định là không có điều kiện
            var filterBuilder = Builders<Payment>.Filter;
            var filter = filterBuilder.Empty;

            // Thêm điều kiện theo trạng thái thanh toán (nếu có)
            if (!string.IsNullOrEmpty(paymentStatus))
            {
                filter &= filterBuilder.Eq(p => p.Status, paymentStatus);
            }

            // Thêm điều kiện theo ngày bắt đầu giao dịch (nếu có)
            if (startDate.HasValue)
            {
                filter &= filterBuilder.Gte(p => p.TransactionTime, startDate.Value);
            }

           
            int page = pageNumber ?? 1; 
            int size = pageSize ?? 10;  
            int skip = (page - 1) * size;

         
            return await _paymentCollection
                .Find(filter)
                .Skip(skip)
                .Limit(size)
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
