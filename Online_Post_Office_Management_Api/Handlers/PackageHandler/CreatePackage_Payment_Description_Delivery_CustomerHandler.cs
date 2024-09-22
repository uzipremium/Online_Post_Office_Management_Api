using MediatR;
using MongoDB.Bson;
using Online_Post_Office_Management_Api.Commands.PackageCommand;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Repositories.IRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PackageHandler
{
    public class CreatePackage_Payment_Description_Delivery_CustomerHandler : IRequestHandler<CreatePackage_Payment_Description_Delivery_Customer, Package>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IDescriptionRepository _descriptionRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerSendHistoryRepository _customerSendHistoryRepository;
        private readonly IOfficeSendHistoryRepository _officeSendHistoryRepository;

        public CreatePackage_Payment_Description_Delivery_CustomerHandler(
            IPackageRepository packageRepository,
            IPaymentRepository paymentRepository,
            IDescriptionRepository descriptionRepository,
            IDeliveryRepository deliveryRepository,
            ICustomerRepository customerRepository,
            ICustomerSendHistoryRepository customerSendHistoryRepository,
            IOfficeSendHistoryRepository officeSendHistoryRepository)
        {
            _packageRepository = packageRepository;
            _paymentRepository = paymentRepository;
            _descriptionRepository = descriptionRepository;
            _deliveryRepository = deliveryRepository;
            _customerRepository = customerRepository;
            _customerSendHistoryRepository = customerSendHistoryRepository;
            _officeSendHistoryRepository = officeSendHistoryRepository;
        }

        public async Task<Package> Handle(CreatePackage_Payment_Description_Delivery_Customer request, CancellationToken cancellationToken)
        {
            var package = request.Package;
            var payment = request.Payment;
            var description = request.Description;
            var delivery = request.Delivery;
            var customer = request.Customer;

            // Kiểm tra xem có khách hàng nào với số điện thoại trùng không
            var customerByPhone = await _customerRepository.FindByPhone(customer.Phone);

            if (customerByPhone != null)
            {
                // Sử dụng khách hàng cũ
                package.SenderId = customerByPhone.Id;
            }
            else
            {
                // Nếu không tìm thấy khách hàng nào, tạo khách hàng mới
                customer.Id = ObjectId.GenerateNewId().ToString();
                await _customerRepository.Create(customer);
                package.SenderId = customer.Id;
            }

            // Check and handle ObjectId fields if they are empty strings or invalid
            package.Id = string.IsNullOrEmpty(package.Id) ? ObjectId.GenerateNewId().ToString() : package.Id;
            payment.Id = string.IsNullOrEmpty(payment.Id) ? ObjectId.GenerateNewId().ToString() : payment.Id;
            description.Id = string.IsNullOrEmpty(description.Id) ? ObjectId.GenerateNewId().ToString() : description.Id;
            delivery.Id = string.IsNullOrEmpty(delivery.Id) ? ObjectId.GenerateNewId().ToString() : delivery.Id;

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
