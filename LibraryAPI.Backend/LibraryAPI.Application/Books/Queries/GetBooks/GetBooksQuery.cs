using MediatR;
using LibraryAPI.Domain;
using LibraryAPI.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryAPI.Application.Interfaces;

public class GetBooksQuery : IRequest<List<BookDto>>
{
}

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync();

        var bookDtoList = books.Select(book => new BookDto
        {
            Id = book.Id,
            ISBN = book.ISBN,
            Title = book.Title,
            Genre = book.Genre,
            Description = book.Description,
            AuthorId = book.AuthorId,
            IsBorrowed = book.IsBorrowed
        }).ToList();

        return bookDtoList; // Возвращаем список DTO
    }

}
