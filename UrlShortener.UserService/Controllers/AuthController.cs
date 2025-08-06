using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Shared.Models.DTOs;
using UrlShortener.Shared.Models.Entities;
using UrlShortener.UserService.Data;
using UrlShortener.UserService.Services;

namespace UrlShortener.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserDbContext _db;
        private readonly JwtService _jwt;

        public AuthController(UserDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _db.Users.AnyAsync(u => u.Username == request.Username))
                return BadRequest("Username already exists");

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = request.Username,
                Email = request.Email,
                PasswordHash = PasswordHasher.Hash(request.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = "User registered successfully",
                user = new { user.Id, user.Username, user.Email }
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u =>
                u.Username == request.Username && u.IsActive);

            if (user == null || !PasswordHasher.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user);

            return Ok(new
            {
                message = "Login successful",
                token = token,
                user = new { user.Id, user.Username, user.Email }
            });
        }
    }
}