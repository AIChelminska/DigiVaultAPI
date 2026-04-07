namespace DigiVaultAPI.Features.Admin.Messages.DTOs;

public class AdminUserDto
{
    public int IdUser { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public int WarningsCount { get; set; }
    public bool IsActive { get; set; }
}