using MediatR;
using Online_Post_Office_Management_Api.Models;
namespace Online_Post_Office_Management_Api.Queries.EmployeeQuery
{
    public class EmployeeGetAll : IRequest<List<Employee>>
    {
    }
}
