using MediatR;
using DigiVaultAPI.Features.Cart.Services;
using DigiVaultAPI.Features.Cart.Messages.Commands;

namespace DigiVaultAPI.Features.Cart.Handlers.Commands;

public class AddToCartHandler : IRequestHandler<AddToCartCommand>
{
    private readonly ICartService _service;
    
    public AddToCartHandler(ICartService service)
    {
        _service = service;
    }
    
    public async Task Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        await _service.AddToCart(request.IdUser, request.IdCourse);
    }
}