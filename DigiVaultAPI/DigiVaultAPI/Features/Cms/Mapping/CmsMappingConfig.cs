using DigiVaultAPI.Features.Cms.Messages.DTOs;
using DigiVaultAPI.Models;
using Mapster;

namespace DigiVaultAPI.Features.Cms.Mapping;

public class CmsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CMSContent, CmsContentDto>();
    }
}
