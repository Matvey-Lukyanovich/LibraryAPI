using LibraryAPI.Application.Interfaces;
using MediatR;
using LibraryAPI.Application.Interfaces;
using System.IO;

public class BorrowRequest
{
    public DateTime ReturnBy { get; set; }
}


public class BorrowBookCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public DateTime ReturnBy { get; set; }
}

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Unit>
{
    private readonly IBookRepository _bookRepository;

    public BorrowBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Unit> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);
        if (book == null || book.IsBorrowed)
        {
            throw new InvalidOperationException("Book is either not found or already borrowed.");
        }

        book.BorrowedAt = DateTime.UtcNow;
        book.ReturnBy = request.ReturnBy;
        book.IsBorrowed = true;

        await _bookRepository.UpdateAsync(book);
        return Unit.Value;
    }
}