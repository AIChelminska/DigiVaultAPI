using DigiVaultAPI.Features.Courses.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.Queries;
using DigiVaultAPI.Features.Courses.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Handlers.Queries;

public class GetCoursesHandler : IRequestHandler<GetCoursesQuery, PagedResult<CourseListDto>>
{
    private readonly ICourseProvider _provider;

    public GetCoursesHandler(ICourseProvider provider)
    {
        _provider = provider;
    }

    public async Task<PagedResult<CourseListDto>> Handle(GetCoursesQuery query, CancellationToken cancellationToken)
    {
        // 1. Pobierz stronę kursów i łączną liczbę
        var courses = await _provider.GetCourses(query.Search, query.IdCategory, query.MinPrice, query.MaxPrice, query.SortBy, query.Page, query.PageSize);
        var total   = await _provider.GetCoursesCount(query.Search, query.IdCategory, query.MinPrice, query.MaxPrice);

        // 2. Mapster: Course → CourseListDto
        var dtos = courses.Adapt<List<CourseListDto>>();

        // 3. Zwróć z metadanymi paginacji
        return new PagedResult<CourseListDto>
        {
            Items      = dtos,
            Total      = total,
            Page       = query.Page,
            PageSize   = query.PageSize,
            TotalPages = (int)Math.Ceiling((double)total / query.PageSize)
        };
    }
}
