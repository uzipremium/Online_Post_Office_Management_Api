using MediatR;
using Microsoft.Extensions.Logging;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Queries.OfficeQuery;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.OfficeHandler
{
    public class GetOneOfficeHandler : IRequestHandler<OfficeGetOne, Office>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly ILogger<GetOneOfficeHandler> _logger;

        public GetOneOfficeHandler(IOfficeRepository officeRepository, ILogger<GetOneOfficeHandler> logger)
        {
            _officeRepository = officeRepository;
            _logger = logger;
        }

        public async Task<Office> Handle(OfficeGetOne request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                _logger.LogWarning("Attempted to get office with null or empty ID.");
                throw new ArgumentException("Office ID cannot be null or empty.", nameof(request.Id));
            }

            var office = await _officeRepository.GetById(request.Id);

            if (office == null)
            {
                _logger.LogWarning($"Office with ID {request.Id} not found.");
                throw new KeyNotFoundException($"Office with ID {request.Id} was not found.");
            }

            return office;
        }
    }
}
