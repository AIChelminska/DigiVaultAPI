using DigiVaultAPI.Data;
using DigiVaultAPI.Exceptions;
using DigiVaultAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace DigiVaultAPI.Features.Review.Services;

public class ReviewService : IReviewService
{
    private readonly DigiVaultDbContext _context;

    public ReviewService(DigiVaultDbContext context)
    {
        _context = context;
    }

    public async Task AddReview(int idUser, int idCourse, int rating, string? comment)
    {
        var isMine = await _context.UserCourses
            .AnyAsync(uc => uc.IdUser == idUser && uc.IdCourse == idCourse);
        if(!isMine)
        {
            throw new ForbiddenException("You are not the owner of this course");
        }

        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.IdUser == idUser && r.IdCourse == idCourse);
        bool isNew = review == null;
        int oldRating = review?.Rating ?? 0;
        if(review == null)
        {
            review = new DigiVaultAPI.Models.Review
            {
                IdUser = idUser,
                IdCourse = idCourse,
                Rating = rating,
                Comment = comment
            };
            
            _context.Reviews.Add(review);
        }
        else 
        {
            review.Rating = rating;
            review.Comment = comment;
        }

        var course = await _context.Courses.FirstOrDefaultAsync(c => c.IdCourse == idCourse);
        if(course != null)
        {
            if (isNew)
            {
                course.AverageRating = (course.AverageRating * course.RatingsCount + rating) / (course.RatingsCount + 1);
                course.RatingsCount++;
            }
            else
            {
                course.AverageRating = (course.AverageRating * course.RatingsCount - oldRating + rating) / course.RatingsCount;
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteReview(int idUser, int idCourse)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.IdUser == idUser && r.IdCourse == idCourse);
        if(review == null)
        {
            throw new NotFoundException("Review not found");
        }
        _context.Reviews.Remove(review);
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.IdCourse == idCourse);
        if(course != null)
        {
            if (course.RatingsCount > 1)
            {
                course.AverageRating = (course.AverageRating * course.RatingsCount - review.Rating) / (course.RatingsCount - 1);
            }
            else
            {
                course.AverageRating = 0;
            }
            course.RatingsCount--;
        }
        await _context.SaveChangesAsync();
    }


}