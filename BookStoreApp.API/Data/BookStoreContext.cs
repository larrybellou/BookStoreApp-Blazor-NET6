using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStoreApp.API.Data
{
    public partial class BookStoreContext : IdentityDbContext<ApiUser>
    {
        public BookStoreContext()
        {
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Firstname).HasMaxLength(50);

                entity.Property(e => e.Lastname).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Isbn, "UniqueISBN")
                    .IsUnique();

                entity.Property(e => e.Image).HasMaxLength(150);

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Summary).HasMaxLength(150);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Books_Authors");
            });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "ff350eec-aeb0-4d9b-a063-cc4c9d4e23f0"
                },
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = "0e0ba207-38d3-44de-ae69-712cc644d60b"
                }
                );

            var hasher = new PasswordHasher<ApiUser>();

            modelBuilder.Entity<ApiUser>().HasData(
                new ApiUser
                {
                    FirstName = "Larry",
                    LastName = "Bellou",
                    Email = "here@there.com",
                    UserName = "here@there.com",
                    NormalizedUserName = "HERE@THERE.COM",
                    Id = "7f2d133d-097b-4307-bff4-e09c21a4f552",
                    PasswordHash = hasher.HashPassword(null, "Tester#1")
                },
                new ApiUser
                {
                    FirstName = "Viviann",
                    LastName = "Colvert",
                    Email = "Colvert@there.com",
                    UserName = "Colvert@there.com",
                    NormalizedUserName = "COLVERT@THERE.COM",
                    Id = "895dc575-444f-4827-884e-d6c28b77a46d",
                    PasswordHash = hasher.HashPassword(null, "Tester#1")
                }
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "ff350eec-aeb0-4d9b-a063-cc4c9d4e23f0",
                    UserId = "895dc575-444f-4827-884e-d6c28b77a46d"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "0e0ba207-38d3-44de-ae69-712cc644d60b",
                    UserId = "7f2d133d-097b-4307-bff4-e09c21a4f552"
                }
                );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
