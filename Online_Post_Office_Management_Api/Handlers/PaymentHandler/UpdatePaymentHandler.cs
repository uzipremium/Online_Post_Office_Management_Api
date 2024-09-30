using MediatR;
using Microsoft.Extensions.Logging;
using Online_Post_Office_Management_Api.Commands.PaymentCommand;
using Online_Post_Office_Management_Api.Exceptions;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PaymentHandler
{
    public class UpdatePaymentHandler : IRequestHandler<UpdatePayment, bool>
    {
        private readonly IPaymentRepository _paymentRepository;

        public UpdatePaymentHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<bool> Handle(UpdatePayment request, CancellationToken cancellationToken)
        {

            var payment = await _paymentRepository.GetById(request.Id);

            if (payment == null)
            {
                return false;
            }

            if (payment.Status == request.Status)
            {
                throw new NoChangeException();
            }

            payment.Status = request.Status;

            var updateResult = await _paymentRepository.Update(request.Id, payment);

            return updateResult;
        }
    }
}
