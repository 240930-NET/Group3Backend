using ABCDoubleE.Models;

namespace ABCDoubleE.Services;
public interface IReviewService
{     
    public Task<IEnumerable<Review>> GetAllReviewsAsync();
    //public Task<IEnumerable<Review>> GetAllReviewsByUser(User user);
    //public Task<IEnumerable<Review>> GetAllReviewsByBook(Book book);
    public Task<Review?> GetReviewByIdAsync(int reviewId);
    public Task AddReviewAsync(Review review);
    public Task UpdateReviewAsync(Review review);
    public Task DeleteReviewByIdAsync(int reviewId);
}