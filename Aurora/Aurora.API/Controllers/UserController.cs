using Aurora.API.Backend.Database.Collections;
using Aurora.API.Backend.Requests.User;
using Aurora.API.Backend.Responses;
using Aurora.API.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aurora.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly SignInManager<User> _signInManager;

        public UserController(ILogger<UserController> logger, IMediator mediator, SignInManager<User> signInManager)
        {
            _logger = logger;
            _mediator = mediator;
            _signInManager = signInManager;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterUserRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Result == CreateResult.Created)
                return Ok(); // TODO: return 201
            else
                return StatusCode(500); // TODO: return client error
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Result == CreateResult.NotCreated)
                return Unauthorized();

            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("aurora.api super secret key"))
                                .AddSubject(request.UserName)
                                .AddIssuer("Aurora.Security.Bearer")
                                .AddAudience("Aurora.Security.Bearer")
                                .AddClaim("MembershipId", "111")
                                .AddExpiry(1)
                                .Build();

            return Ok(token.Value);
        }
    }
}