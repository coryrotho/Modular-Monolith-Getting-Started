using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books;

internal interface IBookService
{
    IEnumerable<BookDto> ListBooks();
}

public record BookDto(Guid Id, string Title, string Author);

internal class BookService : IBookService
{
    public IEnumerable<BookDto> ListBooks()
    {
        return
        [
            new BookDto(Guid.NewGuid(), "Book One", "Cory Rothenberger"),
            new BookDto(Guid.NewGuid(), "Book Two", "Cory Rothenberger"),
            new BookDto(Guid.NewGuid(), "Book Three", "Cory Rothenberger")
        ];
    }
}
public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapGet("/books", (IBookService bookService) =>
        {
            return bookService.ListBooks();
        });
    }
}

public static class BookServiceExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        return services;
    }
}