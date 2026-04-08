using DigiVaultAPI.Features.Courses.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Messages.Queries;

public class GetCoursesQuery : IRequest<PagedResult<CourseListDto>>
{
    public string? Search { get; set; }
    public int? IdCategory { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? SortBy { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool IncludeHidden { get; set; } = false;
}
