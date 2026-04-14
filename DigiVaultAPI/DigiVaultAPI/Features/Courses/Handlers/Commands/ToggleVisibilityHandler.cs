using DigiVaultAPI.Features.Courses.Messages.Commands;
using DigiVaultAPI.Features.Courses.Providers;
using DigiVaultAPI.Features.Courses.Services;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Handlers.Commands;

public class ToggleVisibilityHandler : IRequestHandler<ToggleVisibilityCommand>
{
    private readonly ICourseProvider _provider;
    private readonly ICourseService _service;

    public ToggleVisibilityHandler(ICourseProvider provider, ICourseService service)
    {
        _provider = provider;
        _service  = service;
    }

    public async Task Handle(ToggleVisibilityCommand command, CancellationToken cancellationToken)
    {
        var course = await _provider.GetCourseByIdForEdit(command.IdCourse);
        _service.EnsureCourseExists(course);
        _service.EnsureCourseIsActive(course!);                  // usunięty → nie można zmieniać
        _service.EnsureIsAuthor(course!.IdUser, command.IdUser);

        // Ustaw widoczność — IsActive nie jest dotykane
        course!.IsVisible = command.IsVisible;  // true = pokaż, false = ukryj
        await _service.SaveChanges();
    }
}
