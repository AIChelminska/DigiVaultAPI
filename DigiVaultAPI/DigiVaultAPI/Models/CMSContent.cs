namespace DigiVaultAPI.Models;

public class CMSContent
{
    public int IdContent { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; }
}