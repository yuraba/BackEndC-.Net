using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Model;

namespace WebApplication3.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
    }
    public void InitializeData(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Username = "Yura01",
                Score = 4
            },
            new User
            {
                Username = "Yura02",
                Score = 3
            },
            new User
            {
                Username = "Yura03",
                Score = 5
            });
        
    }
}