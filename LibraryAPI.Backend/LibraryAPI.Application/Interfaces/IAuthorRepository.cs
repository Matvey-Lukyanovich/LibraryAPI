// IAuthorRepository.cs
using LibraryAPI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAPI.Application.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(int id);
    }
}
