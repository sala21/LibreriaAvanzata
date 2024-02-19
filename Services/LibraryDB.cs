using Microsoft.EntityFrameworkCore;
using WebAppLibreria.Models;

namespace WebAppLibreria.Services
{
    public class LibraryDB : DbContext
    {
        public LibraryDB(DbContextOptions<LibraryDB> options) : base(options)
        {
        }

        public DbSet<BookItem> Books { get; set; }
        public DbSet<BookshelfItem> Bookshelves { get; set; }
        public DbSet<GenreItem> Genres { get; set; }

    }
}