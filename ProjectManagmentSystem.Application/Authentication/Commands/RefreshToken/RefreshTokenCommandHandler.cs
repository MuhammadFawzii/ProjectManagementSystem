using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Application.Authentication.Commands.GenerateToken;
using ProjectManagementSystem.Application.Authentication.Dtos;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Domain.Exceptions;
using ProjectManagementSystem.Domain.IRepositories;

namespace ProjectManagementSystem.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ITokenProvider tokenProvider,
    ILogger<RefreshTokenCommandHandler> logger) : IRequestHandler<RefreshTokenCommand, TokenDto> // Changed from GenerateTokenCommand
{
    public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // TODO: Replace with actual database query
        // This is mock data for demonstration purposes
        var refreshTokenRecordFromDB = new
        {
            Email = "Fawzi@gmail.com",
            UserId = "79410514-0136-4442-be9b-01f097c57f7a",
            RefreshToken = "7a6f23b4e1d04c9a8f5b6d7c8a9e01f1",
            Expires = DateTime.UtcNow.AddHours(12)
        };

        // Validate refresh token
        if (refreshTokenRecordFromDB is null ||
            request.RefreshToken != "7a6f23b4e1d04c9a8f5b6d7c8a9e01f1" ||
            refreshTokenRecordFromDB.Expires < DateTime.UtcNow)
        {
            throw new NotFoundException(nameof(RefreshTokenCommand), refreshTokenRecordFromDB?.Email ?? "Unknown");
        }

        logger.LogInformation("Refreshing token for user: {Email}", refreshTokenRecordFromDB.Email);
        
        // TODO: Replace with actual database query
        var userFromDb = new
        {
            Id = "79410514-0136-4442-be9b-01f097c57f7a",
            FirstName = "Primary",
            LastName = "Manager",
            Email = "pm@localhost",
            Permissions = new List<string> {
                "project:create",
                "project:read",
                "project:update",
                "project:delete",
                "project:assign_member",
                "project:manage_budget",
                "task:create",
                "task:read",
                "task:update",
                "task:delete",
                "task:assign_user",
                "task:update_status"
            },
            Roles = new List<string> {
                "ProjectManager"
            }
        };
        
        // Generate new token directly
        var generateTokenCommand = new GenerateTokenCommand
        {
            Id = userFromDb.Id,
            FirstName = userFromDb.FirstName,
            LastName = userFromDb.LastName,
            Email = userFromDb.Email,
            Roles = userFromDb.Roles,
            Permissions = userFromDb.Permissions
        };
        
        var token = tokenProvider.GenerateToken(generateTokenCommand);
        
        logger.LogInformation("Token refreshed successfully for user: {Email}", refreshTokenRecordFromDB.Email);
        
        // TODO: Store new refresh token in database
        
        return token;
    }
}
