using DigiVaultAPI.Features.Review.Messages.DTOs;
using DigiVaultAPI.Models;
using Mapster;

namespace DigiVaultAPI.Features.Review.Mapping;

public class ReviewMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Review → ReviewDto
        config.NewConfig<DigiVaultAPI.Models.Review, ReviewDto>()
            .Map(dest=>dest.AuthorName, src=>src.User != null
                ? src.User.FirstName + " " + src.User.LastName
                : string.Empty);
    }
}