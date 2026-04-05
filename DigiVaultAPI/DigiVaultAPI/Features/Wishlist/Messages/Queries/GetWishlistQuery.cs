using DigiVaultAPI.Features.Courses.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Wishlist.Messages.Queries;

public class GetWishlistQuery : IRequest<List<CourseListDto>>
{
    public int IdUser { get; set; }   
}