using MongoDB.Bson;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Commands.EmployeeCommand;
using Online_Post_Office_Management_Api.Models; 
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.EmployeeHandler
{
    public class CreateEmployeeWithAccountHandler : IRequestHandler<CreateEmployeeAndAccount, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRepository _accountRepository;

        public CreateEmployeeWithAccountHandler(IEmployeeRepository employeeRepository, IAccountRepository accountRepository)
        {
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Employee> Handle(CreateEmployeeAndAccount request, CancellationToken cancellationToken)
        {
            var account = request.Account;
            var employee = request.Employee;

            // Generate new IDs
            account.Id = ObjectId.GenerateNewId().ToString();
            employee.Id = ObjectId.GenerateNewId().ToString();
            employee.AccountId = account.Id;

            // Create Account
            await _accountRepository.Create(account);

            // Create Employee
            await _employeeRepository.Create(employee);

            // Return created employee
            return employee;
        }
    }
}
