using LibraryAPI.Application.Books.Commands.DeleteBook;
using LibraryAPI.Application.Books.Commands.UpdateBook;
using LibraryAPI.Application.Books.Queries.GetBooks;
using LibraryAPI.Domain;
using LibraryAPI.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
[ApiController]
[Route("api/[controller]")]
//[Authorize(Policy = "AdminOnly")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var result = await _mediator.Send(new GetBooksQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var result = await _mediator.Send(new GetBookByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("isbn/{isbn}")]
    public async Task<IActionResult> GetBookByIsbn(string isbn)
    {
        var result = await _mediator.Send(new GetBookByIsbnQuery(isbn));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostBook([FromBody] BookDto bookDto)
    {
        var id = await _mediator.Send(new CreateBookCommand { Book = bookDto });
        return CreatedAtAction(nameof(GetBook), new { id }, bookDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, [FromBody] BookDto bookDto)
    {
        await _mediator.Send(new UpdateBookCommand(id, bookDto));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _mediator.Send(new DeleteBookCommand(id));
        return NoContent();
    }

    [HttpPatch("{id}/borrow")]
    public async Task<IActionResult> BorrowBook(int id, [FromBody] BorrowRequest request)
    {
        var command = new BorrowBookCommand { Id = id, ReturnBy = request.ReturnBy };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var command = new ReturnBookCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("upload-image/{id}")]
    public async Task<IActionResult> UploadImage(int id, IFormFile imageFile)
    {
        var command = new UploadImageCommand { Id = id, ImageFile = imageFile };
        await _mediator.Send(command);
        return NoContent();
    }

}



