using DigiVaultAPI.Features.Cms.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Cms.Messages.Queries;

public class GetCmsContentByKeyQuery : IRequest<CmsContentDto>
{
    public string Key { get; set; } = string.Empty;
}
