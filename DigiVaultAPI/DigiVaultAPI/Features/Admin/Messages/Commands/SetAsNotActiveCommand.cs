using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class SetAsNotActiveCommand : IRequest
{
    public int IdUser { get; set; }
}