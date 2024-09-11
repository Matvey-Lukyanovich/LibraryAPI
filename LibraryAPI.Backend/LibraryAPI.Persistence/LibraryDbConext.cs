using Microsoft.EntityFrameworkCore;
using LibraryAPI.Domain;
using LibraryAPI.Application.Interfaces;
using LibraryAPI.Persistence.EntityTypeConfiguration;

namespace LibraryAPI.Persistence
{
    public class LibraryDbContext: DbContext, ILibraryDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; } 

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new LibraryConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
