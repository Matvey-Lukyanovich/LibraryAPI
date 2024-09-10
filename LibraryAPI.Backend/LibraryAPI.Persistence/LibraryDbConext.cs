using Microsoft.EntityFrameworkCore;
using LibraryAPI.Domain;
using LibraryAPI.Application.Interfaces;
using LibraryAPI.Persistence.EntityTypeConfiguration;

namespace LibraryAPI.Persistence
{
    public class LibraryDbConext: DbContext, ILibraryDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public LibraryDbConext(DbContextOptions<LibraryDbConext> options)
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
