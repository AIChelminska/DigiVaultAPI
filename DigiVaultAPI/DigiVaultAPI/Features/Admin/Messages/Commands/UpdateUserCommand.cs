using DigiVaultAPI.Models;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class UpdateUserCommand : IRequest
{
    public int IdUser { get; set; }
    public UserRole Role { get; set; }
    public int WarningsCount { get; set; } = 0;
    public bool IsActive { get; set; }
}