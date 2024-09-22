using LibraryAPI.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

public class UploadImageCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public IFormFile ImageFile { get; set; }
}
public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Unit>
{
    private readonly IBookRepository _bookRepository;

    public UploadImageCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Unit> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);
        if (book == null)
        {
            throw new InvalidOperationException("Book not found.");
        }

        if (request.ImageFile == null || request.ImageFile.Length == 0)
        {
            throw new InvalidOperationException("No image file uploaded.");
        }

        var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(imagesPath))
        {
            Directory.CreateDirectory(imagesPath);
        }

        var filePath = Path.Combine(imagesPath, request.ImageFile.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.ImageFile.CopyToAsync(stream);
        }

        book.ImageUrl = $"/images/{request.ImageFile.FileName}";
        await _bookRepository.UpdateAsync(book);
        return Unit.Value;
    }
}