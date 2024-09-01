using MediatR;

namespace Online_Post_Office_Management_Api.Commands.PaymentCommand
{
    public class UpdatePayment : IRequest<bool>
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public DateTime TransactionTime { get; set; }
        public decimal Cost { get; set; }

        public UpdatePayment(string id, string status, DateTime transactionTime, decimal cost)
        {
            Id = id;
            Status = status;
            TransactionTime = transactionTime;
            Cost = cost;
        }
    }
}
