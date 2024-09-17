using MediatR;
using Online_Post_Office_Management_Api.DTO.Response;

namespace Online_Post_Office_Management_Api.Queries.PackageQuery
{
    public class GetAllPackagesQuery : IRequest<IEnumerable<PackageResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string OfficeId { get; set; }
        public DateTime? StartDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}
