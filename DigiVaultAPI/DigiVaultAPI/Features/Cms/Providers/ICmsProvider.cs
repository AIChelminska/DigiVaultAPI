using DigiVaultAPI.Models;

namespace DigiVaultAPI.Features.Cms.Providers;

public interface ICmsProvider
{
    Task<List<CMSContent>> GetAll();
    Task<CMSContent?> GetByKey(string key);
}
