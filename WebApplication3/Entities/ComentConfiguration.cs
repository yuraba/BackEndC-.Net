using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Model;

namespace WebApplication3.Entities;

public class ComentConfiguration : IEntityTypeConfiguration<Coment>
{
    public void Configure(EntityTypeBuilder<Coment> builder)
    {
    }
    public void InitializeData(EntityTypeBuilder<Coment> builder)
    {
        
        
        builder.HasData(
            new Coment
            {
                Comment = "Briliant"
            },
            new Coment
            {
                Comment = ""
            },
            new Coment
            {
                Comment = ""
            });
        
        
    }
}