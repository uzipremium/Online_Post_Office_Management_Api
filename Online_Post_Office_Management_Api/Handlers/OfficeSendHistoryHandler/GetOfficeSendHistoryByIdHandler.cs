using MediatR;
using Microsoft.Extensions.Logging;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.OfficeSendHistoryQuery;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.OfficeSendHistoryHandler
{
    public class GetOfficeSendHistoryByIdHandler : IRequestHandler<GetOneOfficeSendHistory, OfficeSendHistory>
    {
        private readonly IOfficeSendHistoryRepository _officeSendHistoryRepository;
        private readonly ILogger<GetOfficeSendHistoryByIdHandler> _logger;

        public GetOfficeSendHistoryByIdHandler(IOfficeSendHistoryRepository officeSendHistoryRepository, ILogger<GetOfficeSendHistoryByIdHandler> logger)
        {
            _officeSendHistoryRepository = officeSendHistoryRepository;
            _logger = logger;
        }

        public async Task<OfficeSendHistory> Handle(GetOneOfficeSendHistory request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Retrieving OfficeSendHistory with ID: {request.Id}");

            try
            {
                var history = await _officeSendHistoryRepository.GetById(request.Id);

                if (history == null)
                {
                    _logger.LogWarning($"OfficeSendHistory with ID: {request.Id} not found.");
                }
                else
                {
                    _logger.LogInformation($"Successfully retrieved OfficeSendHistory with ID: {request.Id}.");
                }

                return history;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving OfficeSendHistory.");
                return null; 
            }
        }
    }
}
