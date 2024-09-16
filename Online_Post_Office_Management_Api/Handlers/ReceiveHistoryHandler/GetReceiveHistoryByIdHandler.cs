using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.ReceiveHistoryQuery;
using Online_Post_Office_Management_Api.Repositories.IRepository;

public class GetReceiveHistoryByIdHandler : IRequestHandler<GetOneReceiveHistory, ReceiveHistory>
{
    private readonly IReceiveHistoryRepository _repository;

    public GetReceiveHistoryByIdHandler(IReceiveHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ReceiveHistory> Handle(GetOneReceiveHistory request, CancellationToken cancellationToken)
    {
        return await _repository.GetById(request.Id);
    }
}