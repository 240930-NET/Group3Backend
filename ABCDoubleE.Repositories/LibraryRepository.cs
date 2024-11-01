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
    public async Task AddLibraryAsync(Library library)
    {
        await _context.Libraries.AddAsync(library);
        await _context.SaveChangesAsync();
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

    // Get a  bookshelf by ID in a library
    public async Task<Bookshelf?> GetBookshelfByIdAsync(int libraryId, int bookshelfId)
    {
        var library = await _context.Libraries
            .Include(l => l.bookshelfList)
            .FirstOrDefaultAsync(l => l.libraryId == libraryId);
        
        return library?.bookshelfList.FirstOrDefault(b => b.bookshelfId == bookshelfId);
    }
}
