using MediatR;
using Microsoft.Extensions.Logging;
using Online_Post_Office_Management_Api.Commands.PaymentCommand;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PaymentHandler
{
    public class UpdatePaymentHandler : IRequestHandler<UpdatePayment, bool>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<UpdatePaymentHandler> _logger;

        public UpdatePaymentHandler(IPaymentRepository paymentRepository, ILogger<UpdatePaymentHandler> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdatePayment request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating payment with ID: {request.Id}");

            var payment = await _paymentRepository.GetById(request.Id);

            if (payment == null)
            {
                _logger.LogWarning($"Payment with ID {request.Id} not found for update.");
                return false;
            }

            payment.Status = request.Status;

            var updateResult = await _paymentRepository.Update(request.Id, payment);

            if (updateResult)
            {
                _logger.LogInformation($"Payment with ID {request.Id} updated successfully.");
            }
            else
            {
                _logger.LogError($"Failed to update payment with ID {request.Id}.");
            }

            return updateResult;
        }
    }
}
