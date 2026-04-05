namespace DigiVaultAPI.Features.Profile.Services;

public interface IProfileService
{
    Task UpdateName(int idUser, string firstName, string lastName);
    Task UpdateEmail(int idUser, string email, string password);
    Task UpdatePassword(int idUser, string password, string newPassword, string newPasswordConfirmation);
}