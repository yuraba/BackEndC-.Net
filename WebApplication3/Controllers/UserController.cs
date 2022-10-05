using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Entities;
using WebApplication3.Model;

namespace WebApplication3.Controllers;
[Route("/api/[controller]")]
public class UserController: Controller
{
   
    private readonly Context _context;
    public UserController(Context context)
    {
        _context = context;
    }
    [HttpGet]
    public IEnumerable<User> Get() => _context.Users;
    private int NextUserId => _context.Users.Local.Count == 0 ? 1 : _context.Users.Max(i => i.Id) + 1;

    [HttpGet("NextUserId")]
    public int GetNextUserId()
    {
        return NextUserId;
    }
    [HttpGet("{Id}")]
    [Authorize]
    public IActionResult Get(int Id)
    {
        var user = _context.Users.SingleOrDefault(p => p.Id == Id);
        if (user==null)
        {
            return NotFound();
        }
            
        return Ok(user);
    }
    [HttpPost]
    public async Task<ActionResult<List<User>>> AddUser(User user)
    {
        // user.Id = NextUserId;
        _context.Users.Add(user);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        // return Ok();
    }
    [HttpPut]
    public async Task<ActionResult<List<User>>> UpdateUser(User request)
    {
        var user = _context.Users.SingleOrDefault(p => p.Id == request.Id);
        if (user==null)
        {
            return NotFound();
        }

        user.Username = request.Username;
        user.Email = request.Email;
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        // return Ok();
    }
    
    [HttpDelete("{Id}")]
    public async Task<ActionResult<List<User>>> Delete(int Id)
    {
        var user = _context.Users.SingleOrDefault(p => p.Id == Id);
        if (user==null)
        {
            return NotFound();
        }
        _context.Users.Remove(user);
        _context.SaveChanges();
        return Ok(user);
    }
    
}