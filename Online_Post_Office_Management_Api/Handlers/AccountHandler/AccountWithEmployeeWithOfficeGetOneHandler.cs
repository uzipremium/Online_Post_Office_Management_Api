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
    public class AccountWithEmployeeWithOfficeGetOneHandler : IRequestHandler< AccountWithEmployeeWithOfficeGetOne, EmployeeWithAccountWithOfficeDto>
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
            // Lấy ID người dùng từ JWT Token
            var userIdFromToken = request.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Debugging
            Console.WriteLine($"User ID from Token: {userIdFromToken}");
            Console.WriteLine($"Account ID from Request: {request.Id}");

            if (string.IsNullOrEmpty(userIdFromToken))
            {
                throw new UnauthorizedAccessException("Token does not contain a valid Account ID.");
            }

            if (userIdFromToken != request.Id)
            {
                throw new UnauthorizedAccessException("Invalid token: Account ID mismatch.");
            }

            if (request.Token.ValidTo < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Token has expired.");
            }

        
            var account = await _accountRepository.GetAccountWithEmployeeAndOfficeById(request.Id);
            if (account == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

     
            var employee = await _employeeRepository.GetById(account.EmployeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
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
