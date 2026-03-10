using DigiVaultAPI.Features.Courses.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Courses.Messages.Queries;

public class GetNewestCoursesQuery : IRequest<List<CourseListDto>> { }
