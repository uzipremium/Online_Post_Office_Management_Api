using MediatR;
using Models = Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Commands.EmployeeCommand
{
    public class CreateEmployeeAndAccount : IRequest<Models.Employee>
    {
        public Models.Employee Employee { get; set; }
        public Models.Account Account { get; set; }

        public CreateEmployeeAndAccount(Models.Employee employee, Models.Account account)
        {
            Employee = employee;
            Account = account;
        }
    }
}
