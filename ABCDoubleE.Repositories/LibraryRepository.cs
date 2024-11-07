using Microsoft.EntityFrameworkCore;
using ABCDoubleE.Models;
using ABCDoubleE.Data;

namespace ABCDoubleE.Repositories;
public class LibraryRepository : ILibraryRepository
{
    private readonly ABCDoubleEContext _context;

    public LibraryRepository(ABCDoubleEContext context)
    {
        _context = context;
    }


    public async Task<Library?> GetLibraryByIdAsync(int libraryId)
    {
        return await _context.Libraries
            .Include(l => l.bookshelfList) 
            .FirstOrDefaultAsync(l => l.libraryId == libraryId);
    }

    // Get all libraries
    public async Task<IEnumerable<Library>> GetAllLibrariesAsync()
    {
        return await _context.Libraries
            .Include(l => l.bookshelfList) 
            .ToListAsync();
    }

    // Add a new library
    public async Task<Library> AddLibraryAsync(Library library)
    {
    _context.Libraries.Add(library);
    await _context.SaveChangesAsync();
    return library;
    }
    // Update an existing library
    public async Task UpdateLibraryAsync(Library library)
    {
        _context.Libraries.Update(library);
        await _context.SaveChangesAsync();
    }

    // Delete a library by ID
    public async Task DeleteLibraryAsync(int libraryId)
    {
        var library = await _context.Libraries.FindAsync(libraryId);
        if (library != null)
        {
            _context.Libraries.Remove(library);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int?> GetLibraryIdByUserIdAsync(int userId)
    {
        var library = await _context.Libraries
            .Where(l => l.userId == userId)
            .Select(l => l.libraryId) 
            .FirstOrDefaultAsync();

        return library == 0 ? (int?)null : library;
    }

    // Get a  bookshelf by ID in a library
    public async Task<Bookshelf?> GetBookshelfByIdAsync(int libraryId, int bookshelfId)
    {
        var library = await _context.Libraries
            .Include(l => l.bookshelfList)
            .FirstOrDefaultAsync(l => l.libraryId == libraryId);
        
        return library?.bookshelfList.FirstOrDefault(b => b.bookshelfId == bookshelfId);
    }

        public async Task<IEnumerable<Bookshelf>> GetBookshelvesByLibraryIdAsync(int libraryId)
    {
        var library = await _context.Libraries
            .Where(l => l.libraryId == libraryId)
            .Include(l => l.bookshelfList)
            .FirstOrDefaultAsync();

        return library?.bookshelfList ?? Enumerable.Empty<Bookshelf>();
    }
}
