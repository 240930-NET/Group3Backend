using ABCDoubleE.Models;
using ABCDoubleE.Repositories;
using ABCDoubleE.DTOs;

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

    public async Task AddReviewAsync(ReviewCreateDTO reviewCreateDTO){
        if(reviewCreateDTO.rating < 0){
            throw new Exception("A valid rating is required.");
        }
        else if(reviewCreateDTO.reviewText == "" || reviewCreateDTO.reviewText == null){
            throw new Exception("Missing review.");
        }
        if (reviewCreateDTO.bookId != null && reviewCreateDTO.bookId > 0) {
            throw new Exception("bookId is invalid.");
        }

        else{
            var review = new Review
                {
                rating = reviewCreateDTO.rating,
                reviewText = reviewCreateDTO.reviewText,
                bookId = reviewCreateDTO.bookId,
                userId = reviewCreateDTO.userId
                };
            
            await _reviewRepository.AddReviewAsync(review);
        }
    }

    public async Task UpdateReviewAsync(Review review){
        if(await _reviewRepository.GetReviewByIdAsync(review.reviewId) == null){
            throw new Exception($"Invalid. Must enter an id to update.");
        }
        else if(review.rating < 0){
            throw new Exception("A valid rating is required.");
        }
        else if(review.reviewText == null || review.reviewText == ""){
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