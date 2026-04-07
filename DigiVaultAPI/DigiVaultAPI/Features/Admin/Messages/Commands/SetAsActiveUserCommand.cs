using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class SetAsActiveUserCommand : IRequest
{
    public int IdUser { get; set; }
}