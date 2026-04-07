using DigiVaultAPI.Features.Admin.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Admin.Messages.Queries;

public class GetUsersQuery : IRequest<IEnumerable<AdminUserDto>>
{}