using MediatR;
using MongoDB.Bson;
using Online_Post_Office_Management_Api.Commands.PackageCommand;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PackageHandler
{
    public class CreatePackage_Payment_Description_DeliveryHandler : IRequestHandler<CreatePackage_Payment_Description_Delivery, Package>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IDescriptionRepository _descriptionRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ICustomerSendHistoryRepository _customerSendHistoryRepository;
        private readonly IOfficeSendHistoryRepository _officeSendHistoryRepository;

        public CreatePackage_Payment_Description_DeliveryHandler(
            IPackageRepository packageRepository,
            IPaymentRepository paymentRepository,
            IDescriptionRepository descriptionRepository,
            IDeliveryRepository deliveryRepository,
            ICustomerSendHistoryRepository customerSendHistoryRepository,
            IOfficeSendHistoryRepository officeSendHistoryRepository)
        {
            _packageRepository = packageRepository;
            _paymentRepository = paymentRepository;
            _descriptionRepository = descriptionRepository;
            _deliveryRepository = deliveryRepository;
            _customerSendHistoryRepository = customerSendHistoryRepository;
            _officeSendHistoryRepository = officeSendHistoryRepository;
        }

        public async Task<Package> Handle(CreatePackage_Payment_Description_Delivery request, CancellationToken cancellationToken)
        {
            var package = request.Package;
            var payment = request.Payment;
            var description = request.Description;
            var delivery = request.Delivery;

            // Tạo ID cho từng entity nếu chưa được cung cấp
            package.Id = ObjectId.GenerateNewId().ToString();
            payment.Id = ObjectId.GenerateNewId().ToString();
            description.Id = ObjectId.GenerateNewId().ToString();
            delivery.Id = ObjectId.GenerateNewId().ToString();

            // Lưu Payment, Description, và Delivery
            await _paymentRepository.Create(payment);
            await _descriptionRepository.Create(description);
            await _deliveryRepository.Create(delivery);

            // Liên kết ID đến Package
            package.PaymentId = payment.Id;
            package.DescriptionId = description.Id;
            package.DeliveryId = delivery.Id;

            // Lưu Package
            await _packageRepository.Create(package);

            // Tạo CustomerSendHistory nếu SenderId không null
            if (!string.IsNullOrEmpty(package.SenderId))
            {
                var customerSendHistory = new CustomerSendHistory
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    ReceiveId = package.Id,
                    CustomerId = package.SenderId
                };
                await _customerSendHistoryRepository.Create(customerSendHistory);
            }

            // Tạo OfficeSendHistory nếu OfficeId không null
            if (!string.IsNullOrEmpty(package.OfficeId))
            {
                var officeSendHistory = new OfficeSendHistory
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    ReceiveId = package.Id,
                    OfficeId = package.OfficeId
                };
                await _officeSendHistoryRepository.Create(officeSendHistory);
            }

            return package;
        }
    }
}
