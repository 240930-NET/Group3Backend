using ABCDoubleE.Models;

namespace ABCDoubleE.Repositories;
public interface ILibraryRepository
{
    Task<Library?> GetLibraryByIdAsync(int libraryId);        
    Task<IEnumerable<Library>> GetAllLibrariesAsync();        
    Task<Library> AddLibraryAsync(Library library);                    
    Task UpdateLibraryAsync(Library library);                 
    Task DeleteLibraryAsync(int libraryId);                  
    Task<int?> GetLibraryIdByUserIdAsync(int userId);
    Task<Bookshelf?> GetBookshelfByIdAsync(int libraryId, int bookshelfId); 
    Task<IEnumerable<Bookshelf>> GetBookshelvesByLibraryIdAsync(int libraryId);
}
