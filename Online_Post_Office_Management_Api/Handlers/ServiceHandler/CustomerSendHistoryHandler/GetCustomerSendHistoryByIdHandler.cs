using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.CustomerSendHistoryQuery;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.ServiceHandler.CustomerSendHistoryHandler
{
    public class GetCustomerSendHistoryByIdHandler : IRequestHandler<GetOneCustomerSendHistory, CustomerSendHistory>
    {
        private readonly ICustomerSendHistoryRepository _customerSendHistoryRepository;

        public GetCustomerSendHistoryByIdHandler(ICustomerSendHistoryRepository customerSendHistoryRepository)
        {
            _customerSendHistoryRepository = customerSendHistoryRepository;
        }

        public async Task<CustomerSendHistory> Handle(GetOneCustomerSendHistory request, CancellationToken cancellationToken)
        {
            return await _customerSendHistoryRepository.GetById(request.Id);
        }
    }
}
