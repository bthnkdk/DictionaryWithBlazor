using Dictionary.Api.Application.Features.Commands.User.ConfirmEmail;
using Dictionary.Api.Application.Features.Queries.GetUserDetail;
using Dictionary.Common.Models.CommandModels;
using Dictionary.Common.Models.CommandModels.User;
using Dictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await mediator.Send(new GetUserDetailQuery(id));

            return Ok(result);
        }

        [HttpGet]
        [Route("UserName/{userName}")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var result = await mediator.Send(new GetUserDetailQuery(Guid.Empty, userName));

            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("Confirm")]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var result = await mediator.Send(new ConfirmEmailCommand() { ConfirmationId = id });

            return Ok(result);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            if (!command.UserId.HasValue)
                command.UserId = UserId;

            var result = await mediator.Send(command);

            return Ok(result);
        }
    }
}
