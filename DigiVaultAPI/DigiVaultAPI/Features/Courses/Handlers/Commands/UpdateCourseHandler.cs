using DigiVaultAPI.Features.Courses.Messages.Commands;
using DigiVaultAPI.Features.Courses.Providers;
using DigiVaultAPI.Features.Courses.Services;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Handlers.Commands;

public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand>
{
    private readonly ICourseProvider _provider;
    private readonly ICourseService _service;

    public UpdateCourseHandler(ICourseProvider provider, ICourseService service)
    {
        _provider = provider;
        _service  = service;
    }

    public async Task Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
    {
        // 1. Pobierz kurs do edycji (bez Include, zwraca też IsActive=false)
        var course = await _provider.GetCourseByIdForEdit(command.IdCourse);
        _service.EnsureCourseExists(course);
        _service.EnsureCourseIsActive(course!);               // usunięty przez admina → 404
        _service.EnsureIsAuthor(course!.IdUser, command.IdUser); // nie autor → 403

        // 2. Aktualizuj tylko edytowalne pola
        //    NIE dotykaj: SalesCount, RatingsCount, AverageRating, IsActive, CreatedAt, IdUser
        course!.Title       = command.Title;
        course.Description  = command.Description;
        course.Price        = command.Price;
        course.ImageUrl     = command.ImageUrl;
        course.IdCategory   = command.IdCategory;

        // 3. EF śledzi zmiany — SaveChanges wysyła UPDATE
        await _provider.SaveChanges();
    }
}
