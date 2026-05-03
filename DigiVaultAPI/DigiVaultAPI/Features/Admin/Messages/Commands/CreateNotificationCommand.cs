using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class CreateNotificationCommand : IRequest
{
    public int IdUser { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}