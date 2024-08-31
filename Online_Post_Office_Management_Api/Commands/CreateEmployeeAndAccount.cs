using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Commands
{
    public class CreateEmployeeAndAccount : IRequest<Employee>
    {
        public Employee Employee { get; set; }
        public Account Account { get; set; }

        public CreateEmployeeAndAccount(Employee employee, Account account)
        {
            Employee = employee;
            Account = account;
        }
    }
}
