using MediatR;
using ProjectManagementSystem.Application.Authentication.Dtos;

namespace ProjectManagementSystem.Application.Authentication.Commands.GenerateToken;
public class GenerateTokenCommand:IRequest<TokenDto>
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public List<string> Permissions { get; set; } = [];
    public List<string> Roles { get; set; } = [];
}
