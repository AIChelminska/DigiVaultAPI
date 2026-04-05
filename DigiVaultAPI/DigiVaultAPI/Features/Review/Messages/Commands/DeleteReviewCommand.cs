using MediatR;

namespace DigiVaultAPI.Features.Review.Messages.Commands;

public class DeleteReviewCommand : IRequest
{
    public int IdUser { get; set; }
    public int IdCourse { get; set; }
}