using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries
{
    public class PackageGetAll: IRequest<List<Package>>
    {
    }
}
