using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class DeleteCmsContentHandler : IRequestHandler<DeleteCmsContentCommand>
{
    private readonly IAdminService _adminService;

    public DeleteCmsContentHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(DeleteCmsContentCommand command, CancellationToken cancellationToken)
    {
        await _adminService.DeleteCmsContent(command.IdContent);
    }
}
