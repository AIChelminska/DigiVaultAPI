namespace DigiVaultAPI.Features.Profile.Messages.DTOs;

public class UserProfileDto
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0;
    public decimal TotalWithdrawn { get; set; } = 0;
    public int WarningsCount { get; set; } = 0;
}