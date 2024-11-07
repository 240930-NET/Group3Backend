using System.ComponentModel.DataAnnotations;
using ABCDoubleE.DTOs;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ABCDoubleE.Data;

namespace ABCDoubleE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookshelfController : Controller
{
    private readonly IBookshelfService _bookshelfservice;

    public BookshelfController(IBookshelfService bookshelfservice)
    {
        _bookshelfservice = bookshelfservice;

    }

     [HttpGet]
     public IActionResult GetAllBookshelfRecords(){
        try
        {
            return Ok(_bookshelfservice.GetAllBookshelfRecords());

        }
        catch(Exception e){
            return StatusCode(500, e.Message);

        }

     }

     [HttpGet("GetBookshelfById/{bookshelfId}")]
     public IActionResult GetBookshelfById(int bookshelfId){
        try{
            Bookshelf bookshelf = _bookshelfservice.GetBookshelfByID(bookshelfId);
            return Ok(bookshelf);


        }
         catch(Exception e){
            return StatusCode(500, e.Message);
         }
     }
     [HttpPost]
     public IActionResult AddBookshelf([FromBody]newBookshelfDTO newbookshelfDTO){
        try{
            _bookshelfservice.AddBookshelf(newbookshelfDTO);
            return Ok(newbookshelfDTO);
        }
        catch(Exception e){
            return BadRequest("Could not add bookshelf "+e.Message);

        }
     }
     [HttpPut]
     public IActionResult UpdateBookshelf([FromBody]Bookshelf bookshelf){
        try{
            _bookshelfservice.UpdateBookshelf(bookshelf);
            return Ok(bookshelf);
        }
        catch(Exception e){
            return BadRequest("Could not update bookshelf" + e.Message);
        }
     }


     [HttpDelete]
     public IActionResult DeleteBookshelf(int bookshelfId){
        try{
            _bookshelfservice.DeleteBookshelf(bookshelfId);
            return Ok("Bookshelf deleted");
        }
        catch(Exception e) {
            return BadRequest("Could not delete bookshelf"+e.Message);

        }
     }

    [HttpGet("{bookshelfId}/books")]
    public async Task<IActionResult> GetBooksByBookshelfId(int bookshelfId)
    {
        var books = await _bookshelfservice.GetBooksByBookshelfIdAsync(bookshelfId);
        if (books == null)
        {
            return NotFound($"No books found for bookshelf with ID {bookshelfId}");
        }

        // Transform database book authors and genres to lists of names
        var transformedBooksFromDatabase = books.Select(book => new
        {
            book.bookId,
            book.isbn,
            book.title,
            book.description,
            book.image,
            authors = book.bookAuthors.Select(ba => ba.author.name).ToList(), // Transform authors to list of names
            genres = book.bookGenres.Select(bg => bg.genre.name).ToList(), // Transform genres to list of names
            book.bookshelfBooks,
            book.reviewList
        }).ToList();

        return Ok(transformedBooksFromDatabase);
    }

    [HttpPost("{bookshelfId}/addBook")]
    public async Task<IActionResult> AddBookToBookshelf(int bookshelfId, [FromBody] BookExternalDTO bookExternalDTO)
    {
        var result = await _bookshelfservice.AddBookToBookshelfAsync(bookshelfId, bookExternalDTO);

        if (!result)
        {
            return BadRequest("Failed to add the book to the bookshelf.");
        }

        return Ok("Book successfully added to the bookshelf.");
    }

   

}

