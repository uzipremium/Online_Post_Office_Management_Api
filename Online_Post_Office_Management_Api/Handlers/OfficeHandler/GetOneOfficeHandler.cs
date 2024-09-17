using MediatR;
using Online_Post_Office_Management_Api.Models;
using System.Threading;
using System.Threading.Tasks;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Queries.OfficeQuery;

namespace Online_Post_Office_Management_Api.Handlers.OfficeHandler
{
    public class GetOneOfficeHandler : IRequestHandler<OfficeGetOne, Office>
    {
        private readonly IOfficeRepository _officeRepository;

        public GetOneOfficeHandler(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public async Task<Office> Handle(OfficeGetOne request, CancellationToken cancellationToken)
        {
            return await _officeRepository.GetById(request.Id);
        }
    }
}
