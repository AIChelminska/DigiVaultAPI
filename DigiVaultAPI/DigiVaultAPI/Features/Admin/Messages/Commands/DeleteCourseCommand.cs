using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class DeleteCourseCommand : IRequest
{
    public int IdCourse { get; set; }
}