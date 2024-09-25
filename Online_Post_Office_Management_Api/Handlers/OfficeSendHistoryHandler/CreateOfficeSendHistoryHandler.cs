using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CreateOfficeSendHistoryHandler> _logger;

        public CreateOfficeSendHistoryHandler(IOfficeSendHistoryRepository officeSendHistoryRepository, ILogger<CreateOfficeSendHistoryHandler> logger)
        {
            _officeSendHistoryRepository = officeSendHistoryRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateOfficeSendHistory request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ReceiveId))
            {
                _logger.LogWarning("Attempted to create OfficeSendHistory with null or empty ReceiveId.");
                throw new ArgumentException("ReceiveId cannot be null or empty.", nameof(request.ReceiveId));
            }

            if (string.IsNullOrEmpty(request.OfficeId))
            {
                _logger.LogWarning("Attempted to create OfficeSendHistory with null or empty OfficeId.");
                throw new ArgumentException("OfficeId cannot be null or empty.", nameof(request.OfficeId));
            }

            var newHistory = new OfficeSendHistory
            {
                Id = ObjectId.GenerateNewId().ToString(),
                ReceiveId = request.ReceiveId,
                OfficeId = request.OfficeId,
            };

            try
            {
                await _officeSendHistoryRepository.Create(newHistory);
                _logger.LogInformation($"Successfully created OfficeSendHistory with ID: {newHistory.Id}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating OfficeSendHistory.");
                return false;
            }
        }
    }
}
