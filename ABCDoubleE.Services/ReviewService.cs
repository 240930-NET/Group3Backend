using ABCDoubleE.Models;
using ABCDoubleE.Repositories;

namespace ABCDoubleE.Services;
public class ReviewService : IReviewService
{     
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<Review>> GetAllReviewsAsync(){
        var allReviews = await _reviewRepository.GetAllReviewsAsync();
        if(allReviews.Count() == 0){
            throw new Exception("No reviews found");
        }
        return allReviews;
    }

    public async Task<Review?> GetReviewByIdAsync(int reviewId){
        var review =  await _reviewRepository.GetReviewByIdAsync(reviewId);
        if( review == null){
            throw new Exception($"No review found with id {reviewId}");
        }
        else{
            return review;
        }
    }

    public async Task AddReviewAsync(Review review){
        if(review.rating < 0){
            throw new Exception("A valid rating is required.");
        }
        else if(review.review == "" || review.review == null){
            throw new Exception("Missing review.");
        }
        else{
            await _reviewRepository.AddReviewAsync(review);
        }
    }

    public async Task UpdateReviewAsync(Review review){
        if(await _reviewRepository.GetReviewByIdAsync(review.reviewId) == null){
            throw new Exception($"Invalid. Cannot update review to null.");
        }
        else if(review.rating < 0){
            throw new Exception("A valid rating is required.");
        }
        else if(review.review == "" || review.review == null){
            throw new Exception("Missing review.");
        }
        else{
            await _reviewRepository.UpdateReviewAsync(review);
        }
    }

    public async Task DeleteReviewByIdAsync(int reviewId){
        var review = await _reviewRepository.GetReviewByIdAsync(reviewId);
        if(review == null){
            throw new Exception("Review does not exist.");
        }
        else{
            await _reviewRepository.DeleteReviewByIdAsync(reviewId);
        }
    }
}