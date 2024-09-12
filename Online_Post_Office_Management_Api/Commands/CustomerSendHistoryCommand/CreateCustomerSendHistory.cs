using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Commands.CustomerSendHistoryCommand
{
    public class CreateCustomerSendHistory : IRequest<CustomerSendHistory>
    {
        public string ReceiveId { get; set; }
        public string CustomerId { get; set; }
        public ReceiveHistory ReceiveHistory { get; set; }
        public Customer Customer { get; set; }

        public CreateCustomerSendHistory(string receiveId, string customerId, ReceiveHistory receiveHistory, Customer customer)
        {
            ReceiveId = receiveId;
            CustomerId = customerId;
            ReceiveHistory = receiveHistory;
            Customer = customer;
        }
    }
}
