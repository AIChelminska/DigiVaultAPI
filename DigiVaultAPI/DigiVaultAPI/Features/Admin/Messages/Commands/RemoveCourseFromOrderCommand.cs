using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Commands;

public class RemoveCourseFromOrder : IRequest
{
    public int IdOrder { get; set; }
    public int IdCourse { get; set; }
}