using Aurora.API.Backend.Requests.Form;
using Aurora.API.Backend.Responses;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Aurora.API.Backend.RequestHandlers.Form
{
    public class CreateFormHandler : IAsyncRequestHandler<CreateForm, Response<CreateResult>>
    {
        public CreateFormHandler()
        {
        }

        public async Task<Response<CreateResult>> Handle(CreateForm request)
        {
            throw new NotImplementedException();
        }
    }
}
