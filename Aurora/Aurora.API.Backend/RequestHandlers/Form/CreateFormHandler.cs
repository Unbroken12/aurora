using Aurora.API.Backend.Requests.Form;
using Aurora.API.Backend.Responses;
using MediatR;
using MongoDB.Driver;
using System.Threading.Tasks;
using Aurora.API.Backend.Database.Collections;

namespace Aurora.API.Backend.RequestHandlers.Form
{
    public class CreateFormHandler : IAsyncRequestHandler<CreateForm, Response<CreateResult>>
    {
        private readonly IMongoCollection<DynamicForm> _mongoCollection;

        public CreateFormHandler(IMongoCollection<DynamicForm> mongoCollection)
        {
            _mongoCollection = mongoCollection;
        }

        public async Task<Response<CreateResult>> Handle(CreateForm request)
        {
            // todo: use automapper
            var form = new DynamicForm() { Name = request.Name };

            await _mongoCollection.InsertOneAsync(form);

            return new Response<CreateResult>(CreateResult.Created, null);
        }
    }
}
