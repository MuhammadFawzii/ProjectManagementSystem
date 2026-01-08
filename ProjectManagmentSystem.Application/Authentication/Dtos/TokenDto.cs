namespace ProjectManagementSystem.Application.Authentication.Dtos;
public class TokenDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime Expires { get; set; }
}
