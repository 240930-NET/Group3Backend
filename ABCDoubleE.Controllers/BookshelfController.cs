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
   

}

