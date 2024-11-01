using ABCDoubleE.Models;

namespace ABCDoubleE.Repositories;

public interface IUserRepository{

    public Task<List<User>> GetAllUsers();

    public Task<User> GetUserById(int id);

    public Task<User> AddUser(User user);

    public Task<User> UpdateUser(User user);

    public Task DeleteUser(User user);

}