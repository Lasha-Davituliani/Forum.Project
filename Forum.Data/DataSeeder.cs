using Forum.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data
{
    public static class DataSeeder
    {
       
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "33B7ED72-9434-434A-82D4-3018B018CB87", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "9C07F9F6-D3B0-458A-AB7F-218AA622FA5B", Name = "Customer", NormalizedName = "CUSTOMER" }
            );
        }
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            PasswordHasher<ApplicationUser> hasher = new();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser()
                {
                    Id = "8716071C-1D9B-48FD-B3D0-F059C4FB8031",
                    UserName = "admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = hasher.HashPassword(null, "Admin123!"),
                    PhoneNumber = "555337681",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new ApplicationUser()
                {
                    Id = "87746F88-DC38-4756-924A-B95CFF3A1D8A",
                    UserName = "gio@gmail.com",
                    NormalizedUserName = "GIO@GMAIL.COM",
                    Email = "gio@gmail.com",
                    NormalizedEmail = "GIO@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = hasher.HashPassword(null, "Gio123!"),
                    PhoneNumber = "551442269",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                 new ApplicationUser()
                 {
                     Id = "D514EDC9-94BB-416F-AF9D-7C13669689C9",
                     UserName = "nika@gmail.com",
                     NormalizedUserName = "NIKA@GMAIL.COM",
                     Email = "nika@gmail.com",
                     NormalizedEmail = "NIKA@GMAIL.COM",
                     EmailConfirmed = false,
                     PasswordHash = hasher.HashPassword(null, "Nika123!"),
                     PhoneNumber = "558490645",
                     PhoneNumberConfirmed = false,
                     TwoFactorEnabled = false,
                     LockoutEnd = null,
                     LockoutEnabled = true,
                     AccessFailedCount = 0
                 });
        }
        public static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                    new IdentityUserRole<string> { RoleId = "33B7ED72-9434-434A-82D4-3018B018CB87", UserId = "8716071C-1D9B-48FD-B3D0-F059C4FB8031" },
                    new IdentityUserRole<string> { RoleId = "9C07F9F6-D3B0-458A-AB7F-218AA622FA5B", UserId = "D514EDC9-94BB-416F-AF9D-7C13669689C9" },
                    new IdentityUserRole<string> { RoleId = "9C07F9F6-D3B0-458A-AB7F-218AA622FA5B", UserId = "87746F88-DC38-4756-924A-B95CFF3A1D8A" }
                );
        }

        public static void SeedTopics(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopicEntity>().HasData(
                new TopicEntity()
                {
                    Id = 1,
                    Title = "Story",
                    Description = "Story Description...",
                    CreationDate = DateTime.Now,
                    AuthorId = "8716071C-1D9B-48FD-B3D0-F059C4FB8031",
                    State = State.Pending,
                    Status = Status.Active
                },
                new TopicEntity()
                {
                    Id = 2,
                    Title = "Story2",
                    Description = "Story2 Description...",
                    CreationDate = DateTime.Now,
                    AuthorId = "87746F88-DC38-4756-924A-B95CFF3A1D8A",
                    State = State.Pending,
                    Status = Status.Active
                },
                new TopicEntity()
                {
                    Id = 3,
                    Title = "Story3",
                    Description = "Story3 Description...",
                    CreationDate = DateTime.Now,
                    AuthorId = "D514EDC9-94BB-416F-AF9D-7C13669689C9",
                    State = State.Pending,
                    Status = Status.Active
                }
                );
        }
        public static void SeedComments(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentEntity>().HasData(
                new CommentEntity()
                {
                    Id = 1,
                    Content = "Great Story!",
                    CreationDate = DateTime.Now,
                    TopicId = 1,
                    AuthorId = "87746F88-DC38-4756-924A-B95CFF3A1D8A"
                },
                new CommentEntity()
                {
                    Id = 2,
                    Content = "Great Story2!",
                    CreationDate = DateTime.Now,
                    TopicId = 2,
                    AuthorId = "D514EDC9-94BB-416F-AF9D-7C13669689C9"
                },
                 new CommentEntity()
                 {
                     Id = 3,
                     Content = "Great Story3!",
                     CreationDate = DateTime.Now,
                     TopicId = 3,
                     AuthorId = "8716071C-1D9B-48FD-B3D0-F059C4FB8031"
                 }
                );
            //modelBuilder.Entity<CommentEntity>()
            //            .HasOne(c => c.Topic)
            //            .WithMany(t => t.Comments)
            //            .HasForeignKey(c => c.TopicId)
            //            .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<CommentEntity>()
            //            .HasOne(c => c.Author)
            //            .WithMany(u => u.Comments)
            //            .HasForeignKey(c => c.AuthorId)
            //            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
