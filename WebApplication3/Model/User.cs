using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model;

public class User
{
  
    public int Id { get; set; }
    public string? Role { get; set; } = string.Empty;
    public string? Username { get; set; } = string.Empty;
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
    
    public string? image { get; set; }

    public ICollection<Coment>? Comments { get; set; }
    public ICollection<Article>? UsersArticles { get; set; } 
}