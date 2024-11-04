using System.ComponentModel.DataAnnotations;
using ABCDoubleE.DTOs;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LibraryController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public LibraryController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    // GET: api/Library/{libraryId}
    [HttpGet("{libraryId}")]
    public async Task<IActionResult> GetLibrary(int libraryId)
    {
        var libraryDto = await _libraryService.GetLibraryAsync(libraryId);
        if (libraryDto == null)
        {
            return NotFound($"Library with ID {libraryId} was not found.");
        }
        return Ok(libraryDto);
    }

    // GET: api/Library/{libraryId}/bookshelves
    [HttpGet("{libraryId}/bookshelves")]
    public async Task<IActionResult> GetAllBookshelves(int libraryId)
    {
        var bookshelves = await _libraryService.GetAllBookshelvesAsync(libraryId);
        return Ok(bookshelves);
    }

    // POST: api/Library/{libraryId}/bookshelves
    [HttpPost("{libraryId}/bookshelves")]
    public async Task<IActionResult> AddBookshelf(int libraryId, [FromBody] BookshelfCreateDTO bookshelfCreateDto)
    {
        try
        {
            var addedBookshelf = await _libraryService.AddBookshelfAsync(libraryId, bookshelfCreateDto);
            return CreatedAtAction(nameof(GetLibrary), new { libraryId, bookshelfId = addedBookshelf?.bookshelfId }, addedBookshelf);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    // DELETE: api/Library/{libraryId}/bookshelves/{bookshelfId}
    [HttpDelete("{libraryId}/bookshelves/{bookshelfId}")]
    public async Task<IActionResult> DeleteBookshelf(int libraryId, int bookshelfId)
    {
        var deleted = await _libraryService.DeleteBookshelfAsync(libraryId, bookshelfId);
        if (!deleted)
        {
            return NotFound($"Bookshelf with ID {bookshelfId} was not found in Library {libraryId}.");
        }
        return NoContent();
    }

    // DELETE: api/Library/{libraryId}
    [HttpDelete("{libraryId}")]
    public async Task<IActionResult> DeleteLibrary(int libraryId)
    {
        var deleted = await _libraryService.DeleteLibraryAsync(libraryId);
        if (!deleted)
        {
            return NotFound($"Library with ID {libraryId} was not found.");
        }
        return NoContent();
    }
}
