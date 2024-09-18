using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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

            // Check and handle ObjectId fields if they are empty strings or invalid
            package.Id = string.IsNullOrEmpty(package.Id) ? ObjectId.GenerateNewId().ToString() : package.Id;
            payment.Id = string.IsNullOrEmpty(payment.Id) ? ObjectId.GenerateNewId().ToString() : payment.Id;
            description.Id = string.IsNullOrEmpty(description.Id) ? ObjectId.GenerateNewId().ToString() : description.Id;
            delivery.Id = string.IsNullOrEmpty(delivery.Id) ? ObjectId.GenerateNewId().ToString() : delivery.Id;

            // If SenderId or OfficeId is an empty string, assign null
            package.SenderId = string.IsNullOrEmpty(package.SenderId) ? null : package.SenderId;
            package.OfficeId = string.IsNullOrEmpty(package.OfficeId) ? null : package.OfficeId;

            // Save Payment, Description, and Delivery in parallel
            await Task.WhenAll(
                _paymentRepository.Create(payment),
                _descriptionRepository.Create(description),
                _deliveryRepository.Create(delivery)
            );

            // Link IDs to Package
            package.PaymentId = payment.Id;
            package.DescriptionId = description.Id;
            package.DeliveryId = delivery.Id;

            // Save Package
            await _packageRepository.Create(package);

            // Create CustomerSendHistory if SenderId is valid
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

            // Create OfficeSendHistory if OfficeId is valid
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