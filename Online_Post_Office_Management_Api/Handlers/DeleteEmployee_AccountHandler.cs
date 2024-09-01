using MediatR;
using Online_Post_Office_Management_Api.Commands;
using Online_Post_Office_Management_Api.Repositories;

namespace Online_Post_Office_Management_Api.Handlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee_Account, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<DeleteEmployeeHandler> _logger;

        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository, IAccountRepository accountRepository, ILogger<DeleteEmployeeHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteEmployee_Account request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetById(request.Id);

            if (employee == null)
            {
                _logger.LogWarning($"Employee with ID {request.Id} not found.");
                return false;
            }

        
            var employeeDeleted = await _employeeRepository.Delete(request.Id);

            if (!employeeDeleted)
            {
                _logger.LogError($"Failed to delete Employee with ID {request.Id}.");
                return false;
            }

            // Xóa Account liên quan
            var accountDeleted = await _accountRepository.Delete(employee.AccountId);

            if (!accountDeleted)
            {
                _logger.LogError($"Failed to delete Account with ID {employee.AccountId}.");
           
            }

            return true; 
        }

    }
}