using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy.Server.Api.Database;
using Udemy.Server.Api.Dtos;
using Udemy.Server.Api.Entities;

namespace Udemy.Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users.Select(u => new UserDto(u.UserId, u.UserName)).ToList();
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return new UserDto(user.UserId, user.UserName);
    }

    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
    {
        var user = new User
        {
            UserName = userDto.UserName
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, new UserDto(user.UserId, user.UserName));
    }
}