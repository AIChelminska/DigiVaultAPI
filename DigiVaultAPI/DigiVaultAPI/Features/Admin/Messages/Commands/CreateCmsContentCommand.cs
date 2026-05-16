using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class CreateCmsContentCommand : IRequest
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
