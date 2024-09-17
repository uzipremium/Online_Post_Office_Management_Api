using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.OfficeSendHistoryQuery;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.OfficeSendHistoryHandler
{
    public class GetOfficeSendHistoryByIdHandler : IRequestHandler<GetOneOfficeSendHistory, OfficeSendHistory>
    {
        private readonly IOfficeSendHistoryRepository _officeSendHistoryRepository;

        public GetOfficeSendHistoryByIdHandler(IOfficeSendHistoryRepository officeSendHistoryRepository)
        {
            _officeSendHistoryRepository = officeSendHistoryRepository;
        }

        public async Task<OfficeSendHistory> Handle(GetOneOfficeSendHistory request, CancellationToken cancellationToken)
        {
            return await _officeSendHistoryRepository.GetById(request.Id);
        }
    }
}
