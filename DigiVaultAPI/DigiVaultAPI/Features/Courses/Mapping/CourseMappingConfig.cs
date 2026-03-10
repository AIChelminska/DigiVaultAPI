using DigiVaultAPI.Features.Courses.Messages.DTOs;
using DigiVaultAPI.Models;
using Mapster;

namespace DigiVaultAPI.Features.Courses.Mappings;

public class CourseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Course → CourseListDto
        // AuthorName i CategoryName wymagają navigation properties (Include w Providerze)
        config.NewConfig<Course, CourseListDto>()
            .Map(dest => dest.AuthorName, src => src.User != null
                ? src.User.FirstName + " " + src.User.LastName
                : string.Empty)
            .Map(dest => dest.CategoryName, src => src.Category != null
                ? src.Category.Name
                : string.Empty);

        // Course → CourseDetailDto
        config.NewConfig<Course, CourseDetailDto>()
            .Map(dest => dest.AuthorName, src => src.User != null
                ? src.User.FirstName + " " + src.User.LastName
                : string.Empty)
            .Map(dest => dest.CategoryName, src => src.Category != null
                ? src.Category.Name
                : string.Empty);

        // Course → SellerCourseDto
        config.NewConfig<Course, SellerCourseDto>()
            .Map(dest => dest.CategoryName, src => src.Category != null
                ? src.Category.Name
                : string.Empty);
    }
}
