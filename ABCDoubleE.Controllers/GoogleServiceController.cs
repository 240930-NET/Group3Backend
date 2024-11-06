using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using ABCDoubleE.Data;


[ApiController]
[Route("api/[controller]")]
public class GoogleServiceController : ControllerBase
{
    private readonly GoogleBooksService _googleBooksService;
    private readonly ABCDoubleEContext _context;

    public GoogleServiceController(GoogleBooksService googleBooksService, ABCDoubleEContext context)
    {
        _googleBooksService = googleBooksService;
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

    // Future: Populate genres or authors independently, if needed
    // e.g., [HttpPost("populate/genres")]
}
