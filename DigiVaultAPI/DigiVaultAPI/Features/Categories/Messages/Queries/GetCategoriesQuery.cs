using DigiVaultAPI.Features.Categories.Messages.DTOs;
using MediatR;

namespace DigiVaultAPI.Features.Categories.Messages.Queries;

public class GetCategoriesQuery: IRequest<List<CategoryDto>>
{
    
}