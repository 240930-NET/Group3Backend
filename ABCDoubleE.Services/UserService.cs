using ABCDoubleE.DTOs;
using ABCDoubleE.Models;
using ABCDoubleE.Repositories;

namespace ABCDoubleE.Services;

public class UserService : IUserService {

    public readonly IUserRepository _userRepo;

    public UserService(IUserRepository userRepo) {
        _userRepo = userRepo;
    }


    public async Task<List<User>> GetAllUsers() {
        return await _userRepo.GetAllUsers();
    }

    public async Task<User> GetUserById(int id) {
        User searchedUser = await _userRepo.GetUserById(id);
        if (searchedUser == null) {
            throw new Exception($"No user found with id {id}");
        }
        else {
            return searchedUser;
        }
    }

    public async Task<User> AddUser(UserDTO userDTO) {

        User user = new(){
            fullName = userDTO.fullName,
            userName = userDTO.userName,
            password = userDTO.password
        };

        if (string.IsNullOrEmpty(user.userName) || string.IsNullOrEmpty(user.password) || string.IsNullOrEmpty(user.fullName)) {
            throw new Exception("Cannot have empty name, username, or password");
        }

        return await _userRepo.GetUserByUserNameAsync(userName); //make sure that this can return null to work with authentication. Don;t throw exception here!
    }

public async Task<User> AddUser(User user) {
    if (string.IsNullOrEmpty(user.userName) || string.IsNullOrEmpty(user.passwordHash) || string.IsNullOrEmpty(user.fullName)) {
        throw new Exception("Cannot have empty name, username, or password");
    }

    try
    {
        var existingUser = await _userRepo.GetUserByUserNameAsync(user.userName);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Username already exists.");
        }

        return await _userRepo.AddUser(user);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in AddUser: {ex.Message}");
        throw;
    }
}


    public async Task<User> UpdateUser(UserDTO userDTO, int id) {
        User searchedUser = await _userRepo.GetUserById(id);
        if (searchedUser == null) {
            throw new Exception($"No user with id {id}");
        }
        else if(string.IsNullOrEmpty(userDTO.userName) || string.IsNullOrEmpty(userDTO.password) || string.IsNullOrEmpty(userDTO.fullName)) {
            throw new Exception("Cannot have empty name, username, or password");
        }
        else {
            searchedUser.fullName = userDTO.fullName;
            searchedUser.userName = userDTO.userName;
            searchedUser.password = userDTO.password;
            return await _userRepo.UpdateUser(searchedUser);
        }
    }

    public async Task DeleteUser(int id) {
        User searchedUser = await _userRepo.GetUserById(id);
        if (searchedUser == null) {
            throw new Exception($"No user with id: {id}");
        }
        else {
            await _userRepo.DeleteUser(searchedUser);
        }
    }



}