using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace WebApplication3.Model;

public class Article
{
    public int Id { get; set; }
    
    public string? title { get; set; } = "";

    public string? body { get; set; } = "";
    
    public string? IsApproved { get; set; } = "";
    
    public string? CreatedBy { get; set; } = "";
    public string? image { get; set; }
    
    

   
    public ICollection<User>? Users { get; set; }
    
    public ICollection<Like>? Likes { get; set; }
    
    public ICollection<Coment>? Comments { get; set; }
}