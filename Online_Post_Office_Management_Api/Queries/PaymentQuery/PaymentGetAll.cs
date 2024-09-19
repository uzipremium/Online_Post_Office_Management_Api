using MediatR;
using Online_Post_Office_Management_Api.Models;
using System;

namespace Online_Post_Office_Management_Api.Queries.PaymentQuery
{
    public class PaymentGetAll : IRequest<List<Payment>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string PaymentStatus { get; set; } 
        public DateTime? StartDate { get; set; } 
    }
}
