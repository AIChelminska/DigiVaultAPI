

namespace DigiVaultAPI.Features.Reports.Services;

public interface IReportService
{
    Task AddReport(int idUser, int idCourse, string reason);
}