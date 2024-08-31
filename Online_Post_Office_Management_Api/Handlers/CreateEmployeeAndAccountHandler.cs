using MediatR;
using Online_Post_Office_Management_Api.Commands;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers
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

            account.Id = ObjectId.GenerateNewId().ToString();
            employee.Id = ObjectId.GenerateNewId().ToString();
            employee.AccountId = account.Id;

            await _accountRepository.Create(account);

            await _employeeRepository.Create(employee);

            return employee;
        }
    }
}
