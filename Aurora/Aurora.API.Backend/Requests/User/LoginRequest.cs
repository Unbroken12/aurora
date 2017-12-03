using Aurora.API.Backend.Responses;
using MediatR;

namespace Aurora.API.Backend.Requests.User
{
    public class LoginRequest : IRequest<Response<CreateResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
