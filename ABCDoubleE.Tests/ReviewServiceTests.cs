using ABCDoubleE.Repositories;
using ABCDoubleE.Services;
using ABCDoubleE.Models;
using ABCDoubleE.DTOs;
using Moq;

namespace ABCDoubleE.Tests;

public class ReviewServiceTests
{

    //GetAllReviewsByBookIdAsync
    //AddReviewAsync(ReviewCreateDTO reviewCreateDTO)

    [Fact]
    public async Task GetAllReviewsAsyncReturnsProperList(){
        Mock<IReviewRepository> mockRepo = new();
        ReviewService reviewService = new(mockRepo.Object);

        List<Review> rList = [
            new Review {reviewId = 1, rating = 4, reviewText = "Eh."},
            new Review {reviewId = 2, rating = 10, reviewText = "Wow!"},
            new Review {reviewId = 3, rating = 1, reviewText = "So bad..."},
            new Review {reviewId = 4, rating = 7, reviewText = "I liked it."}
        ];

        mockRepo.Setup(repo => repo.GetAllReviewsAsync()).ReturnsAsync(rList);

        var result = await reviewService.GetAllReviewsAsync();

        Assert.NotNull(result);
        Assert.Equal(4, result.Count());
        Assert.Contains(result, e => e.reviewText!.Equals("Wow!"));
    }

    [Fact]
    public async Task GetAllReviewsAsyncThrowsExceptionOnEmpty(){
        Mock<IReviewRepository> mockRepo = new();
        ReviewService reviewService = new(mockRepo.Object);

        List<Review> rList = [];

        mockRepo.Setup(repo => repo.GetAllReviewsAsync()).ReturnsAsync(rList);

        Assert.ThrowsAsync<Exception>(() => reviewService.GetAllReviewsAsync());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public async Task GetReviewByIdAsyncReturnsProperReview(int id){
        Mock<IReviewRepository> mockRepo = new();
        ReviewService reviewService = new(mockRepo.Object);

        List<Review> rList = [
            new Review {reviewId = 1, rating = 4, reviewText = "Eh."},
            new Review {reviewId = 2, rating = 10, reviewText = "Wow!"},
            new Review {reviewId = 3, rating = 1, reviewText = "So bad..."},
            new Review {reviewId = 4, rating = 7, reviewText = "I liked it."}
        ];

        mockRepo.Setup(repo => repo.GetReviewByIdAsync(It.IsAny<int>()))!
            .ReturnsAsync(rList.FirstOrDefault(review => review.reviewId == id));

        var result = await reviewService.GetReviewByIdAsync(id);

        Assert.NotNull(result);
        Assert.IsType<Review>(result);
        Assert.Equal(id, result.reviewId);
    }

    [Fact]
    public async Task GetReviewByIdAsyncThrowsExceptionOnNull(){
        Mock<IReviewRepository> mockRepo = new();
        ReviewService reviewService = new(mockRepo.Object);

        List<Review> rList = [
            new Review {reviewId = 1, rating = 4, reviewText = "Eh."},
            new Review {reviewId = 2, rating = 10, reviewText = "Wow!"},
            new Review {reviewId = 3, rating = 1, reviewText = "So bad..."},
            new Review {reviewId = 4, rating = 7, reviewText = "I liked it."}
        ];

        mockRepo.Setup(repo => repo.GetReviewByIdAsync(It.IsAny<int>()))!
            .ReturnsAsync(rList.FirstOrDefault(review => review.bookId == 500));


        Assert.ThrowsAsync<Exception>(() => reviewService.GetReviewByIdAsync(500));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(4)]
    public async Task GetAllReviewsByBookIdAsyncReturnsProperList(int bookId){
        Mock<IReviewRepository> mockRepo = new();
        ReviewService reviewService = new(mockRepo.Object);

        List<Review> rList = [
            new Review {reviewId = 1, rating = 4, reviewText = "Eh.", userId = 2, bookId = 4},
            new Review {reviewId = 2, rating = 10, reviewText = "Wow!", userId = 1, bookId = 2},
            new Review {reviewId = 3, rating = 1, reviewText = "So bad...", userId = 1, bookId = 4},
            new Review {reviewId = 4, rating = 7, reviewText = "I liked it.", userId = 1, bookId = 1}
        ];

        var numBooks = 0;
        for(int i = 0; i < rList.Count(); i++){
            if(rList[i].bookId == bookId){
                numBooks += 1;
            }
        }

        mockRepo.Setup(repo => repo.GetAllReviewsByBookIdAsync(It.IsAny<int>()))!
            .ReturnsAsync(rList.FindAll(review => review.bookId == bookId));

        var result = await reviewService.GetAllReviewsByBookIdAsync(bookId);

        Assert.NotNull(result);
        Assert.True(result.Count() == numBooks);
    }

    [Fact]
    public async Task GetAllReviewsByBookIdAsyncThrowsExceptionOnEmpty(){
        Mock<IReviewRepository> mockRepo = new();
        ReviewService reviewService = new(mockRepo.Object);

        List<Review> rList = [
            new Review {reviewId = 1, rating = 4, reviewText = "Eh.", userId = 2, bookId = 4},
            new Review {reviewId = 2, rating = 10, reviewText = "Wow!", userId = 1, bookId = 2},
            new Review {reviewId = 3, rating = 1, reviewText = "So bad...", userId = 1, bookId = 4},
            new Review {reviewId = 4, rating = 7, reviewText = "I liked it.", userId = 1, bookId = 1}
        ];

        mockRepo.Setup(repo => repo.GetAllReviewsByBookIdAsync(It.IsAny<int>()))!
            .ReturnsAsync(rList.FindAll(review => review.bookId == 500));


        Assert.ThrowsAsync<Exception>(() => reviewService.GetAllReviewsByBookIdAsync(500));
    }

    /**
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public async Task DeleteReviewByIdDeletesProperReview(int id){
        Mock<IReviewRepository> mockRepo = new();
        ReviewService reviewService = new(mockRepo.Object);

        List<Review> rList = [
            new Review {reviewId = 1, rating = 4, reviewText = "Eh."},
            new Review {reviewId = 2, rating = 10, reviewText = "Wow!"},
            new Review {reviewId = 3, rating = 1, reviewText = "So bad..."},
            new Review {reviewId = 4, rating = 7, reviewText = "I liked it."}
        ];

        Review reviewToDelete = rList.First(review => review.reviewId == id);

        mockRepo.Setup(repo => repo.DeleteReviewByIdAsync(It.IsAny<int>()))
            .Callback(() => rList.Remove(reviewToDelete));
        mockRepo.Setup(repo => repo.GetReviewByIdAsync(It.IsAny<int>()))!
            .ReturnsAsync(rList.FirstOrDefault(review => review.reviewId == id));

        reviewService.DeleteReviewByIdAsync(id);

        Assert.DoesNotContain(reviewToDelete, rList);
    }
    */
}