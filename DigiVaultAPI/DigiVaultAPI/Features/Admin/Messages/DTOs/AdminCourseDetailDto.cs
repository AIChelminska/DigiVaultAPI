using DigiVaultAPI.Features.Courses.Messages.DTOs;

namespace DigiVaultAPI.Features.Admin.Messages.DTOs;

public class AdminCourseDetailDto : CourseDetailDto
{
    public bool IsActive { get; set; }
    public bool IsVisible { get; set; }
    public int ReportsCount { get; set; }
}