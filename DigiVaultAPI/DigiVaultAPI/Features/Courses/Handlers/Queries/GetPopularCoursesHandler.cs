using DigiVaultAPI.Features.Courses.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.Queries;
using DigiVaultAPI.Features.Courses.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Handlers.Queries;

public class GetPopularCoursesHandler : IRequestHandler<GetPopularCoursesQuery, List<CourseListDto>>
{
    private readonly ICourseProvider _provider;

    public GetPopularCoursesHandler(ICourseProvider provider)
    {
        _provider = provider;
    }

    public async Task<List<CourseListDto>> Handle(GetPopularCoursesQuery query, CancellationToken cancellationToken)
    {
        var courses = await _provider.GetPopularCourses(10);
        return courses.Adapt<List<CourseListDto>>();
    }
}
