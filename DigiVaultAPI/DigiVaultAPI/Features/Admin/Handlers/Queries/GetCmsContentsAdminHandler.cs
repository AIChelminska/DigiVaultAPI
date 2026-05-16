using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Providers;
using DigiVaultAPI.Features.Courses.Messages.DTOs;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetCmsContentsAdminHandler : IRequestHandler<GetCmsContentsAdminQuery, PagedResult<AdminCmsContentDto>>
{
    private readonly IAdminProvider _provider;

    public GetCmsContentsAdminHandler(IAdminProvider provider)
    {
        _provider = provider;
    }

    public async Task<PagedResult<AdminCmsContentDto>> Handle(GetCmsContentsAdminQuery query, CancellationToken cancellationToken)
    {
        var items = await _provider.GetCmsContents(query.Page, query.PageSize, query.Search);
        var total = await _provider.GetCmsContentsCount(query.Search);

        return new PagedResult<AdminCmsContentDto>
        {
            Items = items.Adapt<List<AdminCmsContentDto>>(),
            Total = total,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalPages = (int)Math.Ceiling(total / (double)query.PageSize)
        };
    }
}
