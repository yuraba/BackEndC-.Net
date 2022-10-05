namespace WebApplication3.Model;

public class Coment
{
    // one user may have many comments
    // one to many
    
    public int Id { get; set; }
    public string? Comment { get; set; }
    public User? User { get; set; }
    
    public Article? Article { get; set; }
}