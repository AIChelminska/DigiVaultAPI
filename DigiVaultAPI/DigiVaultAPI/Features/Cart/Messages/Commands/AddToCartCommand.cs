using MediatR;

namespace DigiVaultAPI.Features.Cart.Messages.Commands;

public class AddToCartCommand : IRequest
{
    public int IdUser { get; set; } 
    public int IdCourse { get; set; }
}