using ProjectManagementSystem.Application.Authentication.Commands.GenerateToken;
using ProjectManagementSystem.Application.Authentication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Common.Interfaces
{
    public interface ITokenProvider
    {
        public TokenDto GenerateToken(GenerateTokenCommand generateTokenCommand);
    }
}
