using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace WebApplication3.Model;

public class Article
{
    public int Id { get; set; }
    
    // public DateTime Created { get; set; } = DateTime.Now;
    
    
    [StringLength(50, MinimumLength = 3)]
    public string? Title { get; set; } = "";

    public string? Body { get; set; } = "";
    
    public string? Image { get; set; }
    
   
    public ICollection<User>? Users { get; set; }
    
    public ICollection<Like>? Likes { get; set; }
    
    public ICollection<Coment>? Comments { get; set; }
}