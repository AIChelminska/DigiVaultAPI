using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class UpdateCategoryCommand : IRequest
{
    public int IdCategory { get; set; }
    public string Name { get; set; } = string.Empty;
}