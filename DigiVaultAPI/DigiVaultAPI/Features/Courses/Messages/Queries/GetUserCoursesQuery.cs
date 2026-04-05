using DigiVaultAPI.Features.Courses.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Messages.Queries;

public class GetUserCoursesQuery : IRequest<IEnumerable<CourseListDto>>
{
    public int IdUser { get; set; }
}