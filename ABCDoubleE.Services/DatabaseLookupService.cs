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

    // Check if an author exists by name
    public async Task<Author?> GetExistingAuthorAsync(string name)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.name == name);
    }

    // Check if a genre exists by name
    public async Task<Genre?> GetExistingGenreAsync(string name)
    {
        return await _context.Genres.FirstOrDefaultAsync(g => g.name == name);
    }
}