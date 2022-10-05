using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? Username { get; set; } = string.Empty;
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? VerificationToken { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }

    [Range(1, 5)] public int? Score { get; set; }
    public ICollection<Coment>? Comments { get; set; }
    public ICollection<Article>? UsersArticles { get; set; } 
}