// GetAuthorsQuery.cs
using MediatR;
using System.Collections.Generic;
using LibraryAPI.Domain;

using LibraryAPI.Application.Interfaces;
public class GetAuthorsQuery : IRequest<List<Author>> { }

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, List<Author>>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorsQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<List<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetAllAsync();
    }
}
