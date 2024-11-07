using System.Linq;
using System.Threading.Tasks;
using ABCDoubleE.Data;
using ABCDoubleE.Models;
using Microsoft.EntityFrameworkCore;
namespace ABCDoubleE.Services;
public class DatabaseLookupService
{
    private readonly ABCDoubleEContext _context;

    public DatabaseLookupService(ABCDoubleEContext context)
    {
        _context = context;
    }

    // Check if a book exists by ISBN or title
    public async Task<Book?> GetExistingBookAsync(string isbn, string title)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.isbn == isbn || b.title == title);
    }


    public async Task<Author?> GetExistingAuthorAsync(string name)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.name == name);
    }

    public async Task<Genre?> GetExistingGenreAsync(string name)
    {
        return await _context.Genres.FirstOrDefaultAsync(g => g.name == name);
    }

    public async Task<Genre> GetOrCreateGenreAsync(string genreName)
    {
        var existingGenre = await _context.Genres
            .AsNoTracking() 
            .FirstOrDefaultAsync(g => g.name == genreName);

        if (existingGenre != null)
        {
            _context.Attach(existingGenre);
            return existingGenre;
        }

        var newGenre = new Genre { name = genreName };
        _context.Genres.Attach(newGenre);

        return newGenre;
    }

    // New methods to add and track entities without immediate save
    public void TrackNewGenre(Genre genre) => _context.Genres.Add(genre);
    public void TrackNewAuthor(Author author) => _context.Authors.Add(author);
    public void TrackNewBook(Book book) => _context.Books.Add(book);

    // Final save method to persist all changes at once
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

     // Check or add genre if not exists
    // Check or add genre if not exists, save immediately if new
    public async Task<Genre> GetOrAddGenreAsync(string name)
    {
        var genre = await GetExistingGenreAsync(name) ?? new Genre { name = name };
        if (genre.genreId == 0)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync(); // Save immediately to get the genreId
        }
        return genre;
    }

    // Check or add author if not exists, save immediately if new
    public async Task<Author> GetOrAddAuthorAsync(string name)
    {
        var author = await GetExistingAuthorAsync(name) ?? new Author { name = name };
        if (author.authorId == 0)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync(); // Save immediately to get the authorId
        }
        return author;
    }

    public async Task<List<string>> GetExistingISBNsAsync()
    {
        return await _context.Books
            .Where(b => !string.IsNullOrEmpty(b.isbn))
            .Select(b => b.isbn)
            .ToListAsync();
    }


}
