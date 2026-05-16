using DigiVaultAPI.Features.Cms.Messages.DTOs;
using DigiVaultAPI.Features.Cms.Messages.Queries;
using DigiVaultAPI.Features.Cms.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Cms.Handlers.Queries;

public class GetCmsContentsHandler : IRequestHandler<GetCmsContentsQuery, List<CmsContentDto>>
{
    private readonly ICmsProvider _provider;

    public GetCmsContentsHandler(ICmsProvider provider)
    {
        _provider = provider;
    }

    public async Task<List<CmsContentDto>> Handle(GetCmsContentsQuery query, CancellationToken cancellationToken)
    {
        var items = await _provider.GetAll();
        return items.Adapt<List<CmsContentDto>>();
    }
}
