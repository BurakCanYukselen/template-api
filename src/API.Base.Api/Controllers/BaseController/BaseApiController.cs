using System.Threading.Tasks;
using API.Base.Core.Extensions;
using API.Base.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Base.Api.Controllers.BaseController
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public override OkObjectResult Ok(object value)
        {
            return base.Ok(value.ToApiResponse(true));
        }
    }
}