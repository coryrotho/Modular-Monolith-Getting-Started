namespace RiverBooks.Books;

internal class BookService : IBookService
{
    public List<BookDto> ListBooks()
    {
        return
        [
            new BookDto(Guid.NewGuid(), "Book One", "Cory Rothenberger"),
            new BookDto(Guid.NewGuid(), "Book Two", "Cory Rothenberger"),
            new BookDto(Guid.NewGuid(), "Book Three", "Cory Rothenberger")
        ];
    }
}