using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Model;

namespace WebApplication3.Entities;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    

    public void Configure(EntityTypeBuilder<Like> builder)
    {
    }

    public void InitializeData(EntityTypeBuilder<Like> builder)
    {
     

        builder.HasData(
            new Like
            {
                LikeStatus = true
            },
            new Like
            {
                LikeStatus = false
            },
            new Like
            {
                
            });
    }

    
}