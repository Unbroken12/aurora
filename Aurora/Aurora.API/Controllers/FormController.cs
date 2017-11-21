using Microsoft.AspNetCore.Mvc;
using MediatR;
using Aurora.API.Backend.Requests.Form;
using System.Threading.Tasks;
using Aurora.API.Backend.Responses;

namespace Aurora.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Form")]
    public class FormController : Controller
    {
        private readonly IMediator _mediator;

        public FormController(IMediator mediator)
        {
            _mediator = mediator;
        }        

        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<ActionResult> CreateAsync([FromBody]CreateForm createFormRequest)
        {
            var response = await _mediator.Send(createFormRequest);

            if (response.Result == CreateResult.Created)
                return Ok(); // TODO: return 201
            else
                return StatusCode(500); // TODO: return client error
        }
    }
}
