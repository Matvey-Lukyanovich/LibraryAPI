// GetAuthorByIdQuery.cs
using MediatR;
using LibraryAPI.Domain;

using LibraryAPI.Application.Interfaces;
public class GetAuthorByIdQuery : IRequest<Author>
{
    public int Id { get; set; }
}

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetByIdAsync(request.Id);
    }
}
