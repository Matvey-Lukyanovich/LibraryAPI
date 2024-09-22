using MediatR;
using LibraryAPI.Domain;
using LibraryAPI.Application.Interfaces;

namespace LibraryAPI.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public BookDto Book { get; set; }

        public UpdateBookCommand(int id, BookDto book)
        {
            Id = id;
            Book = book;
        }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            book.ISBN = request.Book.ISBN;
            book.Title = request.Book.Title;
            book.Genre = request.Book.Genre;
            book.Description = request.Book.Description;
            book.AuthorId = request.Book.AuthorId;

            await _bookRepository.UpdateAsync(book);

            return Unit.Value; // Возвращаем Unit для обозначения завершения без результата
        }
    }



}
