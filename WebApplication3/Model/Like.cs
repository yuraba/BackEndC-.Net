namespace WebApplication3.Model;

public class Like
{
    // one user may have many likes
    // one to many
    
    public int Id { get; set; }
    public bool? LikeStatus { get; set; }
    public User? User { get; set; }
    public int? ArticleId { get; set; }
}