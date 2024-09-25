using MediatR;
using Online_Post_Office_Management_Api.Commands.AccountCommand;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.AccountHandler
{
    public class Account_EmployeeUpdateHandler : IRequestHandler<UpdateAccount_Employee, EmployeeWithAccountWithOfficeDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<Account_EmployeeUpdateHandler> _logger;

        public Account_EmployeeUpdateHandler(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, ILogger<Account_EmployeeUpdateHandler> logger)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger;
        }

        public async Task<EmployeeWithAccountWithOfficeDto> Handle(UpdateAccount_Employee request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting account and employee update for AccountId: {request.AccountId}, EmployeeId: {request.EmployeeId}");

            var existingAccount = await _accountRepository.GetById(request.AccountId);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            var existingEmployee = await _employeeRepository.GetById(request.EmployeeId);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException("Employee associated with the account not found.");
            }

            // Validate Email
            if (!string.IsNullOrEmpty(request.Email) && !IsValidEmail(request.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            // Validate Phone
            if (!string.IsNullOrEmpty(request.Phone) && !IsValidPhone(request.Phone))
            {
                throw new ArgumentException("Invalid phone format. The phone number must contain 10 to 15 digits.");
            }

            var employeeHasChanges = existingEmployee.Email != request.Email ||
                                     existingEmployee.Phone != request.Phone ||
                                     existingEmployee.Gender != request.Gender ||
                                     existingEmployee.Name != request.Name ||
                                     existingEmployee.DateOfBirth != request.DateOfBirth ||
                                     existingEmployee.OfficeId != request.OfficeId;

            if (!employeeHasChanges)
            {
                _logger.LogInformation("No changes detected in employee.");
                return new EmployeeWithAccountWithOfficeDto
                {
                    EmployeeId = existingEmployee.Id,
                    Name = existingEmployee.Name,
                    Gender = existingEmployee.Gender,
                    DateOfBirth = existingEmployee.DateOfBirth,
                    CreatedDate = existingEmployee.CreatedDate,
                    Email = existingEmployee.Email,
                    Phone = existingEmployee.Phone,
                    OfficeId = existingEmployee.OfficeId,
                    OfficeName = (await _employeeRepository.GetById2(existingEmployee.Id))?.OfficeName,
                    AccountId = existingEmployee.AccountId,
                    Username = existingAccount.Username,
                    RoleId = existingAccount.RoleId
                };
            }

            _logger.LogInformation("Updating employee in the database.");
            var employeeToUpdate = new Employee
            {
                Id = existingEmployee.Id,
                Name = request.Name ?? existingEmployee.Name,
                Gender = request.Gender ?? existingEmployee.Gender,
                DateOfBirth = request.DateOfBirth != default ? request.DateOfBirth : existingEmployee.DateOfBirth,
                CreatedDate = existingEmployee.CreatedDate,
                Email = request.Email ?? existingEmployee.Email,
                Phone = request.Phone ?? existingEmployee.Phone,
                OfficeId = request.OfficeId ?? existingEmployee.OfficeId,
                AccountId = existingEmployee.AccountId
            };

            var employeeUpdateResult = await _employeeRepository.Update(existingEmployee.Id, employeeToUpdate);
            if (employeeUpdateResult == null)
            {
                _logger.LogError($"Failed to update employee with EmployeeId: {existingEmployee.Id}");
                throw new Exception("Employee update failed.");
            }

            _logger.LogInformation("Successfully updated employee.");

            if (!string.IsNullOrEmpty(request.Password))
            {
                _logger.LogInformation($"Received Password: {request.Password}");
                _logger.LogInformation("Updating account password.");
                existingAccount.Password = BCrypt.Net.BCrypt.HashPassword(request.Password); // Mã hóa mật khẩu
            }

            var accountUpdateResult = await _accountRepository.Update(existingAccount.Id, new EmployeeWithAccountWithOfficeDto
            {
                AccountId = existingAccount.Id,
                Username = existingAccount.Username, // Giữ nguyên Username
                Password = existingAccount.Password, // Lưu mật khẩu đã mã hóa
                RoleId = existingAccount.RoleId
            });

            if (accountUpdateResult == null)
            {
                _logger.LogError($"Failed to update account with AccountId: {existingAccount.Id}");
                throw new Exception("Account update failed.");
            }


            var employeeWithOffice = await _employeeRepository.GetById2(existingEmployee.Id);
            if (employeeWithOffice == null)
            {
                _logger.LogError($"Failed to fetch office information for EmployeeId: {existingEmployee.Id}");
                throw new KeyNotFoundException("Failed to fetch employee with office information.");
            }

            return new EmployeeWithAccountWithOfficeDto
            {
                EmployeeId = existingEmployee.Id,
                Name = existingEmployee.Name,
                Gender = existingEmployee.Gender,
                DateOfBirth = existingEmployee.DateOfBirth,
                CreatedDate = existingEmployee.CreatedDate,
                Email = existingEmployee.Email,
                Phone = existingEmployee.Phone,
                OfficeId = existingEmployee.OfficeId,
                OfficeName = employeeWithOffice.OfficeName,
                AccountId = existingAccount.Id,
                Username = existingAccount.Username,
                RoleId = existingAccount.RoleId
            };
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }

        private bool IsValidPhone(string phone)
        {
            var phoneRegex = @"^\d{10,15}$";
            return Regex.IsMatch(phone, phoneRegex);
        }
    }

}



