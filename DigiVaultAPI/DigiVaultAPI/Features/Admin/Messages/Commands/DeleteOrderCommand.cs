using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class DeleteOrderCommand : IRequest
{
    public int IdOrder { get; set; }
}