<<<<<<< HEAD
﻿namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPaymentRepository
    {
        Task<bool> UpdatePaymentStatusAsync(string paymentId, string status, DateTime transactionTime);
=======
﻿using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetById(string id);
        Task<IEnumerable<Payment>> GetAll();
        Task<bool> Update(string id, Payment payment);
>>>>>>> f14bdc945562b2f8cf05f0b9b6217d8db97fc0b6
    }
}
