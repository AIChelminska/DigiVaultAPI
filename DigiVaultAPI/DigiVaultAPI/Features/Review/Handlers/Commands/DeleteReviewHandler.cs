using DigiVaultAPI.Features.Review.Messages.Commands;
using DigiVaultAPI.Features.Review.Services;
using MediatR;

namespace DigiVaultAPI.Features.Review.Handlers.Commands;

public class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand>
{
    private readonly IReviewService _reviewService;

    public DeleteReviewHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task Handle(DeleteReviewCommand command, CancellationToken cancellationToken)
    {
        await _reviewService.DeleteReview(command.IdUser, command.IdCourse);
    }
}