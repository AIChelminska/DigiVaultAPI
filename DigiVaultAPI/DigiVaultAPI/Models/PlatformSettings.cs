namespace DigiVaultAPI.Models;

// Jedna tabela, jeden rekord — source of truth
// dla commissionRate i platformBalance
public class PlatformSettings
{
    public int IdPlatformSettings { get; set; }
    public decimal CommissionRate { get; set; }     // np. 0.05
    public decimal PlatformBalance { get; set; }
}
