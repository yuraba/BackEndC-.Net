using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Model;
using System;

namespace WebApplication3.Entities;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
    }
    public void InitializeData(EntityTypeBuilder<Article> builder)
    {


        builder.HasData(
            new Article
            {
                
                Title = "My first article :)",
                Body = "I want to tell you about ...",
            },
            new Article
            {
                
                Title = "Relationship important or no",
                Body = "I want to tell you about ...",
                
              
            },
            new Article
            {
                
                Title = "My experience in IT",
                Body = "I want to tell you about ...",
               
           
            });
        
    }

    
}