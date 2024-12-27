using LifePlanner.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Tests;
using LifePlanner.Server;
using LifePlanner.Server.Controllers;
using LifePlanner.Server.Models;
using LifePlanner.Server.Services;
using LifePlanner.Server.Repositories;
using LifePlanner.Server.Repositories.Interfaces;
using Moq;

public class UsersControllerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly UsersController _usersController;
    private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
    private readonly LifePlannerServerContext _context;
    
    public UsersControllerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        var options = new DbContextOptionsBuilder<LifePlannerServerContext>()
            .UseInMemoryDatabase(databaseName: "LifePlannerTestDb")
            .Options;
        _context = new LifePlannerServerContext(options);

        var userRepository = new UserRepository(_context);
        var userService = new UsersService(userRepository);
        _usersController = new UsersController(userService);
    }
    
    [Fact]
    public async Task GetUsers_Returns_OkResult_With_Users()
    {
        // Arrange
        _context.Users.Add(new User { Id = 1, Auth0Id = "auth0|1", Email = "user1@example.com", Name = "User 1" });
        _context.Users.Add(new User { Id = 2, Auth0Id = "auth0|2", Email = "user2@example.com", Name = "User 2" });
        await _context.SaveChangesAsync();

        // Act
        var result = await _usersController.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var users = Assert.IsType<List<User>>(okResult.Value);
        _testOutputHelper.WriteLine(users[1].Name);
        Assert.Equal(2, users.Count);
    }
    
    [Fact]
    public async Task PostUser_Returns_CreatedAtActionResult_When_Successful()
    {
        // Arrange
        var newUser = new User { Auth0Id = "auth0|new", Email = "new@example.com", Name = "New User" };

        // Act
        var result = await _usersController.PostUser(newUser);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var user = Assert.IsType<User>(createdResult.Value);
        _testOutputHelper.WriteLine(user.Name);
        Assert.Equal(newUser.Auth0Id, user.Auth0Id);
    }
    
    
    // DELETING
    
    [Fact]
    public async Task DeleteUser_Returns_NoContent_When_User_Deleted_Successfully()
    {
        // Arrange
        var userId = new Random().Next(1, 1000); 
        var existingUser = new User
        {
            Id = userId,
            Auth0Id = "auth0|1",
            Email = "user1@example.com",
            Name = "User 1"
        };
        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();

        // Act
        var result = await _usersController.DeleteUser(userId);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);

        var deletedUser = await _context.Users.FindAsync(userId);
        Assert.Null(deletedUser);
    }

    [Fact]
    public async Task DeleteUser_Returns_NotFound_When_User_Does_Not_Exist()
    {
        // Arrange
        var userId = 999;  // Assuming this ID does not exist in the DB.

        // Act
        var result = await _usersController.DeleteUser(userId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }
    
    // CREATING
    [Fact]
    public async Task PostUser_Returns_CreatedAtAction_When_User_Created_Successfully()
    {
        // Arrange
        var newUser = new User
        {
            Auth0Id = "auth0|newuser",
            Email = "newuser@example.com",
            Name = "New User"
        };

        // Act
        var result = await _usersController.PostUser(newUser);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var user = Assert.IsType<User>(createdResult.Value);
        Assert.Equal(newUser.Auth0Id, user.Auth0Id);
        Assert.Equal(newUser.Email, user.Email);
    }

    [Fact]
    public async Task PostUser_Returns_Conflict_When_User_Already_Exists()
    {
        // Arrange
        var existingUser = new User
        {
            Auth0Id = "auth0|existinguser",
            Email = "existing@example.com",
            Name = "Existing User"
        };
        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();

        var newUser = new User
        {
            Auth0Id = "auth0|existinguser",  // Same Auth0Id
            Email = "new@example.com",
            Name = "New User"
        };

        // Act
        var result = await _usersController.PostUser(newUser);

        // Assert
        var conflictResult = Assert.IsType<ConflictResult>(result.Result);
        Assert.Equal(409, conflictResult.StatusCode);
    }


    
    
}