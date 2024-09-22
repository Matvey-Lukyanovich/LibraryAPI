// AuthorsController.cs
using LibraryAPI.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        var authors = await _mediator.Send(new GetAuthorsQuery());
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthor(int id)
    {
        var author = await _mediator.Send(new GetAuthorByIdQuery { Id = id });
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateAuthor(CreateAuthorCommand command)
    {
        var authorId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAuthor), new { id = authorId }, authorId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Author ID mismatch.");
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _mediator.Send(new DeleteAuthorCommand { Id = id });
        return NoContent();
    }
}
