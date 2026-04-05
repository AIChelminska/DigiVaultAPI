using DigiVaultAPI.Features.Courses.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Cart.Messages.Queries;

public class GetCartQuery : IRequest<List<CourseListDto>>
{
    public int IdUser { get; set; }  
}