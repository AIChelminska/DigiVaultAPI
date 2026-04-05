using Mapster;
using MediatR;
using DigiVaultAPI.Features.Wishlist.Providers;
using DigiVaultAPI.Features.Courses.Messages.DTOs;
using DigiVaultAPI.Features.Wishlist.Messages.Queries;

namespace DigiVaultAPI.Features.Wishlist.Handlers.Queries;

public class GetWishlistHandler : IRequestHandler<GetWishlistQuery, List<CourseListDto>>
{
    private readonly IWishlistProvider _provider;
    
    public GetWishlistHandler(IWishlistProvider provider)
    {
        _provider = provider;
    }
    
    public async Task<List<CourseListDto>> Handle(GetWishlistQuery query, CancellationToken cancellationToken)
    {
        var courses = await _provider.GetWishlistItemsAsync(query.IdUser);
        var dtos = courses.Adapt<List<CourseListDto>>();
        return dtos;
    }
    
}