using DigiVaultAPI.Features.Review.Messages.Commands;
using DigiVaultAPI.Features.Review.Services;
using MediatR;

namespace DigiVaultAPI.Features.Review.Handlers.Commands;

public class AddReviewHandler : IRequestHandler<AddReviewCommand>
{
    private readonly IReviewService _reviewService;

    public AddReviewHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }
    
    public async Task Handle(AddReviewCommand command, CancellationToken cancellationToken)
    {
        await _reviewService.AddReview(command.IdUser, command.IdCourse, command.Rating, command.Comment);
    }
}