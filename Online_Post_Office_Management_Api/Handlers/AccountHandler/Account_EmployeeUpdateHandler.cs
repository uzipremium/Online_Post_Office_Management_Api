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

        // Constructor để inject các repository
        public Account_EmployeeUpdateHandler(IAccountRepository accountRepository, IEmployeeRepository employeeRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<EmployeeWithAccountWithOfficeDto> Handle(UpdateAccount_Employee request, CancellationToken cancellationToken)
        {
            // Kiểm tra thông tin tài khoản
            if (string.IsNullOrEmpty(request.AccountId))
            {
                throw new ArgumentException("Account ID must be provided.");
            }
            var existingAccount = await _accountRepository.GetById(request.AccountId);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            // Kiểm tra thông tin nhân viên
            if (string.IsNullOrEmpty(request.EmployeeId))
            {
                throw new ArgumentException("Employee ID must be provided.");
            }
            var existingEmployee = await _employeeRepository.GetById(request.EmployeeId);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException("Employee associated with the account not found.");
            }

            // Kiểm tra tính hợp lệ của email
            if (!string.IsNullOrEmpty(request.Email) && !IsValidEmail(request.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            // Kiểm tra tính hợp lệ của số điện thoại
            if (!string.IsNullOrEmpty(request.Phone) && !IsValidPhone(request.Phone))
            {
                throw new ArgumentException("Phone number must be between 10 and 15 digits.");
            }

            // Cập nhật thông tin tài khoản nếu có mật khẩu mới
            if (!string.IsNullOrEmpty(request.Password))
            {
                existingAccount.Password = request.Password; // Cập nhật mật khẩu nếu có mật khẩu mới
            }

            // Cập nhật thông tin nhân viên nếu có thay đổi
            if (!string.IsNullOrEmpty(request.Email)) existingEmployee.Email = request.Email;
            if (!string.IsNullOrEmpty(request.Phone)) existingEmployee.Phone = request.Phone;
            if (!string.IsNullOrEmpty(request.Gender)) existingEmployee.Gender = request.Gender;
            if (!string.IsNullOrEmpty(request.Name)) existingEmployee.Name = request.Name;
            if (request.DateOfBirth != default) existingEmployee.DateOfBirth = request.DateOfBirth;
            if (!string.IsNullOrEmpty(request.OfficeId)) existingEmployee.OfficeId = request.OfficeId;

            // Cập nhật thông tin tài khoản trong cơ sở dữ liệu
            var accountUpdateResult = await _accountRepository.Update(existingAccount.Id, new EmployeeWithAccountWithOfficeDto
            {
                AccountId = existingAccount.Id,
                Username = existingAccount.Username,
                Password = existingAccount.Password,  
                RoleId = existingAccount.RoleId
            });

            if (accountUpdateResult == null)
            {
                throw new Exception("Account update failed.");
            }

            
            var employeeToUpdate = new Employee
            {
                Id = existingEmployee.Id,
                Name = existingEmployee.Name,
                Gender = existingEmployee.Gender,
                DateOfBirth = existingEmployee.DateOfBirth,
                CreatedDate = existingEmployee.CreatedDate,
                Email = existingEmployee.Email,
                Phone = existingEmployee.Phone,
                OfficeId = existingEmployee.OfficeId,
                AccountId = existingEmployee.AccountId
            };

        
            var employeeUpdateResult = await _employeeRepository.Update(existingEmployee.Id, employeeToUpdate);
            if (employeeUpdateResult == null)
            {
                throw new Exception("Employee update failed.");
            }

         
            var employeeWithOffice = await _employeeRepository.GetById2(existingEmployee.Id);
            if (employeeWithOffice == null)
            {
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
