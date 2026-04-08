namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, PagedResult<AdminOrdersDto>>
{
    private readonly IOrderProvider _provider;

    public GetOrdersHandler(IOrderProvider provider)
    {
        _provider = provider;
    }
}