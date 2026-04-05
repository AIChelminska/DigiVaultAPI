using DigiVaultAPI.Features.Categories.Messages.DTOs;
using DigiVaultAPI.Models;
using Mapster;

namespace DigiVaultAPI.Features.Categories.Mapping;

public class CategoryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryDto>();
    }
}