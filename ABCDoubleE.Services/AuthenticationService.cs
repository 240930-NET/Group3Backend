using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.Text;
using ABCDoubleE.Models;

namespace ABCDoubleE.Services;


public class AuthenticationService
{
    private readonly IUserService _userService;
    private readonly string _jwtSecretKey;

    public AuthenticationService(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _jwtSecretKey = configuration["JwtSecretKey"];
    }

public async Task<User> RegisterUserAsync(string userName, string password, string fullName)
{
    if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fullName))
    {
        throw new ArgumentException("Username, password, and full name cannot be empty.");
    }

    var existingUser = await _userService.GetUserByUserNameAsync(userName);
    if (existingUser != null)
    {
        throw new InvalidOperationException("User name exist");
    }

    try
    {
        var (salt, hash) = HashPassword(password);

        var user = new User
        {
            userName = userName,
            fullName = fullName,
            passwordSalt = salt,
            passwordHash = hash,
            library = new Library()
        };

        return await _userService.AddUser(user);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in RegisterUserAsync: {ex.Message}");
        throw;
    }
}

    public async Task<string> LoginAsync(string userName, string password)
    {
        try
        {
            var user = await _userService.GetUserByUserNameAsync(userName);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }

            if (!VerifyPassword(user, password))
            {
                throw new UnauthorizedAccessException("Invalid password.");
            }

            var token = GenerateJwtToken(user);
            return token;
        }
        catch (UnauthorizedAccessException ex)
        {
            //Console.WriteLine($"Authentication failed: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Error in AuthenticationService.LoginAsync: {ex.Message}");
            throw new InvalidOperationException("An unexpected error occurred while logging in.");
        }
    }


    private (string salt, string hash) HashPassword(string password)
    {
        using (var hmac = new HMACSHA512())
        {
            var salt = Convert.ToBase64String(hmac.Key);
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return (salt, hash);
        }
    }

    private bool VerifyPassword(User user, string password)
    {
        using (var hmac = new HMACSHA512(Convert.FromBase64String(user.passwordSalt)))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(computedHash) == user.passwordHash;
        }
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.userName),
                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
