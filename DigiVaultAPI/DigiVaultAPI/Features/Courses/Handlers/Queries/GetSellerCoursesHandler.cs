using DigiVaultAPI.Features.Courses.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.Queries;
using DigiVaultAPI.Features.Courses.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Handlers.Queries;

public class GetSellerCoursesHandler : IRequestHandler<GetSellerCoursesQuery, PagedResult<SellerCourseDto>>
{
    private readonly ICourseProvider _provider;

    public GetSellerCoursesHandler(ICourseProvider provider)
    {
        _provider = provider;
    }

    public async Task<PagedResult<SellerCourseDto>> Handle(GetSellerCoursesQuery query, CancellationToken cancellationToken)
    {
        // Kursy autora — wszystkie statusy widoczne w panelu (ukryte i usunięte też)
        var courses = await _provider.GetSellerCourses(query.IdUser, query.Page, query.PageSize);
        var total   = await _provider.GetSellerCoursesCount(query.IdUser);
        var dtos    = courses.Adapt<List<SellerCourseDto>>();

        return new PagedResult<SellerCourseDto>
        {
            Items      = dtos,
            Total      = total,
            Page       = query.Page,
            PageSize   = query.PageSize,
            TotalPages = (int)Math.Ceiling((double)total / query.PageSize)
        };
    }
}
