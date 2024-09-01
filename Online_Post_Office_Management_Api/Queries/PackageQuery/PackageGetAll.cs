using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.PackageQuery
{
    public class PackageGetAll : IRequest<List<Package>>
    {
    }
}
