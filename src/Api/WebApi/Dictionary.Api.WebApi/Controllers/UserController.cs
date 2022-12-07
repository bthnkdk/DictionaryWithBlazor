using Dictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }
    }
}
