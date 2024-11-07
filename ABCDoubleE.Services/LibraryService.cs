using ABCDoubleE.DTOs;
using ABCDoubleE.Repositories;
using ABCDoubleE.Models;

namespace ABCDoubleE.Services;
public class LibraryService : ILibraryService
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly IUserRepository _userRepository;

    public LibraryService(ILibraryRepository libraryRepository,IUserRepository userRepository)
    {
        _libraryRepository = libraryRepository;
        _userRepository = userRepository;
    }

    public async Task<Library> CreateLibraryAsync(int userId)
    {
        var library = new Library
        {
            userId = userId
        };

        return await _libraryRepository.AddLibraryAsync(library);
    }

    // Get a library by ID from user
    public async Task<LibraryDTO?> GetLibraryAsync(int libraryId)
    {
        var library = await _libraryRepository.GetLibraryByIdAsync(libraryId);
        if (library == null)
        {
            return null;
        }

        return new LibraryDTO
        {
            libraryId  = library.libraryId,
            bookshelfList = library.bookshelfList.Select(b => new BookshelfDTO
            {
                bookshelfId = b.bookshelfId,
                name = b.name
            }).ToList()
        };
    }

    // Get all bookshelves in a library
    public async Task<IEnumerable<BookshelfDTO>> GetAllBookshelvesAsync(int libraryId)
    {
        var library = await _libraryRepository.GetLibraryByIdAsync(libraryId);
        if (library == null)
        {
            return Enumerable.Empty<BookshelfDTO>();
        }

        return library.bookshelfList.Select(b => new BookshelfDTO
        {
            bookshelfId = b.bookshelfId,
            name = b.name
        });
    }

    // Get a bookshelf by ID in a library
    public async Task<BookshelfDTO?> GetBookshelfByIdAsync(int libraryId, int bookshelfId)
    {
        var bookshelf = await _libraryRepository.GetBookshelfByIdAsync(libraryId, bookshelfId);
        if (bookshelf == null)
        {
            return null;
        }

        return new BookshelfDTO
        {
            bookshelfId = bookshelf.bookshelfId,
            name = bookshelf.name
        };
    }

    // Add a new bookshelf to a  library
    public async Task<BookshelfDTO?> AddBookshelfAsync(int libraryId, BookshelfCreateDTO bookshelfCreateDto)
    {
        var library = await _libraryRepository.GetLibraryByIdAsync(libraryId);
        if (library == null)
        {
            throw new KeyNotFoundException($"Library with ID {libraryId} was not found.");
        }

        var bookshelf = new Bookshelf
        {
            name = bookshelfCreateDto.name
        };

        library.bookshelfList.Add(bookshelf);
        await _libraryRepository.UpdateLibraryAsync(library);

        return new BookshelfDTO
        {
            bookshelfId = bookshelf.bookshelfId,
            name = bookshelf.name
        };
    }

    // Delete a bookshelf from a library
    public async Task<bool> DeleteBookshelfAsync(int libraryId, int bookshelfId)
    {
        var library = await _libraryRepository.GetLibraryByIdAsync(libraryId);
        if (library == null)
        {
            return false;
        }

        var bookshelf = library.bookshelfList.FirstOrDefault(b => b.bookshelfId == bookshelfId);
        if (bookshelf == null)
        {
            return false;
        }

        library.bookshelfList.Remove(bookshelf);
        await _libraryRepository.UpdateLibraryAsync(library);
        return true;
    }

    // Delete a specific library
    public async Task<bool> DeleteLibraryAsync(int libraryId)
    {
        var library = await _libraryRepository.GetLibraryByIdAsync(libraryId);
        if (library == null)
        {
            return false;
        }

        await _libraryRepository.DeleteLibraryAsync(libraryId);
        return true;
    }

    public async Task<IEnumerable<Bookshelf>> GetBookshelvesByLibraryIdAsync(int libraryId)
    {
        return await _libraryRepository.GetBookshelvesByLibraryIdAsync(libraryId);
    }

    public async Task<int?> GetLibraryIdByUserIdAsync(int userId)
    {
        return await _libraryRepository.GetLibraryIdByUserIdAsync(userId);
    }
}
