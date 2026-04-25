using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class DeleteCategoryCommand : IRequest
{
    public int IdCategory { get; set; }
}