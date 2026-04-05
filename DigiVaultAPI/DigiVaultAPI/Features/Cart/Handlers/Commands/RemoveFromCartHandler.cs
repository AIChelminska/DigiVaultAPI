using MediatR;
using DigiVaultAPI.Features.Cart.Services;
using DigiVaultAPI.Features.Cart.Messages.Commands;

namespace DigiVaultAPI.Features.Cart.Handlers.Commands;

public class RemoveFromCartHandler : IRequestHandler<RemoveFromCartCommand>
{
    private readonly ICartService _service;
    
    public RemoveFromCartHandler(ICartService service)
    {
        _service = service;
    }
    
    public async Task Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        await _service.RemoveFromCart(request.IdUser, request.IdCourse);
    }
}