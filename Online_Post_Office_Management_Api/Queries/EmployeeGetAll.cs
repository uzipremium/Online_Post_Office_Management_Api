using MediatR;

namespace Online_Post_Office_Management_Api.Queries
{
    public class EmployeeGetAll: IRequest<List<Employee>>
    {
    }
}
