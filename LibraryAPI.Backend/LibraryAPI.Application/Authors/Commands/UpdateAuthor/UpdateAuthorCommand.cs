using LibraryAPI.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraryAPI.Application.Interfaces;
public class UpdateAuthorCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
    public DateTime BirthDate { get; set; }
}

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Unit>
{
    private readonly IAuthorRepository _authorRepository;

    public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }


    public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.Id);
        if (author == null)
        {
            throw new NotFoundException(nameof(Author), request.Id);
        }

        author.FirstName = request.FirstName;
        author.LastName = request.LastName;
        author.BirthDate = request.BirthDate;
        author.Country = request.Country;
        
        await _authorRepository.UpdateAsync(author);
        return Unit.Value;  // Вернуть Unit для совместимости с IRequest<Unit>
    }
}
