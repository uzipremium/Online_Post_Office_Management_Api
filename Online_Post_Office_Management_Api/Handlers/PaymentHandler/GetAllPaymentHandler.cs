using MediatR;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.PaymentQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.PaymentHandler
{
    public class GetAllPaymentHandler : IRequestHandler<PaymentGetAll, List<Payment>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetAllPaymentHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<Payment>> Handle(PaymentGetAll request, CancellationToken cancellationToken)
        {
           
            int pageNumber = request.PageNumber;
            int pageSize = request.PageSize;

           
            string? paymentStatus = !string.IsNullOrEmpty(request.PaymentStatus) ? request.PaymentStatus : null;

       
            var payments = await _paymentRepository.GetAll(
                pageNumber,
                pageSize,
                paymentStatus,
                request.StartDate
            );

            return payments.ToList();
        }
    }
}
