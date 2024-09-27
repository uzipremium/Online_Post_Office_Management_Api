using Online_Post_Office_Management_Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetById(string id);

        // Điều chỉnh phương thức GetAll để các tham số không bắt buộc
        Task<IEnumerable<Payment>> GetAll(int? pageNumber = null, int? pageSize = null, string paymentStatus = null, DateTime? startDate = null);

        Task<bool> Update(string id, Payment payment);
        Task<bool> Create(Payment payment);
    }
}
