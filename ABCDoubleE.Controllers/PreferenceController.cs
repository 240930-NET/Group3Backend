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
}
