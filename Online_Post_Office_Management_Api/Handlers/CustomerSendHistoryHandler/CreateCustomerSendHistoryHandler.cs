using MediatR;
using Online_Post_Office_Management_Api.Commands.CustomerSendHistoryCommand;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.CustomerSendHistoryHandler
{
    public class CreateCustomerSendHistoryHandler : IRequestHandler<CreateCustomerSendHistory, CustomerSendHistory>
    {
        private readonly ICustomerSendHistoryRepository _customerSendHistoryRepository;

        public CreateCustomerSendHistoryHandler(ICustomerSendHistoryRepository customerSendHistoryRepository)
        {
            _customerSendHistoryRepository = customerSendHistoryRepository;
        }

        public async Task<CustomerSendHistory> Handle(CreateCustomerSendHistory request, CancellationToken cancellationToken)
        {
            var customerSendHistory = new CustomerSendHistory
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                ReceiveId = request.ReceiveId,
                CustomerId = request.CustomerId,
            };

            await _customerSendHistoryRepository.Create(customerSendHistory);

            return customerSendHistory;
        }
    }
}
