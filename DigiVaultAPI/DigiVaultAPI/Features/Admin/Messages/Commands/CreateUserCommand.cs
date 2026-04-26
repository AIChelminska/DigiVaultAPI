using MediatR;
using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class CreateUserCommand : IRequest
{
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Worker;
}