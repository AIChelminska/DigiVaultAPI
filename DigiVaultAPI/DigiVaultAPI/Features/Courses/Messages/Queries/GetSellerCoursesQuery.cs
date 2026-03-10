using DigiVaultAPI.Features.Courses.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Messages.Queries;

public class GetSellerCoursesQuery : IRequest<PagedResult<SellerCourseDto>>
{
    public int IdUser { get; set; }         // z JWT claims
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
