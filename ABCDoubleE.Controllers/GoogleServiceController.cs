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
        var booksFromGoogle = await _googleBooksService.PopulateDatabaseWithAuthorAsync(authorName);

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

    [HttpPost("populate/books/by-name")]
    public async Task<IActionResult> PopulateBooksByName([FromQuery] string bookTitle)
    {
        var booksFromGoogle = await _googleBooksService.PopulateDatabaseWithTitleAsync(bookTitle);

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
        return Ok($"Database populated with books with {bookTitle} from Google Books API.");
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] string query)
    {
        // Fetch books from the database
        var booksFromDatabase = await _bookService.SearchBooksAsync(query);

        // Transform database book authors and genres to lists of names
        var transformedBooksFromDatabase = booksFromDatabase.Select(book => new
        {
            book.bookId,
            book.isbn,
            book.title,
            book.description,
            book.image,
            authors = book.bookAuthors.Select(ba => ba.author.name).ToList(), // Transform authors to list of names
            genres = book.bookGenres.Select(bg => bg.genre.name).ToList(), // Transform genres to list of names
            book.bookshelfBooks,
            book.reviewList
        }).ToList();

        // Fetch books from Google API
        var booksFromGoogle = await _googleBooksService.SearchBooksByTitleAsync(query);

        // Transform Google API books to match the same structure
        var transformedBooksFromGoogle = booksFromGoogle.Select(book => new
        {
            book.bookId,
            book.isbn,
            book.title,
            book.description,
            book.image,
            authors = book.bookAuthors.Select(ba => ba.author.name).ToList(), // Assuming Google books authors are in a similar structure
            genres = book.bookGenres.Select(bg => bg.genre.name).ToList(), // Transform genres similarly
            book.bookshelfBooks,
            book.reviewList
        }).ToList();

        var combinedResults = new
        {
            booksFromDatabase = transformedBooksFromDatabase,
            booksFromGoogle = transformedBooksFromGoogle
        };

        return Ok(combinedResults);
    }

}
