using DigiVaultAPI.Features.Review.Messages.DTOs;
using DigiVaultAPI.Features.Review.Messages.Queries;
using DigiVaultAPI.Features.Review.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Review.Handlers.Queries;

public class GetReviewByIdHandler : IRequestHandler<GetReviewByIdQuery, IEnumerable<ReviewDto>>
{
    private readonly IReviewProvider _provider;

    public GetReviewByIdHandler(IReviewProvider provider)
    {
        _provider = provider;
    }

    public async Task<IEnumerable<ReviewDto>> Handle(GetReviewByIdQuery query, CancellationToken cancellationToken)
    {
        var reviews = await _provider.GetReviewById(query.IdCourse);
        return reviews.Adapt<IEnumerable<ReviewDto>>();
    }
}
