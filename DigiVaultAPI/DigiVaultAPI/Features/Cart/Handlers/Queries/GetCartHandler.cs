using DigiVaultAPI.Features.Cart.Messages.Queries;
using DigiVaultAPI.Features.Cart.Providers;
using DigiVaultAPI.Features.Courses.Messages.DTOs;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Cart.Handlers.Queries;

public class GetCartHandler : IRequestHandler<GetCartQuery, List<CourseListDto>>
{
    private readonly ICartProvider _provider;
    
    public GetCartHandler(ICartProvider provider)
    {
        _provider = provider;
    }
    
    public async Task<List<CourseListDto>> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        var cart = await _provider.GetCartItems(query.IdUser);
        var dtos = cart.Adapt<List<CourseListDto>>();
        return dtos;
    }
}