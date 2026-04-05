using MediatR;

namespace DigiVaultAPI.Features.Review.Messages.Commands;

public class AddReviewCommand : IRequest
{
    public int IdUser { get; set; }
    public int IdCourse { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}