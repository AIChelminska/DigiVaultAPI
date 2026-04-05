using MediatR;

namespace DigiVaultAPI.Features.Wishlist.Messages.Commands;

public class AddToWishlistCommand : IRequest
{
    public int IdUser { get; set; } 
    public int IdCourse { get; set; }
}