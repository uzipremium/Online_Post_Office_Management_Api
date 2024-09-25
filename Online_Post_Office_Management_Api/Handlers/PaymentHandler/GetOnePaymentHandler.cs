using MediatR;
using Microsoft.Extensions.Logging;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.PaymentQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PaymentHandler
{
    public class GetOnePaymentHandler : IRequestHandler<PaymentGetOne, Payment>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<GetOnePaymentHandler> _logger;

        public GetOnePaymentHandler(IPaymentRepository paymentRepository, ILogger<GetOnePaymentHandler> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task<Payment> Handle(PaymentGetOne request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching payment with ID: {request.Id}");

            var payment = await _paymentRepository.GetById(request.Id);

            if (payment == null)
            {
                _logger.LogWarning($"Payment with ID {request.Id} not found.");
            }
            else
            {
                _logger.LogInformation($"Payment with ID {request.Id} retrieved successfully.");
            }

            return payment;
        }
    }
}
