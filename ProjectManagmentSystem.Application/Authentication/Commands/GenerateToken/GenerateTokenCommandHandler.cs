using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.Authentication.Dtos;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Domain.IRepositories;
namespace ProjectManagementSystem.Application.Authentication.Commands.GenerateToken;
public class GenerateTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,ITokenProvider tokenProvider,
    ILogger<GenerateTokenCommandHandler> logger) : IRequestHandler<GenerateTokenCommand, TokenDto>
{
    public async Task<TokenDto> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Generating token for user: {Email}", request.Email);
        // TEMPORARY: Mock user for demonstration
        //var mockUserId = Guid.NewGuid();
        //var mockRoles = new[] { "User", "ProjectManager" };
        var token = tokenProvider.GenerateToken(request);
        logger.LogInformation("Token generated successfully for user: {Email}", request.Email);
        return token;
    }
}
