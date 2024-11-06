using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using ABCDoubleE.Data;


[ApiController]
[Route("api/google")]
public class GoogleServiceController : ControllerBase
{
    private readonly GoogleBooksService _googleBooksService;
    private readonly IBookService _bookService;
    private readonly ABCDoubleEContext _context;

    public GoogleServiceController(GoogleBooksService googleBooksService, IBookService bookService, ABCDoubleEContext context)
    {
        _googleBooksService = googleBooksService;
        _bookService = bookService; 
        _context = context;
    }
    // Populate books by a specific author
    [HttpPost("populate/books/by-author")]
    public async Task<IActionResult> PopulateBooksByAuthor([FromQuery] string authorName)
    {
        var booksFromGoogle = await _googleBooksService.SearchBooksByAuthorAsync(authorName);

        foreach (var book in booksFromGoogle)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.isbn == book.isbn || b.title == book.title);

            if (existingBook == null)
            {
                foreach (var bookAuthor in book.bookAuthors)
                {
                    var existingAuthor = _context.Authors.FirstOrDefault(a => a.name == bookAuthor.author.name);
                    if (existingAuthor != null)
                    {
                        bookAuthor.author = existingAuthor;
                    }
                }

                _context.Books.Add(book);
            }
        }

        await _context.SaveChangesAsync();
        return Ok($"Database populated with books by {authorName} from Google Books API.");
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] string query)
    {
        var booksFromDatabase = await _bookService.SearchBooksAsync(query);

        var booksFromGoogle = await _googleBooksService.SearchBooksByTitleAsync(query);

        var combinedResults = new
        {
            booksFromDatabase,
            booksFromGoogle
        };

        return Ok(combinedResults);
    }
}
