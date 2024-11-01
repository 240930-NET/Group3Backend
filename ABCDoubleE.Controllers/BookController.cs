using System.ComponentModel.DataAnnotations;
using ABCDoubleE.DTOs;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCDoubleE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : Controller{

    private readonly IBookService _bookservice;

    public BookController(IBookService bookservice){
        _bookservice = bookservice;
    }

    [HttpGet]
    public IActionResult GetAllBooks(){
        try{
            return Ok(_bookservice.GetAllBooks());
        }
        catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("GetBookByISBN/{isbn}")]
    public IActionResult GetBookByISBN(string isbn){
        try{
            Book book = _bookservice.GetBookByISBN(isbn);
                return Ok(book);
        }
        catch(Exception e){
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public IActionResult AddBook(BookDTO book){
        try{
            _bookservice.AddBook(book);
            return Ok("Book added succesfully!");
        }
        catch(Exception e){
            return BadRequest("Could not add book. "+e.Message);
        }
    }

    [HttpDelete("DeleteBook/{isbn}")]
    public IActionResult DeleteBook(string isbn){
        try{
            _bookservice.DeleteBook(isbn);
            return Ok("Book Deleted succesfully!");
        }
        catch(Exception e){
            return BadRequest("Could not delete book. "+e.Message);
        }
    }

}

