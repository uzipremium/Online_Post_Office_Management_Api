using MediatR;
using MongoDB.Bson;
using Online_Post_Office_Management_Api.Commands.OfficeSendHistoryCommand;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.OfficeSendHistoryHandler
{
    public class CreateOfficeSendHistoryHandler : IRequestHandler<CreateOfficeSendHistory, bool>
    {
        private readonly IOfficeSendHistoryRepository _officeSendHistoryRepository;

        public CreateOfficeSendHistoryHandler(IOfficeSendHistoryRepository officeSendHistoryRepository)
        {
            _officeSendHistoryRepository = officeSendHistoryRepository;
        }

        public async Task<bool> Handle(CreateOfficeSendHistory request, CancellationToken cancellationToken)
        {
            var newHistory = new OfficeSendHistory
            {
                Id = ObjectId.GenerateNewId().ToString(),
                ReceiveId = request.ReceiveId,
                OfficeId = request.OfficeId,
            };

            await _officeSendHistoryRepository.Create(newHistory);

            return true; 
        }
    }
}
