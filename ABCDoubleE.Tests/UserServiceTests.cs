
using ABCDoubleE.DTOs;
using ABCDoubleE.Models;
using ABCDoubleE.Repositories;
using ABCDoubleE.Services;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;
using Moq;

public class UserServiceTests {

    private readonly Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();


    [Fact]
    public async Task GetAllUsersReturnsProperList() {
        //Arrange
        UserService userService = new(mockUserRepo.Object);

        List<User> userList = [
            new User {fullName = "Bob Smith", userName = "Bob's username"}
        ];

        mockUserRepo.Setup(repo => repo.GetAllUsers())
            .ReturnsAsync(userList);

        //Act
        var result = await userService.GetAllUsers();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
        Assert.Contains(result, e => e.fullName.Equals("Bob Smith"));
    }


    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetUserByIdReturnsProperUser(int id)
    {
        //Arrange
        UserService userService = new(mockUserRepo.Object);

        List<User> userList = [
            new User {userId = 1, fullName = "Joe B"},
            new User {userId = 2, fullName = "Sue T"}, 
            new User {userId = 3, fullName = "Jeff R"}
        ];

        mockUserRepo.Setup(repo => repo.GetUserById(It.IsAny<int>()))!
            .ReturnsAsync(userList.FirstOrDefault(user => user.userId == id));

        //Act
        var result = await userService.GetUserById(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<User>(result);
        Assert.Equal(id, result.userId);
    }


    [Fact]
    public async Task AddUserThrowsExceptionWhenMissingField() {
        //Arrange
        UserService userService = new(mockUserRepo.Object);
        User newUser = new User{userName = "testUserName"};

        //Act & Assert
        await Assert.ThrowsAsync<Exception>(() => userService.AddUser(newUser));
    }

    [Fact]
    public async Task GetUserByUserNameAsyncThrowsExceptionWhenNoUserName() {
        //Arrange
        UserService userService = new(mockUserRepo.Object);

        //Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => userService.GetUserByUserNameAsync(""));
    }


    [Fact]
    public async Task UpdateUserAsyncReturnsUpdatedUser() {
        //Arrange
        UserService userService = new(mockUserRepo.Object);

        User searchedUser = new User{userId = 1, fullName = "Mary King", userName = "MKing", passwordHash = "mbielsfj", passwordSalt = "jfdklsfj3784"};
        User updatedUser = new User{userId = 1, fullName = "updated1", userName = "updated2", passwordHash = "bkvc;orujfm", passwordSalt = "49485920"};

        mockUserRepo.Setup(repo => repo.GetUserById(1))
            .ReturnsAsync(searchedUser);

        mockUserRepo.Setup(repo => repo.UpdateUser(updatedUser))
            .ReturnsAsync(updatedUser);

        //Act
        var result = await userService.UpdateUserAsync(updatedUser);

        //Assert
        Assert.Equal(updatedUser.fullName, result.fullName);
        Assert.Equal(updatedUser.userName, result.userName);
        Assert.Equal(updatedUser.passwordHash, result.passwordHash);
        Assert.Equal(updatedUser.passwordSalt, result.passwordSalt);
    }



    [Fact]
    public async Task UpdateUserReturnsUpdatedUser() {
        //Arrange
        UserService userService = new(mockUserRepo.Object);

        User searchedUser = new User{userId = 1, fullName = "Mary King", userName = "MKing", passwordSalt = "jfdklsfj3784"};
        UserRegisterDTO user = new UserRegisterDTO{fullName = "updated1", userName = "updated2", password = "updated3"};
        User updatedUser = new User{userId = 1, fullName = "updated1", userName = "updated2"};

        mockUserRepo.Setup(repo => repo.GetUserById(1))
            .ReturnsAsync(searchedUser);

        mockUserRepo.Setup(repo => repo.UpdateUser(searchedUser))
            .ReturnsAsync(updatedUser);

        //Act
        var result = await userService.UpdateUser(user, 1);

        //Assert
        Assert.Equal(updatedUser, result);
    }

    [Fact]
    public async Task DeleteUserCallsDeleteUserWithCorrectUser() {
        //Arrange
        UserService userService = new(mockUserRepo.Object);

        User searchedUser = new User{userId = 2, fullName = "Mary King", userName = "MKing", passwordSalt = "jfdklsfj3784"};

        mockUserRepo.Setup(repo => repo.GetUserById(2))
            .ReturnsAsync(searchedUser);

        //Act
        await userService.DeleteUser(2);

        //Assert
        mockUserRepo.Verify(repo => repo.DeleteUser(searchedUser), Times.Once);

    }


}