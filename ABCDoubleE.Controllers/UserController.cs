using ABCDoubleE.DTOs;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ABCDoubleE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller{

    public readonly IUserService _userService;

    public UserController(IUserService userService) {
        _userService = userService;
    }
    //temporary controller for testing. Need to update later for user page.
    [HttpGet("profile")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public async Task<IActionResult> GetUserProfile()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }
        var user = await _userService.GetUserById(userId);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(new { fullName = user.fullName, userName = user.userName });
    }

    [HttpGet("userId")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public async Task<IActionResult> GetUserId()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }
        var user = await _userService.GetUserById(userId);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(userId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers() {

        try {
            return Ok(await _userService.GetAllUsers());
        }
        catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

     
    [HttpGet("GetUserById/{id}")]
    public async Task<IActionResult> GetUserById(int id) {
        try {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(await _userService.GetUserById(id));
        }
        catch(Exception e) {
            return BadRequest(e.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO) {
        try {
            User user = new(){
                fullName = userDTO.fullName,
                userName = userDTO.userName,
                passwordHash = userDTO.passwordHash,
                library = new Library()
             };
            await _userService.AddUser(user);
            return Ok(userDTO);
        }
        catch(Exception e) {
            return BadRequest(e.Message);
        }
    }


    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromBody] UserRegisterDTO userRegisterDTO) {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }

        try {
            await _userService.UpdateUser(userRegisterDTO, userId);
            return Ok(userRegisterDTO);
        }
        catch(Exception e) {
            return BadRequest(e.Message);
        }
    }


    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser() {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized("User ID not found in token or invalid format.");
        }

        try {
            await _userService.DeleteUser(userId);
            return Ok();
        }
        catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

}