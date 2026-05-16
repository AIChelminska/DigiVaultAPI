using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class CreateCmsContentHandler : IRequestHandler<CreateCmsContentCommand>
{
    private readonly IAdminService _adminService;

    public CreateCmsContentHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(CreateCmsContentCommand command, CancellationToken cancellationToken)
    {
        await _adminService.CreateCmsContent(command.Key, command.Title, command.Value);
    }
}
