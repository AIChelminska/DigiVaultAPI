using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Courses.Messages.DTOs;
using DigiVaultAPI.Features.Admin.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, PagedResult<AdminCategoryDto>>
{
    private readonly IAdminProvider _provider;

    public GetCategoriesHandler(IAdminProvider provider)
    {
        _provider = provider;
    }

    public async Task<PagedResult<AdminCategoryDto>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await _provider.GetCategories(query.Page, query.PageSize, query.Search);
        var total = await _provider.GetCategoriesCount(query.Search);

        return new PagedResult<AdminCategoryDto>
        {
            Items = categories.Adapt<List<AdminCategoryDto>>(),
            Total = total,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalPages = (int)Math.Ceiling(total / (double)query.PageSize)
        };
    }
}