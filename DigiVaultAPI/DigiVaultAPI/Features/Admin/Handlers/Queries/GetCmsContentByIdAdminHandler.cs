using DigiVaultAPI.Exceptions;
using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetCmsContentByIdAdminHandler : IRequestHandler<GetCmsContentByIdAdminQuery, AdminCmsContentDto>
{
    private readonly IAdminProvider _provider;

    public GetCmsContentByIdAdminHandler(IAdminProvider provider)
    {
        _provider = provider;
    }

    public async Task<AdminCmsContentDto> Handle(GetCmsContentByIdAdminQuery query, CancellationToken cancellationToken)
    {
        var item = await _provider.GetCmsContentById(query.IdContent);
        if (item == null)
            throw new NotFoundException("CMS content not found");

        return item.Adapt<AdminCmsContentDto>();
    }
}
