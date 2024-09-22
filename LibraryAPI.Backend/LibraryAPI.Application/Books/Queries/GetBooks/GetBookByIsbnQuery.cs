using LibraryAPI.Application.Interfaces;
using LibraryAPI.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAPI.Application.Interfaces;


namespace LibraryAPI.Application.Books.Queries.GetBooks
{
    public class GetBookByIsbnQuery : IRequest<BookDto>
    {
        public string Isbn { get; set; }

        public GetBookByIsbnQuery(string isbn)
        {
            Isbn = isbn;
        }
    }

    public class GetBookByIsbnQueryHandler : IRequestHandler<GetBookByIsbnQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIsbnQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookDto> Handle(GetBookByIsbnQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIsbnAsync(request.Isbn);
          
            if (book == null)
            {
                return null; // Или выбросьте исключение, если это допустимо
            }

            return new BookDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Genre = book.Genre,
                Description = book.Description,
                AuthorId = book.AuthorId,
                IsBorrowed = book.IsBorrowed
            };
        }
    }

}
