using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ABCDoubleE.Services;
using ABCDoubleE.DTOs;
using ABCDoubleE.Models;

namespace ABCDoubleE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authService;

        public AuthenticationController(AuthenticationService authService)
        {
            _authService = authService;
        }

[HttpPost("register")]
public async Task<IActionResult> Register([FromBody] UserRegisterDTO request)
{
    if (!ModelState.IsValid)
        return BadRequest("Invalid data.");

    try
    {
        var user = await _authService.RegisterUserAsync(request.userName, request.password, request.fullName);
        return Ok(new { Message = "User registered successfully." });
    }
    catch (ArgumentException ex)
    {
        return BadRequest(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        return Conflict(ex.Message);
    }
    catch (Exception ex)
    {
        // Log the exception here to get more insights
        Console.WriteLine($"Error during registration: {ex.Message}");
        return StatusCode(500, $"An error occurred while registering the user: {ex.Message}");
    }
}



        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            try
            {
                var token = _authService.Login(request.userName, request.password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while logging in.");
            }
        }
    }
}
