using DigiVaultAPI.Features.Courses.Messages.Commands;
using DigiVaultAPI.Features.Courses.Providers;
using DigiVaultAPI.Models;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Handlers.Commands;

public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, int>
{
    private readonly ICourseProvider _provider;

    public CreateCourseHandler(ICourseProvider provider)
    {
        _provider = provider;
    }

    public async Task<int> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        // 1. Zbuduj Course — IdUser z JWT (Controller wstrzyknął do Command)
        //    CreatedAt NIE jest ustawiane — baza ustawia HasDefaultValueSql("NOW()")
        var course = new Course
        {
            Title       = command.Title,
            Description = command.Description,
            Price       = command.Price,
            ImageUrl    = command.ImageUrl,
            IdCategory  = command.IdCategory,
            IdUser      = command.IdUser,
            IsActive    = true,
            IsVisible   = true      // nowy kurs od razu widoczny
        };

        // 2. Zapisz i zwróć IdCourse (Controller zwróci 201 Created)
        var idCourse = await _provider.CreateCourse(course);
        return idCourse;
    }
}
