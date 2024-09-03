using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        try
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .ToListAsync();

            var bookDtos = books.Select(b => new
            {
                b.Id,
                b.ISBN,
                b.Title,
                b.Genre,
                b.Description,
                b.AuthorId,
                AuthorName = $"{b.Author.FirstName} {b.Author.LastName}"
            }).ToList();

            return Ok(bookDtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpGet("isbn/{isbn}")]
    public async Task<ActionResult<Book>> GetBookByIsbn(string isbn)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.ISBN == isbn);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        if (book == null)
        {
            return BadRequest("Book object is null.");
        }

        if (book.AuthorId <= 0)
        {
            return BadRequest("Invalid AuthorId.");
        }

        // Убедитесь, что Author не передается и не валидируется при добавлении
        book.Author = null;

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest("Mismatched Book ID.");
        }

        if (!_context.Books.Any(b => b.Id == id))
        {
            return NotFound("Book not found.");
        }

        // Убедитесь, что Author не передается при обновлении
        book.Author = null;

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(id))
            {
                return NotFound("Concurrency conflict: Book does not exist.");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}/borrow")]
    public async Task<IActionResult> BorrowBook(int id, BorrowRequest request)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null || book.IsBorrowed)
        {
            return BadRequest("Book is either not found or already borrowed.");
        }

        book.BorrowedAt = DateTime.UtcNow;
        book.ReturnBy = request.ReturnBy;
        book.IsBorrowed = true;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null || !book.IsBorrowed)
        {
            return BadRequest("Book is either not found or not borrowed.");
        }

        book.BorrowedAt = null;
        book.ReturnBy = null;
        book.IsBorrowed = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("upload-image/{id}")]
    public async Task<IActionResult> UploadImage(int id, IFormFile imageFile)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        if (imageFile == null || imageFile.Length == 0)
        {
            return BadRequest("No image file uploaded.");
        }

        var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(imagesPath))
        {
            Directory.CreateDirectory(imagesPath);
        }

        var filePath = Path.Combine(imagesPath, imageFile.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        book.ImageUrl = $"/images/{imageFile.FileName}";
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }

    public class BorrowRequest
    {
        public DateTime ReturnBy { get; set; }
    }
}
