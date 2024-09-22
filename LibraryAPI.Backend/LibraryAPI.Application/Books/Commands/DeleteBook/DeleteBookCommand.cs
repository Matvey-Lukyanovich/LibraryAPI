using LibraryAPI.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAPI.Domain;
using LibraryAPI.Application.Interfaces;


namespace LibraryAPI.Application.Books.Commands.DeleteBook
{
    // Команда должна реализовывать IRequest<Unit>
    public class DeleteBookCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteBookCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await _bookRepository.DeleteAsync(request.Id);
            return Unit.Value; // Возвращаем Unit для обозначения успешного выполнения
        }
    }



}
