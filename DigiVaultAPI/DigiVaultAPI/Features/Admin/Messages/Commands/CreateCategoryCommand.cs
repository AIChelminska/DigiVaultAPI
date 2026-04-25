using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class CreateCategoryCommand : IRequest
{
    public string Name { get; set; } = string.Empty;
}
