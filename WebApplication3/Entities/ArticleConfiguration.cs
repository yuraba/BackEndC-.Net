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
                
                title = "My first article :)",
                body = "I want to tell you about ...",
            },
            new Article
            {
                
                title = "Relationship important or no",
                body = "I want to tell you about ...",
                
              
            },
            new Article
            {
                
                title = "My experience in IT",
                body = "I want to tell you about ...",
               
           
            });
        
    }

    
}