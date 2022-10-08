using Microsoft.EntityFrameworkCore;
using WebApplication3.Model;

namespace WebApplication3.Entities;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        
        // Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=blog;User Id=sa;Password=@978w0rD;Encrypt=false;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        modelBuilder.ApplyConfiguration(new ComentConfiguration());
        modelBuilder.ApplyConfiguration(new LikeConfiguration());
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Coment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
}