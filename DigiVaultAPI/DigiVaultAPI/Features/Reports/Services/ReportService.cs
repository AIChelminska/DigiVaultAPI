using DigiVaultAPI.Data;
using DigiVaultAPI.Models;


namespace DigiVaultAPI.Features.Reports.Services;

public class ReportService : IReportService
{
    private readonly DigiVaultDbContext _context;

    public ReportService(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task AddReport(int idUser, int idCourse, string reason)
    {
        var report = new CourseReport
        {
            IdUser = idUser,
            IdCourse = idCourse,
            Reason = reason
        };

        _context.CourseReports.Add(report);
        await _context.SaveChangesAsync();
    }
}