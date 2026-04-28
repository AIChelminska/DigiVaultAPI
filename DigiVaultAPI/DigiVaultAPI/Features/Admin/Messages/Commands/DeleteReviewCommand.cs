using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class DeleteReviewCommand : IRequest
{
    public int IdReview { get; set; }
}