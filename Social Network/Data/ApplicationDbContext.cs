using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Social_Network.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Post> Posts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()");

            builder.Entity<Comment>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getdate()");

            builder.Entity<Like>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getdate()");

            builder.Entity<Post>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getdate()");

            builder.Entity<Image>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getdate()");

            builder.Entity<User>()
            .Property(b => b.ProfileUrl)
            .HasDefaultValueSql("/storage/default/default_profile_img.png");
            

            //

            builder.Entity<User>()
               .HasMany(b => b.Posts)
               .WithOne(t => t.Author)
               .HasForeignKey(j => j.Author.Id);

            builder.Entity<User>()
              .HasMany(b => b.Likes)
              .WithOne(t => t.Author)
              .HasForeignKey(j => j.Author.Id);

            builder.Entity<User>()
              .HasMany(b => b.Comments)
              .WithOne(t => t.Author)
              .HasForeignKey(j => j.Author.Id);

            //

            builder.Entity<User>()
                .HasMany(f => f.Followers)
                .WithOne(u => u)



        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
