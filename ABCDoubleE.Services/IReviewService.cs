using ABCDoubleE.DTOs;
using ABCDoubleE.Models;

namespace ABCDoubleE.Services;
public interface IReviewService
{     
    public Task<IEnumerable<Review>> GetAllReviewsAsync();
    //TODO: public Task<IEnumerable<Review>> GetAllReviewsByUser(User user);
    //TODO: public Task<IEnumerable<Review>> GetAllReviewsByBook(Book book);
    public Task<Review?> GetReviewByIdAsync(int reviewId);
    public Task AddReviewAsync(ReviewCreateDTO reviewCreateDTO);
    public Task UpdateReviewAsync(Review review);
    public Task DeleteReviewByIdAsync(int reviewId);
}