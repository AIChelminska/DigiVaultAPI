using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Queries;

public class GetCategoriesQuery : IRequest<PagedResult<AdminCategoryDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Search { get; set; }
    public bool IsActive { get; set; } = true;
}