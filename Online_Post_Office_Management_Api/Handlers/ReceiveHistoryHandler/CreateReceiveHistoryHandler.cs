using MediatR;
using Microsoft.Extensions.Logging;
using Online_Post_Office_Management_Api.Commands.ReceiveHistoryCommand;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Threading;
using System.Threading.Tasks;

public class CreateReceiveHistoryHandler : IRequestHandler<CreateReceiveHistory, bool>
{
    private readonly IReceiveHistoryRepository _repository;
    private readonly ILogger<CreateReceiveHistoryHandler> _logger;

    public CreateReceiveHistoryHandler(IReceiveHistoryRepository repository, ILogger<CreateReceiveHistoryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateReceiveHistory request, CancellationToken cancellationToken)
    {
        if (request.ReceiveHistory == null)
        {
            _logger.LogWarning("Received null ReceiveHistory object.");
            return false;
        }

        try
        {
            await _repository.Create(request.ReceiveHistory);
            _logger.LogInformation($"Successfully created receive history for ID: {request.ReceiveHistory.Id}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating receive history.");
            return false;
        }
    }
}
