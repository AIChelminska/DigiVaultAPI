namespace DigiVaultAPI.Features.Admin.Messages.DTOs;

public class AdminCategoryDto
{
    public int IdCategory { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}