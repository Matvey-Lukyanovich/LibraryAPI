using MediatR;
using LibraryAPI.Application.Interfaces;
using System.IO;

public class ReturnBookCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Unit>
{
    private readonly IBookRepository _bookRepository;

    public ReturnBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Unit> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);
        if (book == null || !book.IsBorrowed)
        {
            throw new InvalidOperationException("Book is either not found or not borrowed.");
        }

        book.BorrowedAt = null;
        book.ReturnBy = null;
        book.IsBorrowed = false;

        await _bookRepository.UpdateAsync(book);
        return Unit.Value;
    }
}