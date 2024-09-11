using LibraryAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminOnly")]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public AuthorsController(LibraryDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/authors - Получение списка всех авторов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            try
            {
                var authors = await _context.Authors.ToListAsync();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                // Логирование ошибки (например, в файл или систему логирования)
                Console.WriteLine($"Exception occurred while fetching authors: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // 2. GET: api/authors/5 - Получение автора по его Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // 3. POST: api/authors - Добавление нового автора
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            if (author == null)
            {
                return BadRequest("Author object is null.");
            }

            if (!ModelState.IsValid)
            {
                // Логирование всех ошибок модели
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Validation failed", errors });
            }

            try
            {
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                // Логирование исключения
                Console.WriteLine($"Exception occurred while adding author: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // 4. PUT: api/authors/5 - Изменение информации о существующем авторе
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest("Author ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // 5. DELETE: api/authors/5 - Удаление автора
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            // Проверка на наличие книг у автора перед удалением
            if (author.Books.Any())
            {
                return BadRequest("Cannot delete author with existing books.");
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 6. GET: api/authors/{id}/books - Получение всех книг по автору
        [HttpGet("{id}/books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(int id)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            var books = author.Books;
            return Ok(books);
        }

        // Проверка существования автора
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
