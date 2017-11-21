using Aurora.API.Backend.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurora.API.Backend.Requests.Form
{
    public class CreateForm : IRequest<Response<CreateResult>>
    {
        public string Name { get; set; }
    }
}
