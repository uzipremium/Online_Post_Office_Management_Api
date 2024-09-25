using MongoDB.Bson;
using Online_Post_Office_Management_Api.Repositories;
using Online_Post_Office_Management_Api.Commands.EmployeeCommand;
using Online_Post_Office_Management_Api.Models;
using MediatR;
using System;
using System.Text.RegularExpressions;
using BCrypt.Net;
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

            // Validate email format
            if (!IsValidEmail(employee.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            // Check if username already exists
            var existingAccount = await _accountRepository.GetByUsername(account.Username);
            if (existingAccount != null)
            {
                throw new ArgumentException("Username already exists. Please choose a different username.");
            }


            account.Password = HashPassword(account.Password);

            // Generate new IDs
            account.Id = ObjectId.GenerateNewId().ToString();
            employee.Id = ObjectId.GenerateNewId().ToString();
            employee.AccountId = account.Id;

            // Set default role if not provided
            if (string.IsNullOrEmpty(account.RoleId))
            {
                account.RoleId = "66ded343a0d268760bc80984";
            }

            // Set created date to current date/time
            employee.CreatedDate = DateTime.UtcNow;

            // Create Account
            await _accountRepository.Create(account);

            // Create Employee
            await _employeeRepository.Create(employee);

    
            return employee;
        }

        private bool IsValidEmail(string email)
        {
       
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private string HashPassword(string password)
        {
            // Tạo mã hash với chi phí (work factor) mặc định là 10
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
