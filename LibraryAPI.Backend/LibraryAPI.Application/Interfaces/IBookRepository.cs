using LibraryAPI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAPI.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);             // Получение книги по ID
        Task<List<Book>> GetAllAsync();              // Получение всех книг
        Task AddAsync(Book book);                    // Добавление книги
        Task<Book> GetByIsbnAsync(string isbn);
        Task UpdateAsync(Book book);                 // Обновление книги
        Task DeleteAsync(int id);                    // Удаление книги по ID
    }
}
