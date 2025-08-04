using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using UrlShortener.Shared.Models.Entities;
using UrlShortener.UserService.Services;
using Xunit;

public class JwtServiceTests
{
    [Fact]
    public void GenerateToken_Should_Return_ValidToken()
    {
        // Arrange
        var config = new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string>
        {
{ "Jwt:Key", "THIS_IS_A_SAMPLE_SECRET_KEY_32CHARS" },
{ "Jwt:Issuer", "TestIssuer" },
{ "Jwt:Audience", "TestAudience" }
        }).Build();

        var jwtService = new JwtService(config);

        var user = new User
        {
            Id = "test123",
            Username = "john",
            Email = "john@example.com"
        };

        // Act
        var token = jwtService.GenerateToken(user);

        // Assert
        token.Should().NotBeNullOrEmpty();
    }
}