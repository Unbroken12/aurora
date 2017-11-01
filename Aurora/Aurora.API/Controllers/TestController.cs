using Microsoft.AspNetCore.Mvc;

namespace Aurora.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        public ActionResult Get()
        {
            return Ok("test");
        }
    }
}