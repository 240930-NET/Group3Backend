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

    //reason for change. When user is not found in stead of throwing exception which cause => 500 interal server
    // you want to handle it in controller to throw 404: user not Found. so you should return null and handle it
    // in controller
    public async Task<User> GetUserById(int id) {
        //User searchedUser = await _userRepo.GetUserById(id);
       // if (searchedUser == null) {
        //    throw new Exception($"No user found with id {id}");
        //}
        //return searchedUser;
        return await _userRepo.GetUserById(id)!;
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


        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username cannot be null or empty", nameof(userName));
            }

            return await _userRepo.GetUserByUserNameAsync(userName); //make sure that this can return null to work with authentication. Don;t throw exception here!
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            if (user == null || user.userId == 0)
            {
                throw new ArgumentException("Invalid user data.");
            }

            try
            {
                var existingUser = await _userRepo.GetUserById(user.userId);
                if (existingUser == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }
                // Update properties
                existingUser.userName = user.userName;
                existingUser.fullName = user.fullName;
                existingUser.passwordHash = user.passwordHash;
                existingUser.passwordSalt = user.passwordSalt;
                if(string.IsNullOrEmpty(user.userName) || string.IsNullOrEmpty(user.passwordHash) || string.IsNullOrEmpty(user.fullName)) 
                {
                     throw new Exception("Cannot have empty name, username, or password");
                }
                // not sure if i remove the null check since library and preference are always created when user is created
                if (user.library != null)
                {
                    existingUser.library = user.library;
                }
                if (user.preference != null)
                {
                    existingUser.preference = user.preference;
                }
                await _userRepo.UpdateUser(existingUser);

                return existingUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateUserAsync: {ex.Message}");
                throw;
            }
        }


    public async Task<User> UpdateUser(UserDTO userDTO, int id) {
        User searchedUser = await _userRepo.GetUserById(id);
        if (searchedUser == null) {
            throw new Exception($"No user with id {id}");
        }
        else if(string.IsNullOrEmpty(userDTO.userName) || string.IsNullOrEmpty(userDTO.passwordHash) || string.IsNullOrEmpty(userDTO.fullName)) {
            throw new Exception("Cannot have empty name, username, or password");
        }
        else {
            searchedUser.fullName = userDTO.fullName;
            searchedUser.userName = userDTO.userName;
            searchedUser.passwordHash = userDTO.passwordHash;
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