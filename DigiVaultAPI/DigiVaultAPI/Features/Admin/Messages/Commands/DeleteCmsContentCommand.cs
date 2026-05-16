using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class DeleteCmsContentCommand : IRequest
{
    public int IdContent { get; set; }
}
