using DigiVaultAPI.Features.Courses.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.Queries;
using DigiVaultAPI.Features.Courses.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Handlers.Queries;

public class GetTopRatedCoursesHandler : IRequestHandler<GetTopRatedCoursesQuery, List<CourseListDto>>
{
    private readonly ICourseProvider _provider;

    public GetTopRatedCoursesHandler(ICourseProvider provider)
    {
        _provider = provider;
    }

    public async Task<List<CourseListDto>> Handle(GetTopRatedCoursesQuery query, CancellationToken cancellationToken)
    {
        var courses = await _provider.GetTopRatedCourses(10);
        return courses.Adapt<List<CourseListDto>>();
    }
}
