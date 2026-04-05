using DigiVaultAPI.Features.Review.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Review.Messages.Queries;

public class GetReviewByIdQuery : IRequest<IEnumerable<ReviewDto>>
{
    public int IdCourse { get; set; }
}
