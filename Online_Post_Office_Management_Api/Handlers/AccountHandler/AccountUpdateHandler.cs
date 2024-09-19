using MediatR;
using Online_Post_Office_Management_Api.Commands.AccountCommand;
using Online_Post_Office_Management_Api.Models;
using Online_Post_Office_Management_Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Post_Office_Management_Api.Handlers.AccountHandler
{
    public class AccountUpdateHandler : IRequestHandler<UpdateAccount, Account>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountUpdateHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> Handle(UpdateAccount request, CancellationToken cancellationToken)
        {
           
            var existingAccount = await _accountRepository.GetById(request.Id);

            if (existingAccount == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

          
            existingAccount.Username = request.Username;
            existingAccount.Password = request.Password;  
            existingAccount.RoleId = request.RoleId;

           
            var updateResult = await _accountRepository.Update(request.Id, existingAccount);

           
            if (!updateResult)
            {
                throw new Exception("Account update failed.");
            }

            
            return existingAccount;
        }
    }
}
