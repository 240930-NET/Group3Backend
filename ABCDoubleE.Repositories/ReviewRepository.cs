using Microsoft.EntityFrameworkCore;
using ABCDoubleE.Data;
using ABCDoubleE.Models;

namespace ABCDoubleE.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ABCDoubleEContext _context;

    public ReviewRepository(ABCDoubleEContext context) => _context = context;

    public async Task<IEnumerable<Review>> GetAllReviewsAsync(){

        return await _context.Reviews
            .Include(r => r.user)
            .Include(r => r.book)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetAllReviewsByBookIdAsync(int bookId){
        return await _context.Reviews
            .Where(r => r.bookId == bookId)
            .Include(r => r.user)
            .Include(r => r.book)
            .ToListAsync();
    }

    public async Task<Review?> GetReviewByIdAsync(int reviewId){
        return await _context.Reviews
            .Include(r => r.user)
            .Include(r => r.book)
            .FirstOrDefaultAsync(r => r.reviewId == reviewId);
    }

    public async Task AddReviewAsync(Review review){
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateReviewAsync(Review review){
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReviewByIdAsync(int reviewId){
        var review = await _context.Reviews.FindAsync(reviewId);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}