using System;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using ABCDoubleE.Services;
using ABCDoubleE.Models;

namespace ABCDoubleE.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IPreferenceService> _preferenceServiceMock;
        private readonly Mock<ILibraryService> _libraryServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthenticationService _authService;

        public AuthenticationServiceTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _preferenceServiceMock = new Mock<IPreferenceService>();
            _libraryServiceMock = new Mock<ILibraryService>();
            _configurationMock = new Mock<IConfiguration>();

            _configurationMock.Setup(c => c["JwtSecretKey"]).Returns("YourSecretKeyForTesting");

            _authService = new AuthenticationService(
                _userServiceMock.Object,
                _preferenceServiceMock.Object,
                _libraryServiceMock.Object,
                _configurationMock.Object);
        }

        [Fact]
        public async Task RegisterUserAsync_Sucessful()
        {
            // Arrange
            var userName = "testuser";
            var password = "password";
            var fullName = "Test User";
            
            _userServiceMock.Setup(u => u.GetUserByUserNameAsync(userName)).ReturnsAsync((User)null);
            _userServiceMock.Setup(u => u.AddUser(It.IsAny<User>())).ReturnsAsync((User u) => u);
            _libraryServiceMock.Setup(l => l.CreateLibraryAsync(It.IsAny<int>())).ReturnsAsync(new Library { libraryId = 1 });
            _preferenceServiceMock.Setup(p => p.CreatePreferenceAsync(It.IsAny<int>())).ReturnsAsync(new Preference { preferenceId = 1 });

            // Act
            var result = await _authService.RegisterUserAsync(userName, password, fullName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userName, result.userName);
            Assert.Equal(fullName, result.fullName);
            _userServiceMock.Verify(u => u.AddUser(It.IsAny<User>()), Times.Once);
            _libraryServiceMock.Verify(l => l.CreateLibraryAsync(result.userId), Times.Once);
            _preferenceServiceMock.Verify(p => p.CreatePreferenceAsync(result.userId), Times.Once);
        }

        [Fact]
        public async Task RegisterUserAsync_UserExists()
        {
            // Arrange
            var userName = "existinguser";
            var password = "password";
            var fullName = "Existing User";

            _userServiceMock.Setup(u => u.GetUserByUserNameAsync(userName)).ReturnsAsync(new User());

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.RegisterUserAsync(userName, password, fullName));
        }
/*
        [Fact]
        public async Task RegisterAndLogin_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var userName = "testuser";
            var password = "testpassword";
            var fullName = "Test User";

            // Mock setup for user registration
            _userServiceMock.Setup(u => u.GetUserByUserNameAsync(userName)).ReturnsAsync((User)null);
            _userServiceMock.Setup(u => u.AddUser(It.IsAny<User>())).ReturnsAsync((User u) => u);
            _libraryServiceMock.Setup(l => l.CreateLibraryAsync(It.IsAny<int>())).ReturnsAsync(new Library { libraryId = 1 });
            _preferenceServiceMock.Setup(p => p.CreatePreferenceAsync(It.IsAny<int>())).ReturnsAsync(new Preference { preferenceId = 1 });

            // Act 1: Register the user and get the exact registered user instance
            var registeredUser = await _authService.RegisterUserAsync(userName, password, fullName);

            // Assert 1: Verify registration
            Assert.NotNull(registeredUser);
            Assert.Equal(userName, registeredUser.userName);
            Assert.Equal(fullName, registeredUser.fullName);

            // Setup the mock to return the registered user when `GetUserByUserNameAsync` is called
            _userServiceMock.Setup(u => u.GetUserByUserNameAsync(userName)).ReturnsAsync(registeredUser);

            // Act 2: Log in with the registered credentials
            var token = await _authService.LoginAsync(userName, password);

            // Assert 2: Verify a token is returned
            Assert.NotNull(token);
            Assert.IsType<string>(token);
        }
*/

        [Fact]
        public async Task LoginAsync_UserNotFound()
        {
            // Arrange
            var userName = "nonexistentuser";
            var password = "password";

            _userServiceMock.Setup(u => u.GetUserByUserNameAsync(userName)).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.LoginAsync(userName, password));
        }

        [Fact]
        public async Task LoginAsync_PasswordIsInvalid()
        {
            // Arrange
            var userName = "validuser";
            var password = "invalidpassword";
            var (salt, hash) = HashPassword("validpassword"); // Hash for the correct password

            var user = new User
            {
                userName = userName,
                passwordSalt = salt,
                passwordHash = hash,
                userId = 1
            };

            _userServiceMock.Setup(u => u.GetUserByUserNameAsync(userName)).ReturnsAsync(user);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.LoginAsync(userName, password));
        }

        // Helper method for generating hash and salt to mimic your production code
        private (string salt, string hash) HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var salt = Convert.ToBase64String(hmac.Key);
                var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return (salt, hash);
            }
        }
    }
}
