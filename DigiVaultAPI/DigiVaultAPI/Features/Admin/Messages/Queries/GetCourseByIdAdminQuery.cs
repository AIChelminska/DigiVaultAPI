using DigiVaultAPI.Features.Admin.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Queries;

public class GetCourseByIdAdminQuery : IRequest<AdminCourseDetailDto>
{
    public int IdCourse { get; set; }
}