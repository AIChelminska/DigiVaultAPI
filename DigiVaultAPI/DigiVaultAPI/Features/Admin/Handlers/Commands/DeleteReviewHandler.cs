using DigiVaultAPI.Features.Admin.Messages.Commands;
using DigiVaultAPI.Features.Admin.Services;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Commands;

public class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand>
{
    private readonly IAdminService _adminService;

    public DeleteReviewHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        await _adminService.DeleteReview(request.IdReview);
    }
}