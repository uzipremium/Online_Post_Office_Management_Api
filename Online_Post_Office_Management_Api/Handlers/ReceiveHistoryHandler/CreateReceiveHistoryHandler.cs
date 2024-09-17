using MediatR;
using Online_Post_Office_Management_Api.Commands.ReceiveHistoryCommand;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Repositories.IRepository;

public class CreateReceiveHistoryHandler : IRequestHandler<CreateReceiveHistory, bool>
{
    private readonly IReceiveHistoryRepository _repository;

    public CreateReceiveHistoryHandler(IReceiveHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CreateReceiveHistory request, CancellationToken cancellationToken)
    {
        await _repository.Create(request.ReceiveHistory);
        return true;
    }
}