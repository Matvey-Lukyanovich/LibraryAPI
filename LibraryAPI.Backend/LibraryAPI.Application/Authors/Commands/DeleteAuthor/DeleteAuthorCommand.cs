using MediatR;
using System.Threading;
using System.Threading.Tasks;

using LibraryAPI.Application.Interfaces;

public class DeleteAuthorCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Unit>
{
    private readonly IAuthorRepository _authorRepository;

    public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        await _authorRepository.DeleteAsync(request.Id);
        return Unit.Value;  // Вернуть Unit для совместимости с IRequest<Unit>
    }
}
