using DigiVaultAPI.Exceptions;
using DigiVaultAPI.Features.Cms.Messages.DTOs;
using DigiVaultAPI.Features.Cms.Messages.Queries;
using DigiVaultAPI.Features.Cms.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Cms.Handlers.Queries;

public class GetCmsContentByKeyHandler : IRequestHandler<GetCmsContentByKeyQuery, CmsContentDto>
{
    private readonly ICmsProvider _provider;

    public GetCmsContentByKeyHandler(ICmsProvider provider)
    {
        _provider = provider;
    }

    public async Task<CmsContentDto> Handle(GetCmsContentByKeyQuery query, CancellationToken cancellationToken)
    {
        var item = await _provider.GetByKey(query.Key);
        if (item == null)
            throw new NotFoundException("CMS content not found");

        return item.Adapt<CmsContentDto>();
    }
}
