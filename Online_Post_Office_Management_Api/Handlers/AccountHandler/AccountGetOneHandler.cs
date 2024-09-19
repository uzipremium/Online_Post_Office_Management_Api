using MediatR;
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
    public class AccountGetOneHandler : IRequestHandler<AccountGetOne, Account>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountGetOneHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> Handle(AccountGetOne request, CancellationToken cancellationToken)
        {
           
            var tokenAccountId = request.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            
            if (string.IsNullOrEmpty(tokenAccountId))
            {
                throw new UnauthorizedAccessException("Token không chứa account ID");
            }

         
            if (tokenAccountId != request.Id)
            {
                throw new UnauthorizedAccessException("Invalid token: Account ID mismatch");
            }

      
            var account = await _accountRepository.GetById(request.Id);

            if (account == null)
            {
                throw new KeyNotFoundException("Account not found");
            }

            return account;
        }
    }
}