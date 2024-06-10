using Forum.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Forum.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<CommentEntity>()
                        .HasOne(c => c.Topic)
                        .WithMany(t => t.Comments)
                        .HasForeignKey(c => c.TopicId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CommentEntity>()
                        .HasOne(c => c.User)
                        .WithMany(u => u.Comments)
                        .HasForeignKey(c => c.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.SeedTopics();
            modelBuilder.SeedComments();
            modelBuilder.SeedRoles();
            modelBuilder.SeedUsers();
            modelBuilder.SeedUserRoles();
        }

        public DbSet<TopicEntity> Topics { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
    }
}
