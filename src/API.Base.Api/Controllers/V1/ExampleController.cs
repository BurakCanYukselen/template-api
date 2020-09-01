using System.Threading.Tasks;
using API.Base.Api.Controllers;
using API.Base.Api.Controllers.BaseController;
using Microsoft.AspNetCore.Mvc;

namespace API.Base.Api.Controllers.V1
{
    [ApiVersion("1")]
    public class ExampleController: BaseApiController
    {
        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> Info()
        { 
            var version = HttpContext.GetRequestedApiVersion();
            return Ok(version);
        }
    }
}