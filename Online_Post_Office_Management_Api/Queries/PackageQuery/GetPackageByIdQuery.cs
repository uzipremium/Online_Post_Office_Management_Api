using MediatR;
using Online_Post_Office_Management_Api.DTO.Response;

namespace Online_Post_Office_Management_Api.Queries.PackageQuery
{
    public class GetPackageByIdQuery : IRequest<PackageResponse>
    {
        public string Id { get; set; }
    }
}
