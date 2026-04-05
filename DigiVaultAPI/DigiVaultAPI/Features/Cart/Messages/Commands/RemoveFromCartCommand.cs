using MediatR;

namespace DigiVaultAPI.Features.Cart.Messages.Commands;

public class RemoveFromCartCommand : IRequest
{
    public int IdUser { get; set; } 
    public int IdCourse { get; set; }
}