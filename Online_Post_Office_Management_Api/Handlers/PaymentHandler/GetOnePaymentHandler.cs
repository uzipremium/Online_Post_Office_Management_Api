using MediatR;
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

        public GetOnePaymentHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> Handle(PaymentGetOne request, CancellationToken cancellationToken)
        {
            return await _paymentRepository.GetById(request.Id);
        }
    }
}
