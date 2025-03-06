using Microsoft.EntityFrameworkCore;
using WebServiceProject.Models;

namespace WebServiceProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Book - Review(One - to - Many)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for reviews when a Book is deleted

            // User - Review (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.Reviewer)
                .HasForeignKey(r => r.ReviewerId);

            // Configure one-to-many relationship between User and Book
            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Borrowed_Books)
            //    .WithOne(b => b.Borrower)
            //    .HasForeignKey(b => b.BorrowerId)
            //    .OnDelete(DeleteBehavior.Restrict);


            // Book - Genre (Many-to-Many)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity(j => j.ToTable("BookGenres"));  // Customize the join table name

            // Index on Book title
            modelBuilder.Entity<Book>()
                .HasIndex(m => m.Title)
                .IsUnique();  // Enforce unique titles for Books

            base.OnModelCreating(modelBuilder);
        }
    }
}
