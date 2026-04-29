using DigiVaultAPI.Features.Admin.Messages.DTOs;
using DigiVaultAPI.Features.Admin.Messages.Queries;
using DigiVaultAPI.Features.Admin.Providers;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Handlers.Queries;

public class GetCourseByIdAdminHandler : IRequestHandler<GetCourseByIdAdminQuery, AdminCourseDetailDto>
{
    private readonly IAdminProvider _provider;

    public GetCourseByIdAdminHandler(IAdminProvider provider)
    {
        _provider = provider;
    }

    public async Task<AdminCourseDetailDto> Handle(GetCourseByIdAdminQuery query, CancellationToken cancellationToken)
    {
        var course = await _provider.GetCourseById(query.IdCourse);
        return course.Adapt<AdminCourseDetailDto>();
    }
}