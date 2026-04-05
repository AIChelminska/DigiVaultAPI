using MediatR;

namespace DigiVaultAPI.Features.Profile.Messages.Commands;

public class UpdateNameCommand : IRequest
{
    public int IdUser { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}