using DigiVaultAPI.Features.Courses.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.Queries;
using DigiVaultAPI.Features.Courses.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Handlers.Queries;

public class GetNewestCoursesHandler : IRequestHandler<GetNewestCoursesQuery, List<CourseListDto>>
{
    private readonly ICourseProvider _provider;

    public GetNewestCoursesHandler(ICourseProvider provider)
    {
        _provider = provider;
    }

    public async Task<List<CourseListDto>> Handle(GetNewestCoursesQuery query, CancellationToken cancellationToken)
    {
        var courses = await _provider.GetNewestCourses(10);
        return courses.Adapt<List<CourseListDto>>();
    }
}
