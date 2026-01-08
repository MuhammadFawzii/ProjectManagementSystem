using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.Authentication.Commands.GenerateToken;
using ProjectManagementSystem.Application.Authentication.Commands.RefreshToken;
namespace ProjectManagementSystem.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TokenController(IMediator mediator) : ControllerBase
{
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateToken(GenerateTokenCommand request)
    {
        var token = await mediator.Send(request);
        return Ok(token);
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand request)
    {
        var token = await mediator.Send(request);
        return Ok(token);
    } 
}
