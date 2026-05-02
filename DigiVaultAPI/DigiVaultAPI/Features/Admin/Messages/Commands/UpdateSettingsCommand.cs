using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class UpdateSettingsCommand : IRequest
{
    public decimal CommissionRate { get; set; }
}