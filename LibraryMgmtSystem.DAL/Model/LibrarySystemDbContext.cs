using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMgmtSystem.DAL.Model
{
    /// <summary>DB Context class.</summary>
    public class LibrarySystemDbContext : DbContext
    {
        public LibrarySystemDbContext(DbContextOptions<LibrarySystemDbContext> context) : base(context)
        {

        }

        public DbSet<BookCategories> BookCategories { get; set; }
        public DbSet<Shelves> Shelves { get; set; }
        public DbSet<Books> Books { get; set; }

        /// <summary>setting up constraints.</summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<BookCategories>(entity =>
            {

                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Shelves>(entity =>
            {

                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.HasIndex(e => e.BookCategoryId);
                entity.HasOne(d => d.BookCategory)
          .WithMany(p => p.Shelves)
          .HasForeignKey(d => d.BookCategoryId)
          .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.HasIndex(e => e.ShelfId);
                entity.HasOne(d => d.Shelf)
          .WithMany(p => p.Books)
          .HasForeignKey(d => d.ShelfId)
          .OnDelete(DeleteBehavior.Cascade);
            });


     
        }

    }
}
