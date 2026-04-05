using MediatR;
using DigiVaultAPI.Features.Wishlist.Services;
using DigiVaultAPI.Features.Wishlist.Messages.Commands;

namespace DigiVaultAPI.Features.Wishlist.Handlers.Commands;

public class AddToWishlistHandler : IRequestHandler<AddToWishlistCommand>
{
    private readonly IWishlistService _service;
    
    public AddToWishlistHandler(IWishlistService service)
    {
        _service = service;
    }
    
    public async Task Handle(AddToWishlistCommand request, CancellationToken cancellationToken)
    {
        await _service.AddToWishlist(request.IdUser, request.IdCourse);
    }
}