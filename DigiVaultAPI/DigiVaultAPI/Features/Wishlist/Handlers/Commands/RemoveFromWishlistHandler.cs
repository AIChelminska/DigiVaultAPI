using MediatR;
using DigiVaultAPI.Features.Wishlist.Services;
using DigiVaultAPI.Features.Wishlist.Messages.Commands;

namespace DigiVaultAPI.Features.Wishlist.Handlers.Commands;

public class RemoveFromWishlistHandler : IRequestHandler<RemoveFromWishlistCommand>
{
    private readonly IWishlistService _service;
    
    public RemoveFromWishlistHandler(IWishlistService service)
    {
        _service = service;
    }
    
    public async Task Handle(RemoveFromWishlistCommand request, CancellationToken cancellationToken)
    {
        await _service.RemoveFromWishlist(request.IdUser, request.IdCourse);
    }
}