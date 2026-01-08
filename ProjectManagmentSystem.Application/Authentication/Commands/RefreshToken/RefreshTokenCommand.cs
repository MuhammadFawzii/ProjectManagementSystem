using MediatR;
using ProjectManagementSystem.Application.Authentication.Dtos;

namespace ProjectManagementSystem.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<TokenDto> // Changed from IRequest<GenerateTokenCommand>
{
    public string? RefreshToken { get; set; }
}
