using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using System.Collections.Generic;

public class AuthenticationServiceTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly IConfiguration _configuration;
    private readonly AuthenticationService _authService;

    public AuthenticationServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"JwtSecretKey", "ABCDoubleEABCDoubleEABCDoubleEABCDoubleE"}
        };
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        _mockUserService = new Mock<IUserService>();
        _authService = new AuthenticationService(_mockUserService.Object, _configuration);
    }

    [Fact]
    public async Task RegisterUserAsync_ValidUser_ReturnsUser()
    {
        // Arrange
        var userName = "testUser";
        var password = "Password123!";
        var fullName = "Test User";

        // Setup mock to return null when checking if user exists
        _mockUserService.Setup(x => x.GetUserByUserNameAsync(userName))
            .ReturnsAsync((User)null);

        _mockUserService.Setup(x => x.AddUser(It.IsAny<User>()))
            .ReturnsAsync((User u) => u); 

        // Act
        var result = await _authService.RegisterUserAsync(userName, password, fullName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userName, result.userName);
        Assert.Equal(fullName, result.fullName);
        _mockUserService.Verify(x => x.AddUser(It.IsAny<User>()), Times.Once);
    }
}
/*
    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsJwtToken()
    {

        // Arrange
        var userName = "testUser";
        var password = "Password123!";
        var salt = "SampleSalt";
        var hash = _authService.HashPassword(password).hash;

        var user = new User
        {
            userId = 1,
            userName = userName,
            passwordSalt = salt,
            passwordHash = hash
        };

        _mockUserService.Setup(x => x.GetUserByUserNameAsync(userName))
            .ReturnsAsync(user);

        // Act
        var token = await _authService.LoginAsync(userName, password);

        // Assert
        Assert.False(string.IsNullOrEmpty(token));
    }

    [Fact]
    public async Task LoginAsync_InvalidPassword_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var userName = "testUser";
        var password = "WrongPassword";
        var user = new User
        {
            userName = userName,
            passwordSalt = "SampleSalt",
            passwordHash = "SomeHash"
        };

        _mockUserService.Setup(x => x.GetUserByUserNameAsync(userName))
            .ReturnsAsync(user);
        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.LoginAsync(userName, password));
    }
}
*/
