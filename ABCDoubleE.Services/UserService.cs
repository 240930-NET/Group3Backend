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

    public async Task<User> AddUser(User user) {
        if (string.IsNullOrEmpty(user.userName) || string.IsNullOrEmpty(user.password) || string.IsNullOrEmpty(user.fullName)) {
            throw new Exception("Cannot have empty name, username, or password");
        }
        else {
            return await _userRepo.AddUser(user);
        }
    }

    public async Task<User> UpdateUser(User user) {
        if (await _userRepo.GetUserById(user.userId) == null) {
            throw new Exception($"No user with id {user.userId}");
        }
        else if(string.IsNullOrEmpty(user.userName) || string.IsNullOrEmpty(user.password) || string.IsNullOrEmpty(user.fullName)) {
            throw new Exception("Cannot have empty name, username, or password");
        }
        else {
            return await _userRepo.UpdateUser(user);
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