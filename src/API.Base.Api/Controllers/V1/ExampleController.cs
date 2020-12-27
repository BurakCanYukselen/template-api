using System;
using System.Net;
using System.Threading.Tasks;
using API.Base.Api.Controllers;
using API.Base.Api.Controllers.BaseController;
using API.Base.Core.Models;
using API.Base.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Base.Api.Controllers.V1
{
    [ApiVersion("1")]
    [ApiRoute("Example")]
    public class ExampleController : BaseApiController
    {
        public ExampleController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("example-api-endpoint")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<ErrorModel>), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ExampleApiEndpoint()
        {
            var serviceResponse = await _mediator.Send(new ExampleServiceRequest()
            {
                Id = Guid.NewGuid(),
            });
            return Ok(serviceResponse);
        }
    }
}