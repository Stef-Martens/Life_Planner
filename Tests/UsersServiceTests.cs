namespace Tests;
using LifePlanner.Server;
using LifePlanner.Server.Controllers;
using LifePlanner.Server.Models;
using LifePlanner.Server.Services;
using LifePlanner.Server.Repositories;
using LifePlanner.Server.Repositories.Interfaces;
using Moq;

public class UsersServiceTests
{
    private readonly UsersService _usersService;
    private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
    
    public UsersServiceTests()
    {
        _usersService = new UsersService(_userRepositoryMock.Object);
    }
    
    
    [Fact]
    public async Task GetById_ShouldReturnUser_WhenCustomerExists()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "Test User",
            Email = "t.t@t.be"
        };
        
        _userRepositoryMock.Setup(x => x.GetById(user.Id)).ReturnsAsync(user);

        // Act 
        var result = await _usersService.GetById(1);

        // Assert
        Assert.Equal(user, result);
    }
    
    [Fact]
    public async Task GetById_ShouldReturnNothing_WhenCustomerDoesntExists()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(()=>null);
        
        // Act 
        var user = await _usersService.GetById(1);
        
        // Assert
        Assert.Null(user);
    }
}