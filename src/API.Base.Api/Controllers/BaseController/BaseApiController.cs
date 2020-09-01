using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Base.Api.Controllers.BaseController
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController: ControllerBase
    {
    }
}