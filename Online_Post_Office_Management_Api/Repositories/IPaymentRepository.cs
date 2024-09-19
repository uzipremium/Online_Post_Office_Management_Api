using Online_Post_Office_Management_Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetById(string id);

   
        Task<IEnumerable<Payment>> GetAll(int pageNumber, int pageSize, string paymentStatus, DateTime? startDate);

        Task<bool> Update(string id, Payment payment);
        Task<bool> Create(Payment payment);
    }
}
