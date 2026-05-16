using DigiVaultAPI.Features.Cms.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Cms.Messages.Queries;

public class GetCmsContentsQuery : IRequest<List<CmsContentDto>>
{
}
