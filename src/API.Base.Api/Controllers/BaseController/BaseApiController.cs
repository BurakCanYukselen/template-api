using API.Base.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Base.Api.Controllers.BaseController
{
    [ApiController]
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

        protected string GetFromRoute(string routeObjectName) => (string) HttpContext.Request.RouteValues[routeObjectName];
    }
}