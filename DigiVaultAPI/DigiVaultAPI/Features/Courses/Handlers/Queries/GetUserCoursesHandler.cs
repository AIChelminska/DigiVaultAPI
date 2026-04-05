using DigiVaultAPI.Features.Courses.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.Queries;
using DigiVaultAPI.Features.Courses.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Handlers.Queries;

public class GetUserCoursesHandler : IRequestHandler<GetUserCoursesQuery, IEnumerable<CourseListDto>>
{
    private readonly ICourseProvider _provider;

    public GetUserCoursesHandler(ICourseProvider provider)
    {
        _provider = provider;
    }

    public async Task<IEnumerable<CourseListDto>> Handle(GetUserCoursesQuery query, CancellationToken cancellationToken)
    {
        var courses = await _provider.GetUserCourses(query.IdUser);
        return courses.Adapt<IEnumerable<CourseListDto>>();
    }
}