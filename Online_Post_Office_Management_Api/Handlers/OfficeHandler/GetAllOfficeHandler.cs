using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.OfficeQuery;
using Online_Post_Office_Management_Api.Repositories;

namespace Online_Post_Office_Management_Api.Handlers.OfficeHandler
{
    public class GetAllOfficeHandler : IRequestHandler<OfficeGetAll, List<Office>>
    {
        private readonly IOfficeRepository _officeRepository;

        public GetAllOfficeHandler(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public async Task<List<Office>> Handle(OfficeGetAll request, CancellationToken cancellationToken)
        {
            var offices = await _officeRepository.GetAll();
            return offices.ToList();
        }
    }
}
