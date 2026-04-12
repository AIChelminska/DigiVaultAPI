using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Orders.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Queries;

public class GetOrdersQuery : IRequest<PagedResult<AdminOrdersDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Search { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}