using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHAKA.RestApi.EF.Example.API.Data;
using SHAKA.RestApi.EF.Example.API.DTOs;
using SHAKA.RestApi.EF.Example.API.Models;

namespace SHAKA.RestApi.EF.Example.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MyUserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<MyUserController> _logger;

    public MyUserController(ApplicationDbContext context, ILogger<MyUserController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/MyUser
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MyUserDto>>> GetMyUsers()
    {
        _logger.LogInformation("Getting all users");
        var users = await _context.MyUsers.ToListAsync();
        
        return Ok(users.Select(user => new MyUserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedDate = user.CreatedDate,
            IsActive = user.IsActive
        }));
    }

    // GET: api/MyUser/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MyUserDto>> GetMyUser(int id)
    {
        _logger.LogInformation("Getting user with id: {Id}", id);
        var user = await _context.MyUsers.FindAsync(id);

        if (user == null)
        {
            _logger.LogWarning("User with id: {Id} not found", id);
            return NotFound();
        }

        return new MyUserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedDate = user.CreatedDate,
            IsActive = user.IsActive
        };
    }

    // POST: api/MyUser
    [HttpPost]
    public async Task<ActionResult<MyUserDto>> CreateMyUser(CreateMyUserDto createUserDto)
    {
        _logger.LogInformation("Creating new user with username: {Username}", createUserDto.Username);
        
        // Check if username already exists
        if (await _context.MyUsers.AnyAsync(u => u.Username == createUserDto.Username))
        {
            _logger.LogWarning("Username {Username} already exists", createUserDto.Username);
            return BadRequest("Username already exists");
        }
        
        // Check if email already exists
        if (await _context.MyUsers.AnyAsync(u => u.Email == createUserDto.Email))
        {
            _logger.LogWarning("Email {Email} already exists", createUserDto.Email);
            return BadRequest("Email already exists");
        }

        var user = new MyUser
        {
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            CreatedDate = DateTime.UtcNow,
            IsActive = true
        };

        _context.MyUsers.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMyUser), new { id = user.Id }, new MyUserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedDate = user.CreatedDate,
            IsActive = user.IsActive
        });
    }

    // PUT: api/MyUser/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMyUser(int id, UpdateMyUserDto updateUserDto)
    {
        _logger.LogInformation("Updating user with id: {Id}", id);
        var user = await _context.MyUsers.FindAsync(id);
        
        if (user == null)
        {
            _logger.LogWarning("User with id: {Id} not found", id);
            return NotFound();
        }
        
        // Check username uniqueness if it's being updated
        if (updateUserDto.Username != null && updateUserDto.Username != user.Username)
        {
            if (await _context.MyUsers.AnyAsync(u => u.Username == updateUserDto.Username))
            {
                _logger.LogWarning("Username {Username} already exists", updateUserDto.Username);
                return BadRequest("Username already exists");
            }
            user.Username = updateUserDto.Username;
        }
        
        // Check email uniqueness if it's being updated
        if (updateUserDto.Email != null && updateUserDto.Email != user.Email)
        {
            if (await _context.MyUsers.AnyAsync(u => u.Email == updateUserDto.Email))
            {
                _logger.LogWarning("Email {Email} already exists", updateUserDto.Email);
                return BadRequest("Email already exists");
            }
            user.Email = updateUserDto.Email;
        }
        
        // Update other properties
        if (updateUserDto.FirstName != null)
            user.FirstName = updateUserDto.FirstName;
            
        if (updateUserDto.LastName != null)
            user.LastName = updateUserDto.LastName;
            
        if (updateUserDto.IsActive.HasValue)
            user.IsActive = updateUserDto.IsActive.Value;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                _logger.LogWarning("User with id: {Id} not found during update", id);
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(new MyUserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedDate = user.CreatedDate,
            IsActive = user.IsActive
        });
    }

    // Helper method to check if user exists
    private bool UserExists(int id)
    {
        return _context.MyUsers.Any(e => e.Id == id);
    }
}
