using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.CompilerServices;
using WebApplication3.Entities;
using WebApplication3.Model;
using WebApplication3.Registration.DTO;



namespace WebApplication3.Registration.Controller;

[Route("/auth/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly Context _context;
    
    private readonly IConfiguration _configuration;
    // private readonly IUserService _userService;


    public AuthController(IConfiguration configuration,Context context)
    {
        _configuration = configuration;
        _context = context;
        // _userService = userService;
    }
    

    // [HttpGet, Authorize]
    // public ActionResult<string> GetMe()
    // {
    //     var userName = _userService.GetMyName();
    //     return Ok(userName);
    // }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        
        var customer = new User();
        var ExistUser = _context.Users.SingleOrDefault(p => p.Username == request.Username);
        if (ExistUser != null)
        {
            return BadRequest("User is Exist");
        }

        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        
        customer.Username = request.Username;
        customer.PasswordHash = passwordHash;
        customer.PasswordSalt = passwordSalt;
        customer.Role = request.Role;
            _context.Users.Add(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim( "username",user.Username),
            new Claim("role", user.Role),
            new Claim("user_id", $"{user.Id}")
        };
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        
        return jwt;
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto request)
    {
        var customer = _context.Users.SingleOrDefault(p => p.Username == request.Username);
        if (customer == null)
        {
            return BadRequest("User not found");
        }

        if (!VerifyPasswordHash(request.Password, customer.PasswordHash, customer.PasswordSalt))
        {
            return BadRequest("Wrong password");
        }
        string token = CreateToken(customer);
        
        var refreshToken = GenerateRefreshToken();
        SetRefreshToken(refreshToken, request);
        _context.SaveChanges();
        return Ok(token);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<string>> RefreshToken(UserDto request)
    {
        var customer = _context.Users.SingleOrDefault(p => p.Username == request.Username);

        var refreshToken = Request.Cookies["refreshToken"];
        if (!customer.RefreshToken.Equals(refreshToken))
        {
            return Unauthorized("Invalid Refresh token");
        }
        else if (customer.TokenExpires < DateTime.Now)
        {
            return Unauthorized("Token expired");
        }

        string token = CreateToken(customer);
        var newRefreshToken = GenerateRefreshToken();
        SetRefreshToken(newRefreshToken,request );

        return Ok(token);
    }
    
    
    private RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now
        };
        return refreshToken;
    }

    private void SetRefreshToken(RefreshToken newRefreshToken,UserDto request)
    {
        var customer = _context.Users.SingleOrDefault(p => p.Username == request.Username);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires
        };
        Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        customer.RefreshToken = newRefreshToken.Token;
        customer.TokenCreated = newRefreshToken.Created;
        customer.TokenExpires = newRefreshToken.Expires;
    }
    
    

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password,  byte[] passwordHash,  byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}