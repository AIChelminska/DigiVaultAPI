using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Providers;
using DigiVaultAPI.Features.Courses.Messages.DTOs;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetReportsAdminHandler : IRequestHandler<GetReportsAdminQuery, PagedResult<AdminReportDto>>
{
    private readonly IAdminProvider _provider;

    public GetReportsAdminHandler(IAdminProvider provider)
    {
        _provider = provider;
    }

    public async Task<PagedResult<AdminReportDto>> Handle(GetReportsAdminQuery request, CancellationToken cancellationToken)
    {
        var reports = await _provider.GetReportsAdmin(request.IsResolved, request.Page, request.PageSize);
        var total = await _provider.GetReportsAdminCount(request.IsResolved);
        return new PagedResult<AdminReportDto>
        {
            Items = reports.Adapt<List<AdminReportDto>>(),
            Total = total,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalPages = (int)Math.Ceiling(total / (double)request.PageSize)
        };
    }
}