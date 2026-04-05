using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Categories.Providers;

public interface ICategoryProvider
{
    Task<List<Category>> GetCategories();
}