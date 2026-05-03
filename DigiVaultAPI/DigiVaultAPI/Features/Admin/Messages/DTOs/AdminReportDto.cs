using DigiVaultAPI.Features.Courses.Messages.DTOs;

namespace DigiVaultAPI.Features.Admin.Messages.DTOs;

public class AdminReportDto
{
    public int IdCourseReport { get; set; }
    public int IdCourse { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string ReportedBy { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsResolved { get; set; } = false;
}