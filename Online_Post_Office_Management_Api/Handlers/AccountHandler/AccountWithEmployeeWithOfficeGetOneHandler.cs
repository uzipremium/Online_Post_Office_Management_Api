using MediatR;
using Online_Post_Office_Management_Api.DTO;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Queries.AccountQuery;
using Online_Post_Office_Management_Api.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using System;

namespace Online_Post_Office_Management_Api.Handlers.AccountHandler
{
    /// <summary>
    /// Handler for retrieving an account with associated employee and office details.
    /// </summary>
    public class AccountWithEmployeeWithOfficeGetOneHandler : IRequestHandler<AccountWithEmployeeWithOfficeGetOne, EmployeeWithAccountWithOfficeDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOfficeRepository _officeRepository;

        public AccountWithEmployeeWithOfficeGetOneHandler(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IOfficeRepository officeRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _officeRepository = officeRepository;
        }

        public async Task<EmployeeWithAccountWithOfficeDto> Handle(AccountWithEmployeeWithOfficeGetOne request, CancellationToken cancellationToken)
        {
            // Lấy vai trò của người dùng từ token
            var userRole = request.User.FindFirst(ClaimTypes.Role)?.Value;

            // Nếu không phải là admin, kiểm tra ID trong token có khớp với ID yêu cầu không
            if (userRole != "admin")
            {
                var userIdFromToken = request.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdFromToken) || userIdFromToken != request.Id)
                {
                    throw new UnauthorizedAccessException("Invalid token: Account ID mismatch.");
                }
            }

            // Kiểm tra token có hết hạn không
            if (request.Token.ValidTo < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Token has expired.");
            }

            // Lấy thông tin tài khoản cùng với thông tin nhân viên và văn phòng liên quan
            var account = await _accountRepository.GetAccountWithEmployeeAndOfficeById(request.Id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {request.Id} not found.");
            }

            var employee = await _employeeRepository.GetById(account.EmployeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {account.EmployeeId} not found.");
            }

            var office = await _officeRepository.GetById(employee.OfficeId);
            var officeName = office?.OfficeName ?? "N/A";

            return new EmployeeWithAccountWithOfficeDto
            {
                AccountId = account.AccountId,
                Username = account.Username,
                RoleId = account.RoleId,

                EmployeeId = employee.Id,
                Email = employee.Email,
                Phone = employee.Phone,
                Gender = employee.Gender,
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                CreatedDate = employee.CreatedDate,

                OfficeId = employee.OfficeId,
                OfficeName = officeName
            };
        }
    }
}
