using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.PaymentQuery
{
    public class PaymentGetOne : IRequest<Payment>
    {
        public string Id { get; set; }

        public PaymentGetOne(string id)
        {
            Id = id;
        }
    }
}
