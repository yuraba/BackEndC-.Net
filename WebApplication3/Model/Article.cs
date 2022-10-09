using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace WebApplication3.Model;

public class Article
{
    public int Id { get; set; }
    
    // public DateTime Created { get; set; } = DateTime.Now;
    
    
  
    public string? title { get; set; } = "";

    public string? body { get; set; } = "";
    
    public string? image { get; set; }
    
   
    public ICollection<User>? Users { get; set; }
    
    public ICollection<Like>? Likes { get; set; }
    
    public ICollection<Coment>? Comments { get; set; }
}