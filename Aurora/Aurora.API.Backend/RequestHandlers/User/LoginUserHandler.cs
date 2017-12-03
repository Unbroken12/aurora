using Aurora.API.Backend.Requests.User;
using Aurora.API.Backend.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Aurora.API.Backend.RequestHandlers.User
{
    public class LoginUserHandler : AsyncRequestHandler<LoginRequest, Response<CreateResult>>
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<Database.Collections.User> _userManager;
        private readonly ILogger _logger;

        public LoginUserHandler(Microsoft.AspNetCore.Identity.UserManager<Database.Collections.User> userManager, ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<LoginUserHandler>();
        }

        protected override async Task<Response<CreateResult>> HandleCore(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return new Response<CreateResult>(CreateResult.NotCreated);
            var passwordCheckResult = await _userManager.CheckPasswordAsync(user, request.Password);

            // todo: add new results with errors, (password bad, user not found, user locked and etc.)
            if (passwordCheckResult)
                return new Response<CreateResult>(CreateResult.Created);
            else
                return new Response<CreateResult>(CreateResult.NotCreated);
        }
    }
}
