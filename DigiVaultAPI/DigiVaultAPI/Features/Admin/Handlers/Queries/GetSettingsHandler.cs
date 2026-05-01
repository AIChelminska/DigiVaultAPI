using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Messages.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DigiVaultAPI.Data;
using DigiVaultAPI.Features.Admin.Providers;

namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetSettingsHandler : IRequestHandler<GetSettingsQuery, AdminSettingsDto>
{
    private readonly IAdminProvider _provider;

    public GetSettingsHandler(IAdminProvider provider)
    {
        _provider = provider;
    }

    public async Task<AdminSettingsDto> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        return await _provider.GetSettings();
    }
}