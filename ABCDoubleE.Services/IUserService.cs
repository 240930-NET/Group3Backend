using ABCDoubleE.DTOs;
using ABCDoubleE.Models;

namespace ABCDoubleE.Services;

public interface IUserService {

    public Task<List<User>> GetAllUsers();

    public Task<User> GetUserById(int id);

    public Task<User> AddUser(UserDTO userDTO);

    public Task<User> UpdateUser(UserDTO userDTO, int id);

    public Task DeleteUser(int id);

}