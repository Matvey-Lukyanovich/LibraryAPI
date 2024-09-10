using LibraryAPI.Domain;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAPI.Application.Interfaces
{
    public interface ILibraryDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
