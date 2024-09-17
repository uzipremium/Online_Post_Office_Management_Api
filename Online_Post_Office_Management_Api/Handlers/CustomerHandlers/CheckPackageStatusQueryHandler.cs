using MediatR;
using Online_Post_Office_Management_Api.DTO.Response;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.CustomerQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.CustomerHandlers
{
    public class CheckPackageStatusQueryHandler : IRequestHandler<CheckPackageStatusQuery, PackageResponse>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly ICustomerPackageRepository _customerPackageRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IDescriptionRepository _descriptionRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public CheckPackageStatusQueryHandler(
            IPackageRepository packageRepository,
            ICustomerPackageRepository customerPackageRepository,
            IOfficeRepository officeRepository,
            IServiceRepository serviceRepository,
            IDescriptionRepository descriptionRepository,
            IPaymentRepository paymentRepository,
            IDeliveryRepository deliveryRepository)
        {
            _packageRepository = packageRepository;
            _customerPackageRepository = customerPackageRepository;
            _officeRepository = officeRepository;
            _serviceRepository = serviceRepository;
            _descriptionRepository = descriptionRepository;
            _paymentRepository = paymentRepository;
            _deliveryRepository = deliveryRepository;
        }

        public async Task<PackageResponse> Handle(CheckPackageStatusQuery request, CancellationToken cancellationToken)
        {
            var package = await _packageRepository.GetById(request.PackageId);

            if (package == null)
            {
                return null; // Package doesn't exist
            }

            var sender = await _customerPackageRepository.GetById(package.SenderId.ToString());
            var office = await _officeRepository.GetById(package.OfficeId);
            var service = await _serviceRepository.GetById(package.ServiceId);

            // Lấy tên hoặc thông tin mô tả từ các thực thể
            var description = await _descriptionRepository.GetById(package.DescriptionId);
            var paymentStatus = await _paymentRepository.GetById(package.PaymentId);
            var deliveryStatus = await _deliveryRepository.GetById(package.DeliveryId);

            // Tạo đối tượng PackageResponse với các tên thay vì ID
            var packageResponse = new PackageResponse
            {
                Id = package.Id,
                SenderName = sender?.Name ?? "Unknown Sender",
                OfficeName = office?.OfficeName ?? "Unknown Office",
                ServiceName = service?.Name ?? "Unknown Service",
                Weight = package.Weight,
                Distance = package.Distance,
                DeliveryNumber = package.DeliveryNumber,
                Description = description?.DescriptionText ?? "No Description",
                PaymentStatus = paymentStatus?.Status ?? "No Payment Status",
                DeliveryStatus = deliveryStatus?.DeliveryStatus ?? "No Delivery Status",
                Receiver = package.Receiver ?? "Unknown Receiver",
                CreatedAt = package.CreatedAt
            };

            return packageResponse;
        }
    }
}
