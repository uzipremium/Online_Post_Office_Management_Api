using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.CustomerQueries;
using Online_Post_Office_Management_Api.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.CustomerHandlers
{
    public class GetPricingAndPinCodesByServiceQueryHandler : IRequestHandler<GetPricingAndPinCodesByServiceQuery, (Service, List<Office>)>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IOfficeRepository _officeRepository;

        public GetPricingAndPinCodesByServiceQueryHandler(IServiceRepository serviceRepository, IOfficeRepository officeRepository)
        {
            _serviceRepository = serviceRepository;
            _officeRepository = officeRepository;
        }

        public async Task<(Service, List<Office>)> Handle(GetPricingAndPinCodesByServiceQuery request, CancellationToken cancellationToken)
        {
            // Lấy thông tin dịch vụ từ repository dựa trên ServiceId
            var service = await _serviceRepository.GetById(request.ServiceId);
            if (service == null)
            {
                return (null, null);
            }

            // Lấy danh sách các văn phòng (bao gồm mã bưu điện) dựa trên OfficeId nếu được cung cấp
            List<Office> offices;
            if (string.IsNullOrEmpty(request.OfficeId))
            {
                // Nếu không cung cấp OfficeId, lấy tất cả các văn phòng
                offices = (await _officeRepository.GetAll()).ToList();
            }
            else
            {
                // Nếu cung cấp OfficeId, lấy văn phòng cụ thể
                var office = await _officeRepository.GetById(request.OfficeId);
                offices = office != null ? new List<Office> { office } : new List<Office>();
            }

            return (service, offices);
        }

        
    }
}
