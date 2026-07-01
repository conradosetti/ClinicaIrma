using ClinicaIrma.Application.Auth.Commands.Login;
using ClinicaIrma.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaIrma.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IHandler<string, LoginCommand> loginHandler) : ControllerBase
{
    private readonly IHandler<string, LoginCommand> _loginHandler = loginHandler;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _loginHandler.HandleAsync(command, cancellationToken);
            return Ok(new { token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { erro = ex.Message });
        }
    }
}