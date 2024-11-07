using System.ComponentModel.DataAnnotations;
using ABCDoubleE.DTOs;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LibraryController : ControllerBase
{
    private readonly ILibraryService _libraryService;
    public readonly IUserService _userService;

    public LibraryController(ILibraryService libraryService, IUserService userService)
    {
        _libraryService = libraryService;
        _userService = userService;
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

        // GET: api/Library/bookshelves
    [HttpGet("bookshelves")]
    public async Task<IActionResult> GetUserBookshelves()
    {
        // Step 1: Retrieve userId from the token
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }

        // Step 2: Retrieve libraryId associated with the user
        var libraryId = await _libraryService.GetLibraryIdByUserIdAsync(userId);
        if (libraryId == null)
        {
            return NotFound("Library not found for this user.");
        }

        // Step 3: Fetch bookshelves using libraryId
        var bookshelves = await _libraryService.GetBookshelvesByLibraryIdAsync(libraryId.Value);
        if (bookshelves == null || !bookshelves.Any())
        {
            return NotFound("No bookshelves found for this user's library.");
        }

        return Ok(bookshelves);
    }

    [HttpDelete("bookshelves/{bookshelfId}")]
    public async Task<IActionResult> DeleteBookshelf(int bookshelfId)
    {
        // Step 1: Retrieve userId from the token
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }

        // Step 2: Retrieve libraryId associated with the user
        var libraryId = await _libraryService.GetLibraryIdByUserIdAsync(userId);
        if (libraryId == null)
        {
            return NotFound("Library not found for this user.");
        }

        // Step 3: Delete the bookshelf using libraryId and bookshelfId
        var deleted = await _libraryService.DeleteBookshelfAsync(libraryId.Value, bookshelfId);
        if (!deleted)
        {
            return NotFound($"Bookshelf with ID {bookshelfId} was not found in Library {libraryId}.");
        }

        return NoContent();
    }


}
