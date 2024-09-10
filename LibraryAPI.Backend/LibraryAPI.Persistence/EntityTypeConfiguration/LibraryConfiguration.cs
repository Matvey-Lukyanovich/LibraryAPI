using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryAPI.Domain;

namespace LibraryAPI.Persistence.EntityTypeConfiguration
{
    public class LibraryConfiguration : IEntityTypeConfiguration<Book>
    {         
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade); 

        }


    }
}
