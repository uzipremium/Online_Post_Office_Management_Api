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
            // Extract Account ID from the ClaimsPrincipal (user claims)
            var userIdFromToken = request.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Log both Account IDs for debugging purposes
            Console.WriteLine($"User ID from Token: {userIdFromToken}");
            Console.WriteLine($"Account ID from Request: {request.Id}");


            // Validate token claims
            if (string.IsNullOrEmpty(userIdFromToken))
            {
                throw new UnauthorizedAccessException("Token does not contain a valid Account ID.");
            }

            if (userIdFromToken != request.Id)
            {
                throw new UnauthorizedAccessException("Invalid token: Account ID mismatch.");
            }

            // Optional: Check token validity, expiration, etc.
            if (request.Token.ValidTo < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Token has expired.");
            }

            // Fetch the account from the database using the Account ID from the request
            var account = await _accountRepository.GetById(request.Id);

            // Check if the account was found
            if (account == null)
            {
                throw new UnauthorizedAccessException("Account not found.");
            }

            return account;
        }




    }
}