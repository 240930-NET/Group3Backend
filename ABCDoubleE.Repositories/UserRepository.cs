using ABCDoubleE.Data;
using ABCDoubleE.Models;
using Microsoft.EntityFrameworkCore;

namespace ABCDoubleE.Repositories;

public class UserRepository : IUserRepository {

    private readonly ABCDoubleEContext _context;

    public UserRepository(ABCDoubleEContext context) {
        _context = context;
    }


    public async Task<List<User>> GetAllUsers() {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserById(int id) {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> GetUserByUserNameAsync(string userName) 
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.userName == userName);
    }
    /*
    public async Task<User> AddUser(User user) {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }*/

    public async Task<User> AddUser(User user) {
    try {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    } catch (Exception ex) {
        Console.WriteLine($"Error in UserRepository.AddUser: {ex.Message}");
        throw;
    }
}


    public async Task<User> UpdateUser(User user) {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUser(User user) {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }


}