using System.Threading.Tasks;
using API.Base.Api.Controllers.BaseController;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Base.Api.Controllers.V2
{
    [ApiVersion("2")]
    public class ExampleController: BaseApiController
    {
        public ExampleController(IMediator mediator) : base(mediator)
        {
        }
        
        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> Info()
        { 
            var version = HttpContext.GetRequestedApiVersion();
            return Ok(version);
        }
    }
}