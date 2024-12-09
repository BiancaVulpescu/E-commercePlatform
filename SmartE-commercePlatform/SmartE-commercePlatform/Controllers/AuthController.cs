using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {  
        var result = await _mediator.Send(command);

        if (result.IsError)
        {
          return BadRequest(result.Errors);
        }

        return Ok(new { userId = result.Value });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsError)
        {
          return BadRequest(result.Errors);
        }

        return Ok(new { Token = result.Value });
  }
}
