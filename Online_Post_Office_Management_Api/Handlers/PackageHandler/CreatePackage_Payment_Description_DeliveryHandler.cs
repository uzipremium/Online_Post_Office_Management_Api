using MediatR;
using MongoDB.Bson;
using Online_Post_Office_Management_Api.Commands.PackageCommand;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories;

namespace Online_Post_Office_Management_Api.Handlers.PackageHandler
{
    public class CreatePackage_Payment_Description_DeliveryHandler : IRequestHandler<CreatePackage_Payment_Description_Delivery, Package>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IDescriptionRepository _descriptionRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public CreatePackage_Payment_Description_DeliveryHandler(
            IPackageRepository packageRepository,
            IPaymentRepository paymentRepository,
            IDescriptionRepository descriptionRepository,
            IDeliveryRepository deliveryRepository)
        {
            _packageRepository = packageRepository;
            _paymentRepository = paymentRepository;
            _descriptionRepository = descriptionRepository;
            _deliveryRepository = deliveryRepository;
        }

        public async Task<Package> Handle(CreatePackage_Payment_Description_Delivery request, CancellationToken cancellationToken)
        {
            var package = request.Package;
            var payment = request.Payment;
            var description = request.Description;
            var delivery = request.Delivery;

            // Generate IDs for each entity if not provided
            package.Id = ObjectId.GenerateNewId().ToString();
            payment.Id = ObjectId.GenerateNewId().ToString();
            description.Id = ObjectId.GenerateNewId().ToString();
            delivery.Id = ObjectId.GenerateNewId().ToString();

            // Save Payment, Description, and Delivery
            await _paymentRepository.Create(payment);
            await _descriptionRepository.Create(description);
            await _deliveryRepository.Create(delivery);

            // Link IDs to Package
            package.PaymentId = payment.Id;
            package.DescriptionId = description.Id;
            package.DeliveryId = delivery.Id;

            // Save the Package
            await _packageRepository.Create(package);

            return package;
        }
    }
}
