using ABCDoubleE.DTOs;
using ABCDoubleE.Models;

namespace ABCDoubleE.Services;
public interface IReviewService
{     
    public Task<IEnumerable<Review>> GetAllReviewsAsync();
    public Task<IEnumerable<Review>> GetAllReviewsByBookIdAsync(int bookId);
    public Task<Review?> GetReviewByIdAsync(int reviewId);
    public Task AddReviewAsync(ReviewCreateDTO reviewCreateDTO);
    //public Task UpdateReviewAsync(Review review);
    //public Task DeleteReviewByIdAsync(int reviewId);
}