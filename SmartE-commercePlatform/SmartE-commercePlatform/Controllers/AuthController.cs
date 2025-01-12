using Application.Use_Cases.Authentication.Commands;
using Application.Use_Cases.Authentication.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await mediator.Send(command);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { userId = result.Value });
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginUserCommand command)
        {
            return (await mediator.Send(command))
                .Match<ActionResult<LoginResponseDto>>(
                    loginResponse => Ok(loginResponse),
                    errors => BadRequest(errors)
                );
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<RefreshResponseDto>> Refresh([FromBody] RefreshAccessCommand command)
        {
            return (await mediator.Send(command))
                .Match<ActionResult<RefreshResponseDto>>(
                    loginResponse => Ok(loginResponse),
                    errors => BadRequest(errors)
                );
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout([FromBody] LogoutCommand command)
        {
            return (await mediator.Send(command))
                .Match<ActionResult>(
                    _ => NoContent(),
                    errors => BadRequest(errors)
                );
        }

        [HttpPost("logoutall")]
        public async Task<ActionResult> LogoutAll([FromBody] LogoutAllCommand command)
        {
            return (await mediator.Send(command))
                .Match<ActionResult>(
                    _ => NoContent(),
                    errors => BadRequest(errors)
                );
        }

        [HttpPut("changepassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            return (await mediator.Send(command))
                .Match<ActionResult>(
                    _ => NoContent(),
                    errors => BadRequest(errors)
                );
        }
    }
}
