using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

internal class EfBookRepository : IBookRepository
{
    private readonly BookDbContext _dbContext;
    public EfBookRepository(BookDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await _dbContext!.Books.FindAsync(id);
    }

    public async Task<List<Book>> ListAsync()
    {
        return await _dbContext.Books.ToListAsync();
    }

    public Task AddAsync(Book book)
    {
        _dbContext.Add(book);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Book book)
    {
        _dbContext.Remove(book);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

internal class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<List<BookDto>> ListBooksAsync()
    {
        var books = (await _bookRepository.ListAsync()).Select(book =>
            new BookDto(book.Id, book.Title, book.Author, book.Price)).ToList();
        
        return books;
    }

    public async Task<BookDto> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        
        // TODO: Handle Not Found
        
        return new BookDto(book!.Id, book.Title, book.Author, book.Price);
    }

    public async Task CreateBookAsync(BookDto newBook)
    {
        var book = new Book(newBook.Id, newBook.Title, newBook.Author, newBook.Price);
        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var bookToDelete = await _bookRepository.GetByIdAsync(id);
        if (bookToDelete is not null)
        {
            await _bookRepository.DeleteAsync(bookToDelete);
            await _bookRepository.SaveChangesAsync();
        }
    }

    public async  Task UpdateBookPriceAsync(Guid id, decimal newPrice)
    {
        // validate the price
        var book = await _bookRepository.GetByIdAsync(id);
        // handle not found case
        book!.UpdatePrice(newPrice);
        await _bookRepository.SaveChangesAsync();
    }
}