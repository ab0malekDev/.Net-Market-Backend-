using BimMarket.Application.Auth;
using BimMarket.Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BimMarket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new LoginCommand(request.Email, request.Password), ct);
        if (result == null)
            return Unauthorized(new { message = "بريد أو كلمة مرور غير صحيحة" });
        return Ok(result);
    }
}
