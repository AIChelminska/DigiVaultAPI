using MediatR;

namespace DigiVaultAPI.Features.Courses.Messages.Commands;

public class ToggleVisibilityCommand : IRequest
{
    public int IdCourse { get; set; }       // z route
    public int IdUser { get; set; }         // z JWT claims
    public bool IsVisible { get; set; }     // z body
}
