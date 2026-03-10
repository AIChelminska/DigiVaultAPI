using DigiVaultAPI.Features.Courses.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Messages.Queries;

public class GetCourseByIdQuery : IRequest<CourseDetailDto>
{
    public int IdCourse { get; set; }
}
