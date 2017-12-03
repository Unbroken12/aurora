using Aurora.API.Backend.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurora.API.Backend.Requests.User
{
    public class RegisterUserRequest : IRequest<Response<CreateResult>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
