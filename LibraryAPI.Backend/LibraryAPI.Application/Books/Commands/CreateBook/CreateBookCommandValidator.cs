using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace LibraryAPI.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Book).NotNull().WithMessage("Book details are required.");
            RuleFor(x => x.Book.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Book.ISBN).NotEmpty().WithMessage("ISBN is required.");
        }
    }
}
