using ABCDoubleE.DTOs;
using ABCDoubleE.Models;

namespace ABCDoubleE.Services;

public interface IUserService {

    public Task<List<User>> GetAllUsers();

    public Task<User> GetUserById(int id);
    public Task<User> GetUserByUserNameAsync(string userName);

    public Task<User> AddUser(User user);

    public Task<User> UpdateUserAsync(User user);
    public Task<User> UpdateUser(UserRegisterDTO userDTO, int id);

    public Task DeleteUser(int id);

}