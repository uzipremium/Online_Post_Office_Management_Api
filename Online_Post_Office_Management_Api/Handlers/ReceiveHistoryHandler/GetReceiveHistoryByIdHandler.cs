using MediatR;
using Microsoft.Extensions.Logging;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.ReceiveHistoryQuery;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Threading;
using System.Threading.Tasks;

public class GetReceiveHistoryByIdHandler : IRequestHandler<GetOneReceiveHistory, ReceiveHistory>
{
    private readonly IReceiveHistoryRepository _repository;
    private readonly ILogger<GetReceiveHistoryByIdHandler> _logger;

    public GetReceiveHistoryByIdHandler(IReceiveHistoryRepository repository, ILogger<GetReceiveHistoryByIdHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ReceiveHistory> Handle(GetOneReceiveHistory request, CancellationToken cancellationToken)
    {
        try
        {
            var receiveHistory = await _repository.GetById(request.Id);
            if (receiveHistory == null)
            {
                _logger.LogWarning($"Receive history with ID {request.Id} not found.");
            }
            return receiveHistory;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving receive history with ID {request.Id}.");
            return null; 
        }
    }
}
