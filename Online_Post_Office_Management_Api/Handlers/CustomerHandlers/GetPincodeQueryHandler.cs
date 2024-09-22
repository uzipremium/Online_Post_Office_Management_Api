using MediatR;
using MongoDB.Driver;
using Online_Post_Office_Management_Api.DTO.Response;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.CustomerQuery;

namespace Online_Post_Office_Management_Api.Handlers.CustomerHandlers
{
    public class GetPincodeQueryHandler : IRequestHandler<GetPincodeQuery, PincodeResponse>
    {
        private readonly IMongoCollection<Office> _offices;

        public GetPincodeQueryHandler(IMongoDatabase database)
        {
            _offices = database.GetCollection<Office>("Offices");
        }

        public async Task<PincodeResponse> Handle(GetPincodeQuery request, CancellationToken cancellationToken)
        {
            var office = await _offices.Find(o => o.Id == request.OfficeId)
                               .FirstOrDefaultAsync(cancellationToken);

            if (office == null)
            {
                throw new KeyNotFoundException("Not Found Office");
            }

            return new PincodeResponse
            {
                Location = office.City,
                Pincode = office.PinCode
            };
        }
    }
}
