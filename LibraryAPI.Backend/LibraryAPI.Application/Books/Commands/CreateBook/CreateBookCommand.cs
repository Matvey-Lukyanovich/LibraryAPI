using MediatR;
using LibraryAPI.Domain;
using LibraryAPI.Application.Interfaces;
public class CreateBookCommand : IRequest<int>
{
    public BookDto Book { get; set; }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            ISBN = request.Book.ISBN,
            Title = request.Book.Title,
            Genre = request.Book.Genre,
            Description = request.Book.Description,
            AuthorId = request.Book.AuthorId,
            IsBorrowed = false,
            ImageUrl = request.Book.ImageUrl // Убедитесь, что это поле не null
        };

        // Логирование для проверки
        if (string.IsNullOrEmpty(book.ImageUrl))
        {
            throw new ArgumentException("ImageUrl не может быть null или пустым.");
        }

        await _bookRepository.AddAsync(book);
        return book.Id;
    }

}

