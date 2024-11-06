using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ABCDoubleE.Models;
using ABCDoubleE.Services;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PreferenceController : ControllerBase
{
    private readonly IPreferenceService _preferenceService;

    public PreferenceController(IPreferenceService preferenceService)
    {
        _preferenceService = preferenceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPreference()
    {
        string? userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }

        var preference = await _preferenceService.GetPreferenceByUserIdAsync(userId);
        if (preference == null)
        {
            return NotFound($"Preference for user ID {userId} not found.");
        }
        return Ok(preference);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePreference()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var preference = await _preferenceService.CreatePreferenceAsync(userId);
        return CreatedAtAction(nameof(GetPreference), new { userId = preference.userId }, preference);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePreference([FromBody] Preference updatedPreference)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        if (userId != updatedPreference.userId)
        {
            return BadRequest("User ID mismatch.");
        }
        await _preferenceService.UpdatePreferenceAsync(updatedPreference);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePreference()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _preferenceService.DeletePreferenceByUserIdAsync(userId);
        return NoContent();
    }

    [HttpPost("genre")]
    public async Task<IActionResult> AddGenreToPreference([FromBody] int genreId)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }

        bool success = await _preferenceService.AddGenreToPreferenceAsync(userId, genreId);
        return success ? Ok("Genre added to preferences") : BadRequest("Failed to add genre");
    }

    [HttpPost("author")]
    public async Task<IActionResult> AddAuthorToPreference([FromBody] int authorId)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }

        bool success = await _preferenceService.AddAuthorToPreferenceAsync(userId, authorId);
        return success ? Ok("Author added to preferences") : BadRequest("Failed to add author");
    }

    
    [HttpPost("book")]
    public async Task<IActionResult> AddBookToPreference([FromBody] int bookId)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }

        bool success = await _preferenceService.AddBookToPreferenceAsync(userId, bookId);
        return success ? Ok("Book added to preferences") : BadRequest("Failed to add book");
    }

[HttpDelete("genre/{genreId}")]
public async Task<IActionResult> RemoveGenreFromPreference(int genreId)
{
    if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
    {
        return Unauthorized("User ID not found in token or invalid format.");
    }

    bool success = await _preferenceService.RemoveGenreFromPreferenceAsync(userId, genreId);
    return success ? NoContent() : NotFound("Genre not found or not associated with preference.");
}



    [HttpDelete("author")]
    public async Task<IActionResult> RemoveAuthorFromPreference([FromBody] int authorId)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }

        bool success = await _preferenceService.RemoveAuthorFromPreferenceAsync(userId, authorId);
        return success ? NoContent() : NotFound("Author not found or not associated with preference.");
    }

    [HttpDelete("book")]
    public async Task<IActionResult> RemoveBookFromPreference([FromBody] int bookId)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }

        bool success = await _preferenceService.RemoveBookFromPreferenceAsync(userId, bookId);
        return success ? NoContent() : NotFound("Book not found or not associated with preference.");
    }
}
