using MediatR;
using Microsoft.Extensions.Logging;
using Online_Post_Office_Management_Api.Commands.PackageCommand;
using Online_Post_Office_Management_Api.Exceptions;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PackageHandler
{
    public class UpdatePackageHandler : IRequestHandler<UpdatePackage, bool>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IPaymentRepository _paymentRepository;

        public UpdatePackageHandler(IPackageRepository packageRepository, IServiceRepository serviceRepository, IPaymentRepository paymentRepository)
        {
            _packageRepository = packageRepository;
            _serviceRepository = serviceRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<bool> Handle(UpdatePackage request, CancellationToken cancellationToken)
        {
            var package = await _packageRepository.GetPackageById(request.Id);
            if (package == null)
            {
                return false;
            }

            if (package.Weight == (decimal)request.Weight &&
                package.Distance == (decimal)request.Distance &&
                package.DeliveryNumber == request.DeliveryNumber &&
                package.Receiver == request.Receiver)
            {
                throw new NoChangeException();
            }

            package.Weight = (decimal)request.Weight;
            package.Distance = (decimal)request.Distance;
            package.DeliveryNumber = request.DeliveryNumber;
            package.Receiver = request.Receiver;


            var service = await _serviceRepository.GetById(package.ServiceId);
            if (service == null)
            {
                throw new ArgumentException("Invalid service type provided.");
            }

            var payment = await _paymentRepository.GetById(package.PaymentId);
            payment.Cost = CalculateCost(package.Weight, package.Distance, service.BaseRate, service.RatePerKg, service.RatePerKm);

            await _paymentRepository.Update(payment.Id, payment);

            var updateResult = await _packageRepository.Update(request.Id, package);

            return updateResult;
        }

        private decimal CalculateCost(decimal weight, decimal distance, decimal baseRate, decimal ratePerKg, decimal ratePerKm)
        {
            return baseRate + (ratePerKg * weight) + (ratePerKm * distance);
        }
    }
}
