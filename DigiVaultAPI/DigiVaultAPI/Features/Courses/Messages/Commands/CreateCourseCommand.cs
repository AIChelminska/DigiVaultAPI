using MediatR;

namespace DigiVaultAPI.Features.Courses.Messages.Commands;

// Zwraca int — IdCourse nadany przez PostgreSQL
public class CreateCourseCommand : IRequest<int>
{
    public int IdUser { get; set; }         // wstrzykiwany przez Controller z JWT claims
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int IdCategory { get; set; }
}
