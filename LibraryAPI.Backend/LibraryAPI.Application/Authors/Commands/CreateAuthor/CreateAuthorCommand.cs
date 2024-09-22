// CreateAuthorCommand.cs
using MediatR;
using LibraryAPI.Domain;
using LibraryAPI.Application.Interfaces;
using System.Diagnostics.Metrics;


public class CreateAuthorCommand : IRequest<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Country { get; set; }
    public DateTime BirthDate { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
{
    private readonly IAuthorRepository _authorRepository;

    public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Country = request.Country,
            BirthDate = request.BirthDate

        };

        await _authorRepository.AddAsync(author);
        return author.Id;
    }
}
