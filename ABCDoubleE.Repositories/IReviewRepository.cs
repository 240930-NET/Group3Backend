using ABCDoubleE.Models;

namespace ABCDoubleE.Repositories;

public interface IReviewRepository
{
    public Task<IEnumerable<Review>> GetAllReviewsAsync();
    public Task<Review?> GetReviewByIdAsync(int reviewId); 
    public Task AddReviewAsync(Review review);
    public Task UpdateReviewAsync(Review review);
    public Task DeleteReviewByIdAsync(int reviewId);
}