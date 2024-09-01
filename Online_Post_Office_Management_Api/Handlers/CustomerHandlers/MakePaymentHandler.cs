using MediatR;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.Commands.CustomerCommands;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.CustomerHandlers
{
    public class MakePaymentHandler : IRequestHandler<MakePaymentCommand, bool>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMongoCollection<Service> _services;

        public MakePaymentHandler(IPackageRepository packageRepository, IPaymentRepository paymentRepository, IMongoClient mongoClient)
        {
            _packageRepository = packageRepository;
            _paymentRepository = paymentRepository;

            var database = mongoClient.GetDatabase("qwer");
            _services = database.GetCollection<Service>("Service");
        }

        public async Task<bool> Handle(MakePaymentCommand request, CancellationToken cancellationToken)
        {
            // Tìm Package dựa trên PackageId
            var package = await _packageRepository.GetPackageByIdAsync(request.PackageId);

            if (package == null)
            {
                return false; // Không tìm thấy Package
            }

            // Tìm Service dựa trên ServiceId của Package
            var service = await _services.Find(s => s.Id == package.Service.Id).FirstOrDefaultAsync(cancellationToken);

            if (service == null || service.Name == "VPP")
            {
                return false; // Không thực hiện thanh toán nếu dịch vụ là VPP hoặc không tìm thấy Service
            }

            // Cập nhật trạng thái Payment thành "paided"
            var paymentSuccess = await _paymentRepository.UpdatePaymentStatusAsync(package.PaymentId, "paided", DateTime.UtcNow);

            if (!paymentSuccess)
            {
                return false; // Không thể cập nhật trạng thái Payment
            }

            return true; // Trả về true nếu cập nhật thành công
        }
    }
}
