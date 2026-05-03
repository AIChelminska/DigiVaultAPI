using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Queries;

public class GetReportsAdminQuery : IRequest<PagedResult<AdminReportDto>>
{
    public bool? IsResolved { get; set; } = null;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}