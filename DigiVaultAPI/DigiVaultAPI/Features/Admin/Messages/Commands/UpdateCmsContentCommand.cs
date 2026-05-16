using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class UpdateCmsContentCommand : IRequest
{
    public int IdContent { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
