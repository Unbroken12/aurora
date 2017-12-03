using Microsoft.AspNetCore.Mvc;
using MediatR;
using Aurora.API.Backend.Requests.Form;
using System.Threading.Tasks;
using Aurora.API.Backend.Responses;
using Microsoft.AspNetCore.Authorization;

namespace Aurora.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FormController : Controller
    {
        private readonly IMediator _mediator;

        public FormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<ActionResult> Create([FromBody]CreateFormRequest createFormRequest)
        {
            var response = await _mediator.Send(createFormRequest);

            if (response.Result == CreateResult.Created)
                return Ok(); // TODO: return 201
            else
                return StatusCode(500); // TODO: return client error
        }
    }
}
