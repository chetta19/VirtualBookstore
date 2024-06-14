using Books.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Books.Data
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = "1",
                    ISBN = "978-0-306-40615-7",
                    Title = "The C Programming Language",
                    Author = "Brian W. Kernighan, Dennis M. Ritchie",
                    Genre = "Programming",
                    Price = 9.99m,
                    CoverImage = "https://images-na.ssl-images-amazon.com/images/I/41KmXv2v6LL._SX258_BO1,204,203,200_.jpg",
                    Description = "The authors present the complete guide to ANSI standard C language programming. Written by the developers of C, this new version helps readers keep up with the finalized ANSI standard for C while showing how to take advantage of C's rich set of operators, economy of expression, improved control flow, and data structures."
                },
                new Book
                {
                    Id = "2",
                    ISBN = "978-0-13-110362-7",
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    Genre = "Programming",
                    Price = 29.99m,
                    CoverImage = "https://images-na.ssl-images-amazon.com/images/I/41jEbK-jG+L._SX258_BO1,204,203,200_.jpg",
                    Description = "Even bad code can function. But if code isn't clean, it can bring a development organization to its knees. Every year, countless hours and significant resources are lost because of poorly written code. But it doesn't have to be that way."
                },
                new Book
                {
                    Id = "3",
                    ISBN = "978-0-13-235088-4",
                    Title = "Clean Architecture",
                    Author = "Robert C. Martin",
                    Genre = "Programming",
                    Price = 37.99m,
                    CoverImage = "https://images-na.ssl-images-amazon.com/images/I/41ZQZ8a6g8L._SX258_BO1,204,203,200_.jpg",
                    Description = "By applying universal rules of software architecture, you can dramatically improve developer productivity throughout the life of any software system. Now, building upon the success of his best-selling books Clean Code and The Clean Coder, legendary software craftsman Robert C. Martin turns his attention to what makes a great software architect."
                });
        }
    }
}