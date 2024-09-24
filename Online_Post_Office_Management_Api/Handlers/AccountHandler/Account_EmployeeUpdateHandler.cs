using MediatR;
using Online_Post_Office_Management_Api.Commands.AccountCommand;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories;
using System;
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
            // Lấy thông tin tài khoản
            var accountId = !string.IsNullOrEmpty(request.AccountId) ? request.AccountId : request.AccountId;
            var existingAccount = await _accountRepository.GetById(accountId);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            // Lấy thông tin nhân viên
            var employeeId = !string.IsNullOrEmpty(request.EmployeeId) ? request.EmployeeId : request.EmployeeId;
            var existingEmployee = await _employeeRepository.GetById(employeeId);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException("Employee associated with the account not found.");
            }

            // Cập nhật thông tin tài khoản nếu có mật khẩu mới (không rỗng)
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
                Password = existingAccount.Password,  // Đã cập nhật mật khẩu nếu có mật khẩu mới
                RoleId = existingAccount.RoleId
            });

            if (accountUpdateResult == null)
            {
                throw new Exception("Account update failed.");
            }

            // Tạo đối tượng Employee từ EmployeeWithOfficeDto trước khi cập nhật
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

            // Cập nhật thông tin nhân viên trong cơ sở dữ liệu
            var employeeUpdateResult = await _employeeRepository.Update(existingEmployee.Id, employeeToUpdate);
            if (employeeUpdateResult == null)
            {
                throw new Exception("Employee update failed.");
            }

            // Lấy thông tin văn phòng sau khi cập nhật
            var employeeWithOffice = await _employeeRepository.GetById2(existingEmployee.Id);
            if (employeeWithOffice == null)
            {
                throw new KeyNotFoundException("Failed to fetch employee with office information.");
            }

            // Trả về DTO chứa thông tin tài khoản và nhân viên đã cập nhật
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

    }
}
