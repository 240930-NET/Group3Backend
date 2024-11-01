using ABCDoubleE.DTOs;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCDoubleE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller{

    public readonly IUserService _userService;

    public UserController(IUserService userService) {
        _userService = userService;
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
                passwordHash = userDTO.passwordHash
             };
            await _userService.AddUser(user);
            return Ok(userDTO);
        }
        catch(Exception e) {
            return BadRequest(e.Message);
        }
    }


    [HttpPut("UpdateUser/{id}")]
    public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO, int id) {

        try {
            await _userService.UpdateUser(userDTO, id);
            return Ok(userDTO);
        }
        catch(Exception e) {
            return BadRequest(e.Message);
        }
    }


    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(int id) {

        try {
            await _userService.DeleteUser(id);
            return Ok();
        }
        catch(Exception e) {
            return BadRequest(e.Message);
        }

    }

}