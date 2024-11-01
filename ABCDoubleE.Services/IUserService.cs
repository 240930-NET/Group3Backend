using ABCDoubleE.Models;

namespace ABCDoubleE.Services;

public interface IUserService {

    public Task<List<User>> GetAllUsers();

    public Task<User> GetUserById(int id);
    public Task<User> GetUserByUserNameAsync(string userName);

    public Task<User> AddUser(User user);

    public Task<User> UpdateUser(User user);

    public Task DeleteUser(int id);

}