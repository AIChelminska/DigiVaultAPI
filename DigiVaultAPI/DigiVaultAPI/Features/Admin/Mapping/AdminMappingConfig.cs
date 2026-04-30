using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Orders.Messages.DTOs;
using DigiVaultAPI.Models;
using Mapster;

namespace DigiVaultAPI.Features.Admin.Mapping;

public class AdminMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, AdminOrdersDto>()
            .Map(dest => dest.FullName, src => src.User.FirstName + " " + src.User.LastName)
            .Map(dest => dest.Email, src => src.User.Email)
            .Map(dest => dest.ItemsCount, src => src.OrderItems.Count);

        config.NewConfig<User, AdminUserDetailDto>()
            .Map(dest => dest.CreatedCourses, src => src.Courses)
            .Map(dest => dest.PurchasedCourses, src => src.UserCourses.Select(uc => uc.Course));
            
        config.NewConfig<Course, AdminCourseDetailDto>()
            .Map(dest => dest.AuthorName, src => src.User.FirstName + " " + src.User.LastName)
            .Map(dest => dest.CategoryName, src => src.Category.Name)
            .Map(dest => dest.ReportsCount, src => src.CourseReports.Count);

        config.NewConfig<Order, AdminOrderDetailsDto>()
            .Map(dest => dest.UserName, src => src.User.FirstName + " " + src.User.LastName)
            .Map(dest => dest.Email, src => src.User.Email)
            .Map(dest => dest.ItemsCount, src => src.OrderItems.Count);

        config.NewConfig<OrderItem, OrderItemDto>()
            .Map(dest => dest.Title, src => src.Course != null ? src.Course.Title : string.Empty);
    }
}