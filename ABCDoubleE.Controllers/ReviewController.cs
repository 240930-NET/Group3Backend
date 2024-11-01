using Microsoft.AspNetCore.Mvc;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using ABCDoubleE.DTOs;

namespace ABCDoubleE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController: Controller{
     private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService){
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> getAllReviews(){
        
        try{
            return Ok(await _reviewService.GetAllReviewsAsync());
        }
        catch(Exception e){
            return BadRequest("Could not get all reviews:" + e.Message);
        }
        
    }

    [HttpGet("GetReviewById/{reviewId}")]
    public async Task<IActionResult> getReviewById(int reviewId){
        
        try{
            return Ok(await _reviewService.GetReviewByIdAsync(reviewId));
        }
        catch(Exception e){
            return BadRequest("Could not get review by id: " + e.Message);
        }
        
    }
    /*

    [HttpPost]
    public async Task<IActionResult> AddNewReview([FromBody] ReviewCreateDTO reviewCreateDTO){
        
        try{
            await _reviewService.AddReviewAsync(reviewCreateDTO);
            return Ok("Added review.");
        }
        catch(Exception e){
            return BadRequest("Could not add review: " + e.Message);
        }
    }
    */

    [HttpPost]
    public async Task<IActionResult> AddNewReview([FromBody] ReviewCreateDTO reviewCreateDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data.");

        try
        {
            await _reviewService.AddReviewAsync(reviewCreateDTO);
            return CreatedAtAction(nameof(getReviewById), new { reviewId = reviewCreateDTO.bookId }, "Added review.");
        }
        catch (Exception e)
        {
            return StatusCode(500, "Could not add review: " + e.Message);
        }
    }


    [HttpPut]
    public async Task<IActionResult> EditReview([FromBody] Review review){
        try{
            await _reviewService.UpdateReviewAsync(review);
            return Ok("Updated review.");
        }

        catch(Exception e){
            return BadRequest("Could not update review: " + e.Message);
        }
    }

    [HttpDelete("DeleteUserById/{reviewId}")]
    public async Task<IActionResult> DeleteReview(int reviewId){
        try{    
            await _reviewService.DeleteReviewByIdAsync(reviewId);
            return Ok("Deleted review.");
        }

        catch(Exception e){
            return BadRequest("Could not delete review: " + e.Message);
        }
    }
}