using MediatR;
using DigiVaultAPI.Features.Categories.Providers;
using DigiVaultAPI.Features.Categories.Messages.Queries;
using DigiVaultAPI.Features.Categories.Messages.DTOs;
using Mapster;


namespace DigiVaultAPI.Features.Categories.Handlers.Queries;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoryProvider _provider;
    
    public GetCategoriesHandler(ICategoryProvider provider)
    {
        _provider = provider;
    }
    
    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await _provider.GetCategories();
        return categories.Adapt<List<CategoryDto>>();
    }
}