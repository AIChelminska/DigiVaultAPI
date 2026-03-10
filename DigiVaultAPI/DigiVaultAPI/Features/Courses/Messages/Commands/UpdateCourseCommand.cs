using MediatR;

namespace DigiVaultAPI.Features.Courses.Messages.Commands;

public class UpdateCourseCommand : IRequest
{
    public int IdCourse { get; set; }       // z route
    public int IdUser { get; set; }         // z JWT claims
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int IdCategory { get; set; }
}
