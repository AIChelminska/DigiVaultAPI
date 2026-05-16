using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class UpdateCmsContentHandler : IRequestHandler<UpdateCmsContentCommand>
{
    private readonly IAdminService _adminService;

    public UpdateCmsContentHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(UpdateCmsContentCommand command, CancellationToken cancellationToken)
    {
        await _adminService.UpdateCmsContent(command.IdContent, command.Title, command.Value);
    }
}
