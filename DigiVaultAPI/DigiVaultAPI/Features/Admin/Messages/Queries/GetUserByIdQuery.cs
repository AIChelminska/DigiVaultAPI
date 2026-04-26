using DigiVaultAPI.Features.Admin.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Queries;

public class GetUserByIdQuery : IRequest<AdminUserDetailDto>
{
    public int IdUser { get; set; }
}