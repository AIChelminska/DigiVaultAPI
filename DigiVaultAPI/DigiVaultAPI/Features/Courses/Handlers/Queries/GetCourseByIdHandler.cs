using DigiVaultAPI.Features.Courses.Messages.DTOs;
using DigiVaultAPI.Features.Courses.Messages.Queries;
using DigiVaultAPI.Features.Courses.Providers;
using DigiVaultAPI.Features.Courses.Services;
using Mapster;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Handlers.Queries;

public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, CourseDetailDto>
{
    private readonly ICourseProvider _provider;
    private readonly ICourseService _service;

    public GetCourseByIdHandler(ICourseProvider provider, ICourseService service)
    {
        _provider = provider;
        _service  = service;
    }

    public async Task<CourseDetailDto> Handle(GetCourseByIdQuery query, CancellationToken cancellationToken)
    {
        // 1. Pobierz kurs z Include na User i Category
        var course = await _provider.GetCourseById(query.IdCourse);

        // 2. Trzy warunki przez Service — kolejność ma znaczenie
        _service.EnsureCourseExists(course);     // null          → 404
        _service.EnsureCourseIsActive(course!);  // IsActive=false  → 404
        _service.EnsureCourseIsVisible(course!); // IsVisible=false → 404

        // 3. Zmapuj i zwróć
        return course!.Adapt<CourseDetailDto>();
    }
}
