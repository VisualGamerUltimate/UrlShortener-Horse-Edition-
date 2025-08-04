using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Shared.Models.Entities;
using UrlShortener.UserService.Controllers;
using UrlShortener.UserService.Data;
using UrlShortener.Shared.Models.DTOs;
using Xunit;

public class AuthControllerTests
{
    private readonly AuthController _controller;
public AuthControllerTests()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase("AuthTestDb")
            .Options;

        var context = new UserDbContext(options);
        _controller = new AuthController(context, null);
    }

    [Fact]
    public async Task Register_Returns_Ok_For_NewUser()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Username = "testuser",
            Email = "testuser@example.com",
        };

        // Act
        var result = await _controller.Register(request);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}