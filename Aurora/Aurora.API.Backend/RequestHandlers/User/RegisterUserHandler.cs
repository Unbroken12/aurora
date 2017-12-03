using Aurora.API.Backend.Requests.User;
using Aurora.API.Backend.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Aurora.API.Backend.RequestHandlers.User
{
    public class RegisterUserHandler : AsyncRequestHandler<RegisterUserRequest, Response<CreateResult>>
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<Database.Collections.User> _userManager;
        private readonly ILogger _logger;

        public RegisterUserHandler(Microsoft.AspNetCore.Identity.UserManager<Database.Collections.User> userManager, ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<RegisterUserHandler>();
        }

        protected override async Task<Response<CreateResult>> HandleCore(RegisterUserRequest request)
        {
            var user = new Database.Collections.User(request.UserName, request.Email);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation(3, "User created a new account with password.");

                return new Response<CreateResult>(CreateResult.Created);
            }

            return new Response<CreateResult>(CreateResult.NotCreated);
        }
    }
}
